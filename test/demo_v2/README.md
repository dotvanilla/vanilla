# Vanilla WebAssembly runtime — demo (v2)

A minimal, self contained demo proving the rewritten **lightweight** Vanilla runtime
(`dist/visualbasic.wasm.js`) runs a WebAssembly module that performs **console output**
and **math computation**.

## Files

| File | Purpose |
| --- | --- |
| `demo.wast` | Hand authored WebAssembly text module (console + math). |
| `demo.wasm` | `demo.wast` compiled by `dist/tools/wat2wasm.exe`. |
| `index.html` | Browser page: instantiates `demo.wasm` and calls `App.RunApp()`. |
| `run.mjs` | Node headless test: runs the demo and asserts every result. |

## Build

```powershell
& "G:\vanilla\dist\tools\wat2wasm.exe" demo.wast -o demo.wasm
```

## Run in the browser

The page must be served over HTTP (the runtime `fetch`es the `.wasm`):

```powershell
# any static server works, e.g. from g:/vanilla
python -m http.server 8080        # then open test/demo_v2/index.html
# or
npx http-server -p 8080 .
```

Then open <http://localhost:8080/test/demo_v2/index.html> and click **Run demo**.

## Run headless (Node)

```powershell
cd test/demo_v2
node run.mjs          # uses dist/visualbasic.wasm.js
node run.mjs --min    # uses dist/visualbasic.wasm.min.js
```

Expected output:

```
=== Vanilla WebAssembly Runtime Demo (console + math) ===
pi (Leibniz, n=20000) = 3.14154...
10! = 3628800
sqrt(2) = 1.4142135623730951
sin(pi/6) = 0.49999999999999994
cos(pi/3) = 0.5000000000000001
primes below 100 = 25
2.0-demo
demo finished. console + math verified in WebAssembly.
```

and `ALL ASSERTIONS PASSED`.

## What the demo exercises

* `console.log` / `console.warn` reading **null terminated UTF-8 strings from linear memory**.
* `string.add` (concatenation) and `string.toString` for both `i32` and `f64` overloads.
* Native `Math` imports (`sqrt`/`sin`/`cos`) — the math itself runs entirely inside wasm.
* `global.GetMemorySize` (used by the runtime to place its downward string allocator).
* `AssemblyInfo.*` reflection (title / version).

## Runtime contract (v2) in one paragraph

Every string crossing the wasm/JS boundary is a pointer into linear memory holding a
null terminated UTF-8 byte sequence. Strings produced by the runtime are allocated by a
JavaScript bump allocator that grows **downwards** from the top of linear memory, so it
never collides with the compiler managed heap (which grows upwards from low addresses).
The old `ObjectManager` JavaScript string/object dictionaries, the `-999999999..-1`
"hash code" scheme and the base64/JSON `GarbageCollection` reflection are gone.

The wasm **import/export names and type signatures are unchanged**, so existing compiler
output (e.g. `test/HelloWorld.wasm`) links and runs against the new runtime as-is.
