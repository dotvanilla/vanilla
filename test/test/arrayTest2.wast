(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/1/2019 8:44:37 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function array.new Lib "Array" Alias "create" (size As i32) As array
    (func $array.new (import "Array" "create") (param $size i32) (result i32))
    ;; Declare Function array_array.push Lib "Array" Alias "push" (array As array, element As array(Of f64)) As array
    (func $array_array.push (import "Array" "push") (param $array i32) (param $element i32) (result i32))
    ;; Declare Function array_array.get Lib "Array" Alias "get" (array As array, index As i32) As array(Of f64)
    (func $array_array.get (import "Array" "get") (param $array i32) (param $index i32) (result i32))
    ;; Declare Function array_array.set Lib "Array" Alias "set" (array As array, index As i32, value As array(Of f64)) As void
    (func $array_array.set (import "Array" "set") (param $array i32) (param $index i32) (param $value i32) )
    ;; Declare Function array.length Lib "Array" Alias "length" (array As array) As i32
    (func $array.length (import "Array" "length") (param $array i32) (result i32))
    ;; Declare Function i32_array.push Lib "Array" Alias "push" (array As array, element As i32) As array
    (func $i32_array.push (import "Array" "push") (param $array i32) (param $element i32) (result i32))
    ;; Declare Function i32_array.get Lib "Array" Alias "get" (array As array, index As i32) As i32
    (func $i32_array.get (import "Array" "get") (param $array i32) (param $index i32) (result i32))
    ;; Declare Function i32_array.set Lib "Array" Alias "set" (array As array, index As i32, value As i32) As void
    (func $i32_array.set (import "Array" "set") (param $array i32) (param $index i32) (param $value i32) )
    ;; Declare Function f64_array.push Lib "Array" Alias "push" (array As array, element As f64) As array
    (func $f64_array.push (import "Array" "push") (param $array i32) (param $element f64) (result i32))
    ;; Declare Function f64_array.get Lib "Array" Alias "get" (array As array, index As i32) As f64
    (func $f64_array.get (import "Array" "get") (param $array i32) (param $index i32) (result f64))
    ;; Declare Function f64_array.set Lib "Array" Alias "set" (array As array, index As i32, value As f64) As void
    (func $f64_array.set (import "Array" "set") (param $array i32) (param $index i32) (param $value f64) )
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    
    (global $arrayTest2.data (mut i32) (i32.const 0))

     



;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew

(set_global $arrayTest2.data (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $array.new (i32.const -1)) (f64.convert_s/i32 (i32.const 24))) (f64.convert_s/i32 (i32.const 23))) (f64.convert_s/i32 (i32.const 424))) (f64.convert_s/i32 (i32.const 2423))) (f64.convert_s/i32 (i32.const 4534))) (f64.convert_s/i32 (i32.const 5353))) (f64.convert_s/i32 (i32.const 55))) (f64.convert_s/i32 (i32.const 55))) (f64.convert_s/i32 (i32.const 55))) (f64.convert_s/i32 (i32.const 55))) (f64.convert_s/i32 (i32.const 5555))) (f64.convert_s/i32 (i32.const 5))))
)

(start $Application_SubNew)

)