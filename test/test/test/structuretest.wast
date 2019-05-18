(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/18/2019 2:22:27 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function print Lib "console" Alias "log" (data As string, color As string) As void
    (func $testStrucutre.print (import "console" "log") (param $data i32) (param $color i32) )
;; Declare Function string.replace Lib "string" Alias "replace" (input As string, find As intptr, replacement As string) As string
    (func $string.replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
;; Declare Function string.add Lib "string" Alias "add" (a As string, b As string) As string
    (func $string.add (import "string" "add") (param $a i32) (param $b i32) (result i32))
;; Declare Function string.length Lib "string" Alias "length" (text As string) As i32
    (func $string.length (import "string" "length") (param $text i32) (result i32))
;; Declare Function string.indexOf Lib "string" Alias "indexOf" (input As string, find As string) As i32
    (func $string.indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
;; Declare Function f32.toString Lib "string" Alias "toString" (x As f32) As string
    (func $f32.toString (import "string" "toString") (param $x f32) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 472))

    ;; Memory data for string constant
        
    ;; String from 444 with 5 bytes in memory
    (data (i32.const 444) "99999\00")
    
    ;; String from 450 with 1 bytes in memory
    (data (i32.const 450) "A\00")
    
    ;; String from 452 with 4 bytes in memory
    (data (i32.const 452) "blue\00")
    
    ;; String from 457 with 4 bytes in memory
    (data (i32.const 457) "blue\00")
    
    ;; String from 462 with 4 bytes in memory
    (data (i32.const 462) "blue\00")
    
    ;; String from 467 with 4 bytes in memory
    (data (i32.const 467) "blue\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 11 with 432 bytes in memory
    ;;
    ;; structure structuretest.[11] circle
    ;;
    (data (i32.const 11) "eyJjb21tZW50IjpudWxsLCJtZW1vcnlQdHIiOnsiVmFsdWUiOjExfSwiY2xhc3MiOiJjaXJjbGUiLCJjbGFzc19pZCI6MTEsImZpZWxkcyI6eyJ4Ijp7ImdlbmVyaWMiOm51bGwsInJhdyI6IlNpbmdsZSIsInR5cGUiOjN9LCJ5Ijp7ImdlbmVyaWMiOm51bGwsInJhdyI6IlNpbmdsZSIsInR5cGUiOjN9LCJyYWRpdXMiOnsiZ2VuZXJpYyI6bnVsbCwicmF3IjoiU2luZ2xlIiwidHlwZSI6M30sImlkIjp7ImdlbmVyaWMiOltdLCJyYXciOiJzdHJpbmciLCJ0eXBlIjo1fX0sImlzU3RydWN0Ijp0cnVlLCJtZXRob2RzIjp7fSwibmFtZXNwYWNlIjoic3RydWN0dXJldGVzdCJ9\00")

    ;; Global variables in this module
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
(global $circle.INF (mut i32) (i32.const 2147483647))

    ;; Export methods of this module
     

    ;; functions in [testStrucutre]
    
    (func $testStrucutre.createValue  (result i32)
        ;; Public Function createValue() As intptr
        
    (local $newObject_9a020000 i32)
    
    
    ;; Initialize a object instance of [[11]circle]
    ;; Object memory block begin at location: (get_local $newObject_9a020000)
    (set_local $newObject_9a020000 (get_global $global.ObjectManager))
    ;; Offset object manager with 16 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $newObject_9a020000) (i32.const 16)))
    ;; set field [structuretest.circle::id]
    (i32.store (i32.add (get_local $newObject_9a020000) (i32.const 12)) (i32.const 444))
    ;; set field [structuretest.circle::x]
    (f32.store (i32.add (get_local $newObject_9a020000) (i32.const 0)) (f32.const 0))
    ;; set field [structuretest.circle::y]
    (f32.store (i32.add (get_local $newObject_9a020000) (i32.const 4)) (f32.const 0))
    ;; set field [structuretest.circle::radius]
    (f32.store (i32.add (get_local $newObject_9a020000) (i32.const 8)) (f32.const 0))
    ;; Initialize an object memory block with 16 bytes data
    
    (return (get_local $newObject_9a020000))
    )
    
    
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $testStrucutre.constructor )
)

(func $testStrucutre.constructor  
    ;; Public Function constructor() As void
    
(local $newObject_9b020000 i32)
(local $circle i32)
(local $newObject_9c020000 i32)
(local $copy i32)
(local $newObject_9d020000 i32)
(local $newObject_9e020000 i32)
(local $arrayOffset_9f020000 i32)
(local $structCopyOf_a0020000 i32)
(local $structCopyOf_a1020000 i32)
(local $structCopyOf_a2020000 i32)
(local $tempOfStructFunc_a3020000 i32)
(local $arrayTest i32)
(local $a i32)
(local $b i32)


;; Initialize a object instance of [[11]circle]
;; Object memory block begin at location: (get_local $newObject_9b020000)
(set_local $newObject_9b020000 (get_global $global.ObjectManager))
;; Offset object manager with 16 bytes.
(set_global $global.ObjectManager (i32.add (get_local $newObject_9b020000) (i32.const 16)))
;; set field [structuretest.circle::id]
(i32.store (i32.add (get_local $newObject_9b020000) (i32.const 12)) (i32.const 450))
;; set field [structuretest.circle::x]
(f32.store (i32.add (get_local $newObject_9b020000) (i32.const 0)) (f32.convert_s/i32 (i32.const 1)))
;; set field [structuretest.circle::y]
(f32.store (i32.add (get_local $newObject_9b020000) (i32.const 4)) (f32.convert_s/i32 (i32.const 2)))
;; set field [structuretest.circle::radius]
(f32.store (i32.add (get_local $newObject_9b020000) (i32.const 8)) (f32.convert_s/i32 (get_global $circle.INF)))
;; Initialize an object memory block with 16 bytes data

(set_local $circle (get_local $newObject_9b020000))

;; Initialize a object instance of [[11]circle]
;; Object memory block begin at location: (get_local $newObject_9c020000)
(set_local $newObject_9c020000 (get_global $global.ObjectManager))
;; Offset object manager with 16 bytes.
(set_global $global.ObjectManager (i32.add (get_local $newObject_9c020000) (i32.const 16)))
;; set field [structuretest.circle::x]
(f32.store (i32.add (get_local $newObject_9c020000) (i32.const 0)) (f32.load (i32.add (get_local $circle) (i32.const 0))))
;; set field [structuretest.circle::y]
(f32.store (i32.add (get_local $newObject_9c020000) (i32.const 4)) (f32.load (i32.add (get_local $circle) (i32.const 4))))
;; set field [structuretest.circle::radius]
(f32.store (i32.add (get_local $newObject_9c020000) (i32.const 8)) (f32.load (i32.add (get_local $circle) (i32.const 8))))
;; set field [structuretest.circle::id]
(i32.store (i32.add (get_local $newObject_9c020000) (i32.const 12)) (i32.load (i32.add (get_local $circle) (i32.const 12))))
;; Initialize an object memory block with 16 bytes data

(set_local $copy (get_local $newObject_9c020000))
(f32.store (i32.add (get_local $copy) (i32.const 4)) (f32.convert_s/i32 (i32.const 100)))
(f32.store (i32.add (get_local $circle) (i32.const 4)) (f32.convert_s/i32 (i32.const 500)))
(call $testStrucutre.print (call $f32.toString (f32.load (i32.add (get_local $copy) (i32.const 4)))) (i32.const 452))
(call $testStrucutre.print (call $f32.toString (f32.load (i32.add (get_local $circle) (i32.const 4)))) (i32.const 457))

;; Save (i32.const 3) array element data to memory:
;; Array memory block begin at location: (get_global $global.ObjectManager)
;; class_id/typealias_enum i32 data: (i32.const 11)/array(Of intptr)
(i32.store (get_global $global.ObjectManager) (i32.const 11))
(i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 3))
;; End of byte marks meta data, start write data blocks
;; Offset object manager with (i32.add (i32.const 8) (i32.mul (i32.const 3) (i32.const 16))) bytes
(set_local $arrayOffset_9f020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
(set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9f020000) (i32.const -8)) (i32.add (i32.const 8) (i32.mul (i32.const 3) (i32.const 16)))))
(set_local $structCopyOf_a0020000 (i32.add (get_local $arrayOffset_9f020000) (i32.const 0)))
(set_local $newObject_9d020000 (get_local $structCopyOf_a0020000))
;; Offset object manager with 16 bytes.
(set_global $global.ObjectManager (i32.add (get_local $newObject_9d020000) (i32.const 16)))
;; set field [structuretest.circle::x]
(f32.store (i32.add (get_local $newObject_9d020000) (i32.const 0)) (f32.load (i32.add (get_local $copy) (i32.const 0))))
;; set field [structuretest.circle::y]
(f32.store (i32.add (get_local $newObject_9d020000) (i32.const 4)) (f32.load (i32.add (get_local $copy) (i32.const 4))))
;; set field [structuretest.circle::radius]
(f32.store (i32.add (get_local $newObject_9d020000) (i32.const 8)) (f32.load (i32.add (get_local $copy) (i32.const 8))))
;; set field [structuretest.circle::id]
(i32.store (i32.add (get_local $newObject_9d020000) (i32.const 12)) (i32.load (i32.add (get_local $copy) (i32.const 12))))
(set_local $structCopyOf_a1020000 (i32.add (get_local $arrayOffset_9f020000) (i32.const 16)))
(set_local $newObject_9e020000 (get_local $structCopyOf_a1020000))
;; Offset object manager with 16 bytes.
(set_global $global.ObjectManager (i32.add (get_local $newObject_9e020000) (i32.const 16)))
;; set field [structuretest.circle::x]
(f32.store (i32.add (get_local $newObject_9e020000) (i32.const 0)) (f32.load (i32.add (get_local $circle) (i32.const 0))))
;; set field [structuretest.circle::y]
(f32.store (i32.add (get_local $newObject_9e020000) (i32.const 4)) (f32.load (i32.add (get_local $circle) (i32.const 4))))
;; set field [structuretest.circle::radius]
(f32.store (i32.add (get_local $newObject_9e020000) (i32.const 8)) (f32.load (i32.add (get_local $circle) (i32.const 8))))
;; set field [structuretest.circle::id]
(i32.store (i32.add (get_local $newObject_9e020000) (i32.const 12)) (i32.load (i32.add (get_local $circle) (i32.const 12))))
(set_local $structCopyOf_a2020000 (i32.add (get_local $arrayOffset_9f020000) (i32.const 32)))
;; Offset object manager with 16 bytes.
(set_global $global.ObjectManager (i32.add (get_local $structCopyOf_a2020000) (i32.const 16)))
(set_local $tempOfStructFunc_a3020000 (call $testStrucutre.createValue ))
;; set field [structuretest.circle::x]
(f32.store (i32.add (get_local $structCopyOf_a2020000) (i32.const 0)) (f32.load (i32.add (get_local $tempOfStructFunc_a3020000) (i32.const 0))))
;; set field [structuretest.circle::y]
(f32.store (i32.add (get_local $structCopyOf_a2020000) (i32.const 4)) (f32.load (i32.add (get_local $tempOfStructFunc_a3020000) (i32.const 4))))
;; set field [structuretest.circle::radius]
(f32.store (i32.add (get_local $structCopyOf_a2020000) (i32.const 8)) (f32.load (i32.add (get_local $tempOfStructFunc_a3020000) (i32.const 8))))
;; set field [structuretest.circle::id]
(i32.store (i32.add (get_local $structCopyOf_a2020000) (i32.const 12)) (i32.load (i32.add (get_local $tempOfStructFunc_a3020000) (i32.const 12))))
;; Assign array memory data to another expression
(set_local $arrayTest (i32.add (get_local $arrayOffset_9f020000) (i32.const -8)))
(set_local $a (i32.load (i32.add (i32.add (get_local $arrayTest) (i32.const 8)) (i32.mul (i32.const 0) (i32.const 16)))))
(set_local $b (i32.load (i32.add (i32.add (get_local $arrayTest) (i32.const 8)) (i32.mul (i32.const 1) (i32.const 16)))))
(f32.store (i32.add (i32.add (i32.add (get_local $arrayTest) (i32.const 8)) (i32.mul (i32.const 0) (i32.const 16))) (i32.const 8)) (f32.convert_s/i32 (i32.sub (i32.const 0) (i32.const 100))))
(call $testStrucutre.print (call $f32.toString (f32.load (i32.add (i32.load (i32.add (i32.add (get_local $arrayTest) (i32.const 8)) (i32.mul (i32.const 0) (i32.const 16)))) (i32.const 8)))) (i32.const 462))
(call $testStrucutre.print (call $f32.toString (f32.load (i32.add (get_local $a) (i32.const 8)))) (i32.const 467))
)

(start $Application_SubNew)
)