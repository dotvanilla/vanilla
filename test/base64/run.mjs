// base64.vbproj -> wasm 的 Node 运行与验收脚本
//
//   node run.mjs            使用 dist/visualbasic.wasm.js
//   node run.mjs --min      使用 dist/visualbasic.wasm.min.js
//
// 验收标准：Base64Codec.EncodeText / DecodeText 对固定样例的结果与 .NET 完全一致

import fs from 'node:fs';
import path from 'node:path';
import vm from 'node:vm';
import { fileURLToPath } from 'node:url';

const here = path.dirname(fileURLToPath(import.meta.url));
const useMin = process.argv.includes('--min');
const runtimeName = useMin ? 'visualbasic.wasm.min.js' : 'visualbasic.wasm.js';
const runtimePath = path.resolve(here, '..', '..', 'dist', runtimeName);
const wasmPath = path.resolve(here, 'base64.wasm');

// ---------------- 载入运行时 ----------------
vm.runInThisContext(fs.readFileSync(runtimePath, 'utf8'), { filename: runtimePath });

const nativeConsole = console;
const captured = [];

// 捕获 wasm 侧 console.log 输出
globalThis.console = {
    log: (...a) => captured.push(['log', a.join(' ')]),
    info: (...a) => captured.push(['info', a.join(' ')]),
    warn: (...a) => captured.push(['warn', a.join(' ')]),
    error: (...a) => captured.push(['error', a.join(' ')]),
    debug: (...a) => captured.push(['log', a.join(' ')]),
    trace: (...a) => captured.push(['log', a.join(' ')])
};

// ---------------- 载入并实例化 ----------------
const app = await Vanilla.run(fs.readFileSync(wasmPath));

nativeConsole.log(`=== base64 demo (runtime: ${runtimeName}) ===`);
nativeConsole.log(`exports: ${Object.keys(app.exports).join(', ')}`);

// ---------------- 运行 Program.Main()（保持 console 捕获） ----------------
const main = app.exports['Program.Main'];
if (typeof main !== 'function') {
    throw new Error('wasm module 之中没有导出 Program.Main');
}
const exitCode = main();

// 恢复真实终端输出，并把 wasm 侧控制台输出回显
globalThis.console = nativeConsole;

nativeConsole.log(`Program.Main() returned ${exitCode}`);
nativeConsole.log('---- console output from wasm ----');
for (const [, line] of captured) {
    nativeConsole.log(line);
}
nativeConsole.log('----------------------------------');

// ---------------- 断言 ----------------
let passed = 0;
let failed = 0;

function check(name, actual, expected) {
    if (actual === expected) {
        passed++;
        nativeConsole.log(`PASS  ${name}`);
    } else {
        failed++;
        nativeConsole.log(`FAIL  ${name}\n      expected: ${JSON.stringify(expected)}\n      actual  : ${JSON.stringify(actual)}`);
    }
}

function invokeText(fullName, ...args) {
    const args2 = args.map(a => (typeof a === 'string' ? Vanilla.writeText(a) : a));
    const result = app.exports[fullName](...args2);
    return Vanilla.readText(result);
}

// 参考值由 Node 的 UTF-8 + base64 实现给出（与 .NET Convert.ToBase64String(Encoding.UTF8.GetBytes()) 一致）
function reference(text) {
    return Buffer.from(text, 'utf8').toString('base64');
}

const samples = ['Man', 'Ma', 'M', '你好，Base64！', '', 'Hello, World!'];

for (const text of samples) {
    const encoded = invokeText('Base64Codec.EncodeText', text);
    const expected = reference(text);

    check(`EncodeText(${JSON.stringify(text)}) === .NET`, encoded, expected);

    const decoded = invokeText('Base64Codec.DecodeText', expected);
    check(`DecodeText round-trip (${JSON.stringify(text)})`, decoded, text);
}

// Program.Main 应当已经打印出全部通过
const allPassLine = captured.map(x => x[1]).find(l => l.includes('全部通过'));
check('random cross-check printed', typeof allPassLine === 'string', true);
if (allPassLine) {
    check('random cross-check passed', allPassLine.includes('True'), true);
}

nativeConsole.log(`\n===== ${passed} passed, ${failed} failed =====`);
if (failed > 0) {
    process.exit(1);
}
