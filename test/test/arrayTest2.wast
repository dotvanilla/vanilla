(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/11/2019 3:43:13 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function print Lib "console" Alias "log" (x As f64) As void
    (func $arrayTest2.print (import "console" "log") (param $x f64) )
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 11))

    ;; Memory data for string constant
    
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    

    ;; Global variables in this module
    (global $Math.E (mut f64) (f64.const 2.71828182845905))
(global $Math.PI (mut f64) (f64.const 3.14159265358979))
(global $Integer.MaxValue (mut i32) (i32.const 2147483647))
(global $Long.MaxValue (mut i64) (i64.const 9223372036854775807))
(global $Single.MaxValue (mut f32) (f32.const 340282356779733623858607532500980858880))
(global $Double.MaxValue (mut f64) (f64.const 179769313486231570814527423731704356798070567525844996598917476803157260780028538760589558632766878171540458953514382464234321326889464182768467546703537516986049910576551282076245490090389328944075868508455133942304583236903222948165808559332123348274797826204144723168738177180919299881250404026184124858368))
(global $Integer.MinValue (mut i32) (i32.const -2147483648))
(global $Long.MinValue (mut i64) (i64.const -9223372036854775808))
(global $Single.MinValue (mut f32) (f32.const -340282356779733623858607532500980858880))
(global $Double.MinValue (mut f64) (f64.const -179769313486231570814527423731704356798070567525844996598917476803157260780028538760589558632766878171540458953514382464234321326889464182768467546703537516986049910576551282076245490090389328944075868508455133942304583236903222948165808559332123348274797826204144723168738177180919299881250404026184124858368))
(global $arrayTest2.data (mut i32) (i32.const 0))

    ;; Export methods of this module
    ;; export from VB.NET module: [arrayTest2]
    
    (export "arrayTest2.returnArrayTest" (func $arrayTest2.returnArrayTest))
    (export "arrayTest2.readTest" (func $arrayTest2.readTest))
    (export "arrayTest2.setValueTest" (func $arrayTest2.setValueTest))
    
     

    ;; functions in [arrayTest2]
    
    (func $arrayTest2.returnArrayTest  (result i32)
        ;; Public Function returnArrayTest() As array(Of f32)
        
    (local $x f64)
    (local $arrayOffset_9a020000 i32)
    
    (set_local $x (f64.load (i32.add (i32.add (get_global $arrayTest2.data) (i32.const 8)) (i32.mul (i32.const 1) (i32.const 8)))))
    
    ;; Save (i32.const 8) array element data to memory:
    ;; Array memory block begin at location: (get_global $global.ObjectManager)
    ;; class_id/typealias_enum i32 data: (i32.const 3)/array(Of f32)
    (i32.store (get_global $global.ObjectManager) (i32.const 3))
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 8))
    ;; End of byte marks meta data, start write data blocks
    ;; Offset object manager with (i32.add (i32.const 8) (i32.mul (i32.const 8) (i32.const 4))) bytes
    (set_local $arrayOffset_9a020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    (set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9a020000) (i32.const -8)) (i32.add (i32.const 8) (i32.mul (i32.const 8) (i32.const 4)))))
    (f32.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 0)) (f32.demote/f64 (get_local $x)))
    (f32.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 4)) (f32.convert_s/i32 (i32.const 0)))
    (f32.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 8)) (f32.convert_s/i32 (i32.const 35)))
    (f32.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 12)) (f32.convert_s/i32 (i32.const 78345)))
    (f32.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 16)) (f32.convert_s/i32 (i32.const 34)))
    (f32.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 20)) (f32.convert_s/i32 (i32.const 534)))
    (f32.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 24)) (f32.convert_s/i32 (i32.const 53)))
    (f32.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 28)) (f32.convert_s/i32 (i32.load (i32.add (get_global $arrayTest2.data) (i32.const 4)))))
    ;; Assign array memory data to another expression
    (return (i32.add (get_local $arrayOffset_9a020000) (i32.const -8)))
    )
    
    
    (func $arrayTest2.readTest  (result f32)
        ;; Public Function readTest() As f32
        
    (local $x i64)
    (local $i i32)
    
    (set_local $x (i64.trunc_s/f64 (f64.load (i32.add (i32.add (get_global $arrayTest2.data) (i32.const 8)) (i32.mul (i32.const 9999) (i32.const 8))))))
    (set_local $i (i32.const 0))
    ;; For i As Integer = 0 To data.Length - 1
    
    (block $block_9b020000 
        (loop $loop_9c020000
    
                    (br_if $block_9b020000 (i32.gt_s (get_local $i) (i32.sub (i32.load (i32.add (get_global $arrayTest2.data) (i32.const 4))) (i32.const 1))))
            (call $arrayTest2.print (f64.load (i32.add (i32.add (get_global $arrayTest2.data) (i32.const 8)) (i32.mul (get_local $i) (i32.const 8)))))
            ;; For loop control step: (i32.const 1)
            (set_local $i (i32.add (get_local $i) (i32.const 1)))
            (br $loop_9c020000)
            ;; For Loop Next On loop_9c020000
    
        )
    )
    (return (f32.convert_s/i64 (get_local $x)))
    )
    
    
    (func $arrayTest2.setValueTest (param $x i32) 
        ;; Public Function setValueTest(x As i32) As void
        
    
    
    (f64.store (i32.add (i32.add (get_global $arrayTest2.data) (i32.const 8)) (i32.mul (i32.add (get_local $x) (i32.const 1)) (i32.const 8))) (f64.div (f64.convert_s/i32 (i32.mul (get_local $x) (i32.const 2))) (f64.convert_s/i32 (i32.sub (i32.load (i32.add (get_global $arrayTest2.data) (i32.const 4))) (i32.const 1)))))
    (call $arrayTest2.print (f64.load (i32.add (i32.add (get_global $arrayTest2.data) (i32.const 8)) (i32.mul (i32.mul (get_local $x) (i32.const 99)) (i32.const 8)))))
    )
    
    
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $arrayTest2.constructor )
)

(func $arrayTest2.constructor  
    ;; Public Function constructor() As void
    
(local $arrayOffset_9d020000 i32)


;; Save (i32.const 12) array element data to memory:
;; Array memory block begin at location: (get_global $global.ObjectManager)
;; class_id/typealias_enum i32 data: (i32.const 4)/array(Of f64)
(i32.store (get_global $global.ObjectManager) (i32.const 4))
(i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 12))
;; End of byte marks meta data, start write data blocks
;; Offset object manager with (i32.add (i32.const 8) (i32.mul (i32.const 12) (i32.const 8))) bytes
(set_local $arrayOffset_9d020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
(set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9d020000) (i32.const -8)) (i32.add (i32.const 8) (i32.mul (i32.const 12) (i32.const 8)))))
(f64.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 0)) (f64.convert_s/i32 (i32.const 24)))
(f64.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 8)) (f64.convert_s/i32 (i32.const 23)))
(f64.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 16)) (f64.convert_s/i32 (i32.const 424)))
(f64.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 24)) (f64.convert_s/i32 (i32.const 2423)))
(f64.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 32)) (f64.convert_s/i32 (i32.const 4534)))
(f64.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 40)) (f64.convert_s/i32 (i32.const 5353)))
(f64.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 48)) (f64.convert_s/i32 (i32.const 55)))
(f64.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 56)) (f64.convert_s/i32 (i32.const 55)))
(f64.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 64)) (f64.convert_s/i32 (i32.const 55)))
(f64.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 72)) (f64.convert_s/i32 (i32.const 55)))
(f64.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 80)) (f64.convert_s/i32 (i32.const 5555)))
(f64.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 88)) (f64.convert_s/i32 (i32.const 5)))
;; Assign array memory data to another expression
(set_global $arrayTest2.data (i32.add (get_local $arrayOffset_9d020000) (i32.const -8)))
)

(start $Application_SubNew)
)