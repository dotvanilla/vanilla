(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/2/2019 12:14:00 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function print Lib "console" Alias "log" (x As f64) As void
    (func $arrayTest2.print (import "console" "log") (param $x f64) )
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

    ;; export from VB.NET module: [arrayTest2]
    
    (export "arrayTest2.returnArrayTest" (func $arrayTest2.returnArrayTest))
    (export "arrayTest2.readTest" (func $arrayTest2.readTest))
    (export "arrayTest2.setValueTest" (func $arrayTest2.setValueTest))
    
     

    ;; functions in [arrayTest2]
    
    (func $arrayTest2.returnArrayTest  (result i32)
        ;; Public Function returnArrayTest() As array(Of i32)
        
    (return (i32.const 1))
    )
    (func $arrayTest2.readTest  (result f32)
        ;; Public Function readTest() As f32
        (local $x i64)
    (local $i i32)
    (set_local $x (i64.trunc_s/f64 (f64.load (i32.add (get_global $arrayTest2.data) (i32.mul (i32.const 9999) (i32.const 8))))))
    (set_local $i (i32.const 0))
    ;; For i As Integer = 0 To data.Length - 1
    
    (block $block_9a020000 
        (loop $loop_9b020000
    
                    (br_if $block_9a020000 (i32.gt_s (get_local $i) (i32.sub (call $array.length (get_global $arrayTest2.data)) (i32.const 1))))
            (call $arrayTest2.print (f64.load (i32.add (get_global $arrayTest2.data) (i32.mul (get_local $i) (i32.const 8)))))
            ;; For loop control step: (i32.const 1)
            (set_local $i (i32.add (get_local $i) (i32.const 1)))
            (br $loop_9b020000)
            ;; For Loop Next On loop_9b020000
    
        )
    )
    (return (f32.convert_s/i64 (get_local $x)))
    )
    (func $arrayTest2.setValueTest (param $x i32) 
        ;; Public Function setValueTest(x As i32) As void
        
    (f64.store (i32.add (get_global $arrayTest2.data) (i32.mul (i32.add (get_local $x) (i32.const 1)) (i32.const 8))) (f64.convert_s/i32 (i32.mul (get_local $x) (i32.const 2))))
    (call $arrayTest2.print (f64.load (i32.add (get_global $arrayTest2.data) (i32.mul (i32.mul (get_local $x) (i32.const 99)) (i32.const 8)))))
    )
    

;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew

;; 
;; Save 12 array element data to memory:
;; Array memory block begin at location: 26
(i32.store (i32.const 26) (i32.const 24))
(i32.store (i32.const 34) (i32.const 23))
(i32.store (i32.const 42) (i32.const 424))
(i32.store (i32.const 50) (i32.const 2423))
(i32.store (i32.const 58) (i32.const 4534))
(i32.store (i32.const 66) (i32.const 5353))
(i32.store (i32.const 74) (i32.const 55))
(i32.store (i32.const 82) (i32.const 55))
(i32.store (i32.const 90) (i32.const 55))
(i32.store (i32.const 98) (i32.const 55))
(i32.store (i32.const 106) (i32.const 5555))
(i32.store (i32.const 114) (i32.const 5))
;; Assign array memory data to another expression
(set_global $arrayTest2.data (i32.const 26))
)

(start $Application_SubNew)

)