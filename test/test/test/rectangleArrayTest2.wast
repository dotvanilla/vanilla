(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/18/2019 4:47:08 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 348))

    ;; Memory data for string constant
    
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 11 with 336 bytes in memory
    ;;
    ;; class [11] MyCircle
    ;;
    (data (i32.const 11) "eyJjb21tZW50IjpudWxsLCJtZW1vcnlQdHIiOnsiVmFsdWUiOjExfSwiY2xhc3MiOiJNeUNpcmNsZSIsImNsYXNzX2lkIjoxMSwiZmllbGRzIjp7IngiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImY2NCIsInR5cGUiOjR9LCJ5Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJmNjQiLCJ0eXBlIjo0fSwiciI6eyJnZW5lcmljIjpbXSwicmF3IjoiZjY0IiwidHlwZSI6NH19LCJpc1N0cnVjdCI6ZmFsc2UsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOm51bGx9\00")

    ;; Pre-defined constant values
    (global $Math.E (mut f64) (f64.const 2.7182818284590451))
(global $Math.PI (mut f64) (f64.const 3.1415926535897931))
(global $Integer.MaxValue (mut i32) (i32.const 2147483647))
(global $Long.MaxValue (mut i64) (i64.const 9223372036854775807))
(global $Single.MaxValue (mut f32) (f32.const 3.40282347e+38))
(global $Double.MaxValue (mut f64) (f64.const 1.7976931348623157e+308))
(global $Integer.MinValue (mut i32) (i32.const -2147483648))
(global $Long.MinValue (mut i64) (i64.const -9223372036854775808))
(global $Single.MinValue (mut f32) (f32.const -3.40282347e+38))
(global $Double.MinValue (mut f64) (f64.const -1.7976931348623157e+308))

    ;; Global variables in this module
    

    ;; Export methods of this module
     




    ;; Application Initialize
    ;; 
    ;; Sub New
    (func $Application_SubNew
        ;; call of the global variable initialize
        (call $global.initializer )

        (call $rectangleArrayTest2.constructor )
    )

    ;; Initializer for global variables if it is not a primitive abstract type
    (func $global.initializer  
    ;; Public Function initializer() As void
    



)

    (func $rectangleArrayTest2.constructor  
    ;; Public Function constructor() As void
    
(local $d i32)
(local $arrayOffset_9a020000 i32)
(local $a i32)
(local $b i32)
(local $c f32)

(set_local $d (i32.const 0))

;; Save (i32.sub (i32.const 100) (i32.const 99)) array element data to memory:
;; Array memory block begin at location: (get_global $global.ObjectManager)
;; class_id/typealias_enum i32 data: (i32.const 11)/array(Of intptr)
(i32.store (get_global $global.ObjectManager) (i32.const 11))
(i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.sub (i32.const 100) (i32.const 99)))
;; End of byte marks meta data, start write data blocks
;; Offset object manager with (i32.add (i32.const 8) (i32.mul (i32.sub (i32.const 100) (i32.const 99)) (i32.const 4))) bytes
(set_local $arrayOffset_9a020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
(set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9a020000) (i32.const -8)) (i32.add (i32.const 8) (i32.mul (i32.sub (i32.const 100) (i32.const 99)) (i32.const 4)))))
;; Assign array memory data to another expression
(set_local $a (i32.add (get_local $arrayOffset_9a020000) (i32.const -8)))
(set_local $b (i32.load (i32.add (i32.add (get_local $a) (i32.const 8)) (i32.mul (i32.const 3) (i32.const 4)))))
(set_local $c (f32.demote/f64 (f64.load (i32.add (i32.load (i32.add (i32.add (get_local $b) (i32.const 8)) (i32.mul (i32.const 33) (i32.const 4)))) (i32.const 16)))))
(i32.store (i32.add (i32.add (get_local $d) (i32.const 8)) (i32.mul (i32.const 88) (i32.const 4))) (i32.trunc_s/f64 (f64.add (f64.load (i32.add (i32.load (i32.add (i32.add (get_local $b) (i32.const 8)) (i32.mul (i32.const 5) (i32.const 4)))) (i32.const 0))) (f64.load (i32.add (i32.load (i32.add (i32.add (get_local $b) (i32.const 8)) (i32.mul (i32.const 0) (i32.const 4)))) (i32.const 8))))))
)

    (start $Application_SubNew)
)