@echo off

java -jar closure-compiler-v20181125.jar --js_output_file=../visualbasic.wasm.min.js ../visualbasic.wasm.js

pause