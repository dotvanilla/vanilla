@echo off

SET gcc="./closure-compiler-v20181125.jar"

SET release=../../runtime/build
SET min=../../dist

java -jar %gcc% --js_output_file=%min%/visualbasic.wasm.min.js %release%/visualbasic.wasm.js
java -jar %gcc% --js_output_file=%min%/linq.min.js %release%/linq.js

pause