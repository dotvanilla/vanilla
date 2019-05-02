(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/2/2019 11:01:16 AM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function array.new Lib "Array" Alias "create" (size As i32) As list
    (func $array.new (import "Array" "create") (param $size i32) (result i32))
    ;; Declare Function i32_array.push Lib "Array" Alias "push" (array As list, element As i32) As list
    (func $i32_array.push (import "Array" "push") (param $array i32) (param $element i32) (result i32))
    ;; Declare Function i32_array.get Lib "Array" Alias "get" (array As list, index As i32) As i32
    (func $i32_array.get (import "Array" "get") (param $array i32) (param $index i32) (result i32))
    ;; Declare Function i32_array.set Lib "Array" Alias "set" (array As list, index As i32, value As i32) As void
    (func $i32_array.set (import "Array" "set") (param $array i32) (param $index i32) (param $value i32) )
    ;; Declare Function array.length Lib "Array" Alias "length" (array As list) As i32
    (func $array.length (import "Array" "length") (param $array i32) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    
    (global $arrayTest2.data (mut i32) (i32.const 0))

     



;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew

;; 
;; Save 12 array element data to memory:
;; Array memory block begin at location: 1
(i32.store (i32.const 1) (i32.const 24))
(i32.store (i32.const 9) (i32.const 23))
(i32.store (i32.const 17) (i32.const 424))
(i32.store (i32.const 25) (i32.const 2423))
(i32.store (i32.const 33) (i32.const 4534))
(i32.store (i32.const 41) (i32.const 5353))
(i32.store (i32.const 49) (i32.const 55))
(i32.store (i32.const 57) (i32.const 55))
(i32.store (i32.const 65) (i32.const 55))
(i32.store (i32.const 73) (i32.const 55))
(i32.store (i32.const 81) (i32.const 5555))
(i32.store (i32.const 89) (i32.const 5))
;; Assign array memory data to another expression
(set_global $arrayTest2.data (i32.const 1))
)

(start $Application_SubNew)

)