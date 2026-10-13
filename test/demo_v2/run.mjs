/**
 * Headless (Node.js) verification of the lightweight Vanilla WebAssembly runtime.
 *
 *   node run.mjs            # use dist/visualbasic.wasm.js
 *   node run.mjs --min      # use dist/visualbasic.wasm.min.js
 *
 * It loads the runtime, instantiates test/demo_v2/demo.wasm, runs App.RunApp and
 * asserts both the console output and every computed math result.
 */
import fs from 'node:fs';
import path from 'node:path';
import vm from 'node:vm';
import { fileURLToPath } from 'node:url';

const here = path.dirname(fileURLToPath(import.meta.url));
const useMin = process.argv.includes('--min');
const runtimeFile = useMin ? 'visualbasic.wasm.min.js' : 'visualbasic.wasm.js';
const runtimePath = path.resolve(here, '../../dist', runtimeFile);
const wasmPath = path.resolve(here, 'demo.wasm');

// ---------------------------------------------------------------------------
// load the runtime (plain script that attaches to globalThis)
// ---------------------------------------------------------------------------
const runtimeSource = fs.readFileSync(runtimePath, 'utf8');
vm.runInThisContext(runtimeSource, { filename: runtimePath });
const Vanilla = globalThis.Vanilla;
if (!Vanilla) { throw new Error('runtime did not expose globalThis.Vanilla'); }

// ---------------------------------------------------------------------------
// capture console output
// ---------------------------------------------------------------------------
const output = [];
const nativeConsole = console;
const capture = (level, color) => (...args) => output.push({ level, args });
const fakeConsole = {
    log: capture('log', ''),
    info: capture('info', ''),
    warn: capture('warn', ''),
    error: capture('error', ''),
    debug: capture('debug', ''),
    trace: capture('trace', '')
};

let failures = 0;
function check(name, cond, detail = '') {
    const ok = !!cond;
    if (!ok) { failures++; }
    nativeConsole.log(`${ok ? 'PASS' : 'FAIL'}  ${name}${detail && !ok ? '  -> ' + detail : ''}`);
}

// ---------------------------------------------------------------------------
// run the demo
// ---------------------------------------------------------------------------
globalThis.console = fakeConsole;

const bytes = fs.readFileSync(wasmPath);
const t0 = process.hrtime.bigint();
const app = await Vanilla.run(bytes);
const loadMs = Number(process.hrtime.bigint() - t0) / 1e6;

const t1 = process.hrtime.bigint();
const code = app.App.RunApp();
const runMs = Number(process.hrtime.bigint() - t1) / 1e6;

globalThis.console = nativeConsole;

// ---------------------------------------------------------------------------
// assertions
// ---------------------------------------------------------------------------
const lines = output.map(o => o.args.join(' '));
nativeConsole.log('\n--- captured console output ---');
for (const l of lines) { nativeConsole.log('  ' + l); }
nativeConsole.log('-------------------------------\n');

const findLine = (prefix) => lines.find(l => l.startsWith(prefix));
const valueAfter = (prefix) => {
    const l = findLine(prefix);
    if (!l) { return undefined; }
    return l.substring(l.lastIndexOf('=') + 1).trim();
};

check('App.RunApp returns 0', code === 0, String(code));
check('header printed', lines[0] === '=== Vanilla WebAssembly Runtime Demo (console + math) ===', lines[0]);

const pi = parseFloat(valueAfter('pi (Leibniz'));
check('pi (Leibniz) within delta 1e-3', Math.abs(pi - Math.PI) < 1e-3, String(pi));

const fact = valueAfter('10! ');
check('10! = 3628800', fact === '3628800', String(fact));

const sqrt2 = parseFloat(valueAfter('sqrt(2)'));
check('sqrt(2) within delta 1e-15', Math.abs(sqrt2 - Math.sqrt(2)) < 1e-15, String(sqrt2));

const sin30 = parseFloat(valueAfter('sin(pi/6)'));
check('sin(pi/6) ~= 0.5', Math.abs(sin30 - 0.5) < 1e-12, String(sin30));

const cos60 = parseFloat(valueAfter('cos(pi/3)'));
check('cos(pi/3) ~= 0.5', Math.abs(cos60 - 0.5) < 1e-12, String(cos60));

const primes = valueAfter('primes below 100');
check('primes below 100 = 25', primes === '25', String(primes));

check('console.warn captured "2.0-demo"', output.some(o => o.level === 'warn' && o.args[0] === '2.0-demo'));
check('finished line printed', lines[lines.length - 1].startsWith('demo finished.'), lines[lines.length - 1]);

const info = Vanilla.AssemblyInfo.read(app.instance);
check('AssemblyTitle', info.AssemblyTitle === 'Vanilla WASM Demo', info.AssemblyTitle);
check('AssemblyVersion', info.AssemblyVersion === '2.0-demo', info.AssemblyVersion);

// ---------------------------------------------------------------------------
// runtime string micro benchmark (linear memory allocator vs. removed js dictionary)
// ---------------------------------------------------------------------------
const N = 50000;
const tb = process.hrtime.bigint();
let sink = 0;
for (let i = 0; i < N; i++) {
    const p = Vanilla.writeText('item-' + i + '-abcdefghijklmnop');
    sink += Vanilla.readText(p).length;
}
const strMs = Number(process.hrtime.bigint() - tb) / 1e6;

nativeConsole.log(`\nruntime   : ${runtimeFile}`);
nativeConsole.log(`instantiate: ${loadMs.toFixed(3)} ms`);
nativeConsole.log(`App.RunApp : ${runMs.toFixed(3)} ms`);
nativeConsole.log(`string ops : ${N} write+read pairs in ${strMs.toFixed(3)} ms (${(N / strMs * 1000).toFixed(0)} ops/s, sink=${sink})`);

if (failures > 0) {
    nativeConsole.log(`\n${failures} assertion(s) FAILED`);
    process.exit(1);
} else {
    nativeConsole.log('\nALL ASSERTIONS PASSED');
}
