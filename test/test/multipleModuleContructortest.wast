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

    ;; Declare Function string.replace Lib "string" Alias "replace" (input As string, find As intptr, replacement As string) As string
    (func $string.replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
;; Declare Function string.add Lib "string" Alias "add" (a As string, b As string) As string
    (func $string.add (import "string" "add") (param $a i32) (param $b i32) (result i32))
;; Declare Function string.length Lib "string" Alias "length" (text As string) As i32
    (func $string.length (import "string" "length") (param $text i32) (result i32))
;; Declare Function string.indexOf Lib "string" Alias "indexOf" (input As string, find As string) As i32
    (func $string.indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 23))

    ;; Memory data for string constant
        
    ;; String from 11 with 3 bytes in memory
    (data (i32.const 11) "sdf\00")
    
    ;; String from 15 with 3 bytes in memory
    (data (i32.const 15) "sdf\00")
    
    ;; String from 19 with 3 bytes in memory
    (data (i32.const 19) "sdf\00")
    
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
(global $multipleModuleContructortest1.a (mut i32) (i32.const 0))
(global $multipleModuleContructortest2.a (mut i32) (i32.const 0))
(global $multipleModuleContructortest3.a (mut i32) (i32.const 0))

    ;; Export methods of this module
     




;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $multipleModuleContructortest1.constructor )

(call $multipleModuleContructortest2.constructor )

(call $multipleModuleContructortest3.constructor )
)

(func $multipleModuleContructortest1.constructor  
    ;; Public Function constructor() As void
    
(local $arrayOffset_9a020000 i32)


;; Save (i32.const 4) array element data to memory:
;; Array memory block begin at location: (get_global $global.ObjectManager)
;; class_id/typealias_enum i32 data: (i32.const 4)/array(Of f64)
(i32.store (get_global $global.ObjectManager) (i32.const 4))
(i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 4))
;; End of byte marks meta data, start write data blocks
;; Offset object manager with (i32.add (i32.const 8) (i32.mul (i32.const 4) (i32.const 8))) bytes
(set_local $arrayOffset_9a020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
(set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9a020000) (i32.const -8)) (i32.add (i32.const 8) (i32.mul (i32.const 4) (i32.const 8)))))
(f64.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 0)) (f64.convert_s/i32 (i32.const 4)))
(f64.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 8)) (f64.convert_s/i32 (i32.const 353)))
(f64.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 16)) (f64.convert_s/i32 (i32.const 453534)))
(f64.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 24)) (f64.convert_s/i32 (i32.const 534)))
;; Assign array memory data to another expression
(set_global $multipleModuleContructortest1.a (i32.add (get_local $arrayOffset_9a020000) (i32.const -8)))
)

(func $multipleModuleContructortest2.constructor  
    ;; Public Function constructor() As void
    
(local $arrayOffset_9a020000 i32)
(local $arrayOffset_9b020000 i32)


;; Save (i32.const 3) array element data to memory:
;; Array memory block begin at location: (get_global $global.ObjectManager)
;; class_id/typealias_enum i32 data: (i32.const 5)/array(Of string)
(i32.store (get_global $global.ObjectManager) (i32.const 5))
(i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 3))
;; End of byte marks meta data, start write data blocks
;; Offset object manager with (i32.add (i32.const 8) (i32.mul (i32.const 3) (i32.const 4))) bytes
(set_local $arrayOffset_9b020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
(set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9b020000) (i32.const -8)) (i32.add (i32.const 8) (i32.mul (i32.const 3) (i32.const 4)))))
(i32.store (i32.add (get_local $arrayOffset_9b020000) (i32.const 0)) (i32.const 11))
(i32.store (i32.add (get_local $arrayOffset_9b020000) (i32.const 4)) (i32.const 15))
(i32.store (i32.add (get_local $arrayOffset_9b020000) (i32.const 8)) (i32.const 19))
;; Assign array memory data to another expression
(set_global $multipleModuleContructortest2.a (i32.add (get_local $arrayOffset_9b020000) (i32.const -8)))
)

(func $multipleModuleContructortest3.constructor  
    ;; Public Function constructor() As void
    
(local $arrayOffset_9a020000 i32)
(local $arrayOffset_9b020000 i32)

(set_global $multipleModuleContructortest3.a (i32.sub (i32.const 0) (i32.const 999)))
)

(start $Application_SubNew)
)