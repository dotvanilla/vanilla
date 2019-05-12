(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/12/2019 7:42:16 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    
    
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

    ;; Export methods of this module
     




;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $rectangleArrayTest.constructor )
)

(func $rectangleArrayTest.constructor  
    ;; Public Function constructor() As void
    
(local $arrayOffset_9a020000 i32)
(local $a i32)


;; Save (i32.const 100) array element data to memory:
;; Array memory block begin at location: (get_global $global.ObjectManager)
;; class_id/typealias_enum i32 data: (i32.const 4)/array(Of f64)
(i32.store (get_global $global.ObjectManager) (i32.const 4))
(i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 100))
;; End of byte marks meta data, start write data blocks
;; Offset object manager with (i32.add (i32.const 8) (i32.mul (i32.const 100) (i32.const 8))) bytes
(set_local $arrayOffset_9a020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
(set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9a020000) (i32.const -8)) (i32.add (i32.const 8) (i32.mul (i32.const 100) (i32.const 8)))))
;; Assign array memory data to another expression
(set_local $a (i32.add (get_local $arrayOffset_9a020000) (i32.const -8)))
)

(start $Application_SubNew)
)