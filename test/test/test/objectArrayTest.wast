(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 6/6/2019 11:48:28 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function Math.pow Lib "Math" Alias "pow" (a As f64, b As f64) As f64
    (func $Math.pow (import "Math" "pow") (param $a f64) (param $b f64) (result f64))
;; Declare Function Math.sqrt Lib "Math" Alias "sqrt" (a As f64) As f64
    (func $Math.sqrt (import "Math" "sqrt") (param $a f64) (result f64))
;; Declare Function Math.exp Lib "Math" Alias "exp" (x As f64) As f64
    (func $Math.exp (import "Math" "exp") (param $x f64) (result f64))
;; Declare Function Math.cos Lib "Math" Alias "cos" (x As f64) As f64
    (func $Math.cos (import "Math" "cos") (param $x f64) (result f64))
;; Declare Function Math.random Lib "Math" Alias "random" () As f64
    (func $Math.random (import "Math" "random")  (result f64))
;; Declare Function Math.ceil Lib "Math" Alias "ceil" (x As f64) As f64
    (func $Math.ceil (import "Math" "ceil") (param $x f64) (result f64))
;; Declare Function Math.floor Lib "Math" Alias "floor" (x As f64) As f64
    (func $Math.floor (import "Math" "floor") (param $x f64) (result f64))
;; Declare Function GC.addObject Lib "GC" Alias "addObject" (offset As i32, class_id As i32) As void
    (func $GC.addObject (import "GC" "addObject") (param $offset i32) (param $class_id i32) )
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
    (global $global.ObjectManager (mut i32) (i32.const 368))

    ;; memory allocate in javascript runtime
    (func $global.ObjectManager.Allocate (param $sizeof i32) (param $class_id i32) (result i32)
    ;; Public Function ObjectManager.Allocate(sizeof As i32, class_id As i32) As i32
    
(local $offset i32)

(set_local $offset (get_global $global.ObjectManager))
(set_global $global.ObjectManager (i32.add (get_local $offset) (get_local $sizeof)))
(call $GC.addObject (get_local $offset) (get_local $class_id))
(return (get_local $offset))
)
    (func $global.GetMemorySize  (result i32)
    ;; Public Function GetMemorySize() As i32
    


(return (get_global $global.ObjectManager))
)

    ;; Memory data for string constant
        
    ;; String from 13 with 3 bytes in memory
    (data (i32.const 13) "ABC\00")
    
    ;; String from 352 with 11 bytes in memory
    (data (i32.const 352) "Hello world\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 24 with 320 bytes in memory
    ;;
    ;; class arrayObjects.[24] Point
    ;;
    (data (i32.const 24) "eyJjb21tZW50IjpudWxsLCJjbGFzcyI6IlBvaW50IiwiY2xhc3NfaWQiOjI0LCJmaWVsZHMiOnsieCI6eyJnZW5lcmljIjpbXSwicmF3IjoiZjY0IiwidHlwZSI6NH0sInkiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImY2NCIsInR5cGUiOjR9LCJuYW1lIjp7ImdlbmVyaWMiOltdLCJyYXciOiJzdHJpbmciLCJ0eXBlIjo1fX0sImlzU3RydWN0IjpmYWxzZSwibWV0aG9kcyI6e30sIm5hbWVzcGFjZSI6ImFycmF5T2JqZWN0cyJ9\00")

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
    (global $objectArrayTest.ps (mut i32) (i32.const 0))

    ;; Export methods of this module
    (export "global.GetMemorySize" (func $global.GetMemorySize))

     




    ;; Application Initialize
    ;; 
    ;; Sub New
    (func $Application_SubNew
        ;; call of the global variable initialize
        (call $global.initializer )

        (call $objectArrayTest.constructor )
    )

    ;; Initializer for global variables if it is not a primitive abstract type
    (func $global.initializer  
    ;; Public Function initializer() As void
    
(local $arrayOffset_9c020000 i32)
(local $itemOffset_9d020000 i32)
(local $newObject_9a020000 i32)
(local $newObject_9b020000 i32)


;; Save (i32.const 2) array element data to memory:
;; Array memory block begin at location: (get_local $arrayOffset_9c020000)
(set_local $arrayOffset_9c020000 (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 2) (i32.const 4))) (i32.const 7)))
;; class_id/typealias_enum i32 data: (i32.const 24)/array(Of intptr)
(i32.store (get_local $arrayOffset_9c020000) (i32.const 24))
(i32.store (i32.add (get_local $arrayOffset_9c020000) (i32.const 4)) (i32.const 2))
;; End of byte marks meta data, start write data blocks
(set_local $itemOffset_9d020000 (i32.add (get_local $arrayOffset_9c020000) (i32.const 8)))
(set_local $newObject_9a020000 (call $global.ObjectManager.Allocate (i32.const 20) (i32.const 24)))
;; set field [arrayObjects.Point::x]
(f64.store (i32.add (get_local $newObject_9a020000) (i32.const 0)) (f64.convert_s/i32 (i32.const 100)))
;; set field [arrayObjects.Point::y]
(f64.store (i32.add (get_local $newObject_9a020000) (i32.const 8)) (f64.convert_s/i32 (i32.const 500)))
;; set field [arrayObjects.Point::name]
(i32.store (i32.add (get_local $newObject_9a020000) (i32.const 16)) (i32.const 13))
(i32.store (i32.add (get_local $itemOffset_9d020000) (i32.const 0)) (get_local $newObject_9a020000))
(set_local $newObject_9b020000 (call $global.ObjectManager.Allocate (i32.const 20) (i32.const 24)))
;; set field [arrayObjects.Point::x]
(f64.store (i32.add (get_local $newObject_9b020000) (i32.const 0)) (f64.convert_s/i32 (i32.const 1)))
;; set field [arrayObjects.Point::y]
(f64.store (i32.add (get_local $newObject_9b020000) (i32.const 8)) (f64.mul (f64.load (i32.add (get_local $newObject_9b020000) (i32.const 0))) (f64.convert_s/i32 (i32.const 999))))
;; set field [arrayObjects.Point::name]
(i32.store (i32.add (get_local $newObject_9b020000) (i32.const 16)) (i32.const 352))
(i32.store (i32.add (get_local $itemOffset_9d020000) (i32.const 4)) (get_local $newObject_9b020000))
;; Assign array memory data to another expression
(set_global $objectArrayTest.ps (get_local $arrayOffset_9c020000))
)

    (func $objectArrayTest.constructor  
    ;; Public Function constructor() As void
    



)

    (start $Application_SubNew)
)