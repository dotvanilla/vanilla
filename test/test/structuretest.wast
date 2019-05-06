(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/6/2019 8:03:18 PM
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
    (global $global.ObjectManager (mut i32) (i32.const 451))

    ;; Memory data for string constant
    
    ;; String from 423 with 5 bytes in memory
    (data (i32.const 423) "99999\00")

    ;; String from 429 with 1 bytes in memory
    (data (i32.const 429) "A\00")

    ;; String from 431 with 4 bytes in memory
    (data (i32.const 431) "blue\00")

    ;; String from 436 with 4 bytes in memory
    (data (i32.const 436) "blue\00")

    ;; String from 441 with 4 bytes in memory
    (data (i32.const 441) "blue\00")

    ;; String from 446 with 4 bytes in memory
    (data (i32.const 446) "blue\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    
    ;; String from 10 with 412 bytes in memory
    (data (i32.const 10) "eyJtZW1vcnlQdHIiOnsiVmFsdWUiOjEwfSwiY2xhc3MiOiJjaXJjbGUiLCJjbGFzc19pZCI6MTAsImZpZWxkcyI6eyJ4Ijp7ImdlbmVyaWMiOm51bGwsInJhdyI6IlNpbmdsZSIsInR5cGUiOjN9LCJ5Ijp7ImdlbmVyaWMiOm51bGwsInJhdyI6IlNpbmdsZSIsInR5cGUiOjN9LCJyYWRpdXMiOnsiZ2VuZXJpYyI6bnVsbCwicmF3IjoiU2luZ2xlIiwidHlwZSI6M30sImlkIjp7ImdlbmVyaWMiOltdLCJyYXciOiJzdHJpbmciLCJ0eXBlIjo1fX0sImlzU3RydWN0Ijp0cnVlLCJtZXRob2RzIjp7fSwibmFtZXNwYWNlIjoic3RydWN0dXJldGVzdCJ9\00")

    ;; Global variables in this module
    

    ;; Export methods of this module
     

    ;; functions in [testStrucutre]
    
    (func $testStrucutre.createValue  (result i32)
        ;; Public Function createValue() As intptr
        (local $newObject_9a020000 i32)
    
    ;; Initialize a object instance of [[10]circle]
    ;; Object memory block begin at location: (get_local $newObject_9a020000)
    (set_local $newObject_9a020000 (get_global $global.ObjectManager))
    ;; set field [structuretest.circle::id]
    (i32.store (i32.add (get_local $newObject_9a020000) (i32.const 12)) (i32.const 423))
    ;; set field [structuretest.circle::x]
    (f32.store (i32.add (get_local $newObject_9a020000) (i32.const 0)) (f32.const 0))
    ;; set field [structuretest.circle::y]
    (f32.store (i32.add (get_local $newObject_9a020000) (i32.const 4)) (f32.const 0))
    ;; set field [structuretest.circle::radius]
    (f32.store (i32.add (get_local $newObject_9a020000) (i32.const 8)) (f32.const 0))
    ;; Offset object manager with 16 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $newObject_9a020000) (i32.const 16)))
    ;; Initialize an object memory block with 16 bytes data
    
    (return (get_local $newObject_9a020000))
    )
    

;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (local $newObject_9b020000 i32)
(local $circle i32)
(local $copy i32)
(local $arrayOffset_9c020000 i32)
(local $arrayTest i32)
(local $a i32)
(local $b i32)

;; Initialize a object instance of [[10]circle]
;; Object memory block begin at location: (get_local $newObject_9b020000)
(set_local $newObject_9b020000 (get_global $global.ObjectManager))
;; set field [structuretest.circle::id]
(i32.store (i32.add (get_local $newObject_9b020000) (i32.const 12)) (i32.const 429))
;; set field [structuretest.circle::x]
(f32.store (i32.add (get_local $newObject_9b020000) (i32.const 0)) (f32.convert_s/i32 (i32.const 1)))
;; set field [structuretest.circle::y]
(f32.store (i32.add (get_local $newObject_9b020000) (i32.const 4)) (f32.convert_s/i32 (i32.const 2)))
;; set field [structuretest.circle::radius]
(f32.store (i32.add (get_local $newObject_9b020000) (i32.const 8)) (f32.const 0))
;; Offset object manager with 16 bytes.
(set_global $global.ObjectManager (i32.add (get_local $newObject_9b020000) (i32.const 16)))
;; Initialize an object memory block with 16 bytes data

(set_local $circle (get_local $newObject_9b020000))
(set_local $copy (get_local $circle))
(f32.store (i32.add (get_local $copy) (i32.const 4)) (f32.convert_s/i32 (i32.const 100)))
(f32.store (i32.add (get_local $circle) (i32.const 4)) (f32.convert_s/i32 (i32.const 500)))
(call $testStrucutre.print (call $f32.toString (f32.load (i32.add (get_local $copy) (i32.const 4)))) (i32.const 431))
(call $testStrucutre.print (call $f32.toString (f32.load (i32.add (get_local $circle) (i32.const 4)))) (i32.const 436))

;; Save 3 array element data to memory:
;; Array memory block begin at location: (get_global $global.ObjectManager)
;; class_id/typealias_enum i32 data: (i32.const 10)/array(Of intptr)
(i32.store (get_global $global.ObjectManager) (i32.const 10))
(i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 3))
;; End of byte marks meta data, start write data blocks
(set_local $arrayOffset_9c020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
(i32.store (i32.add (get_local $arrayOffset_9c020000) (i32.const 0)) (get_local $copy))
(i32.store (i32.add (get_local $arrayOffset_9c020000) (i32.const 4)) (get_local $circle))
(i32.store (i32.add (get_local $arrayOffset_9c020000) (i32.const 8)) (call $testStrucutre.createValue ))
;; Offset object manager with 20 bytes
(set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9c020000) (i32.const -8)) (i32.const 20)))
;; Assign array memory data to another expression
(set_local $arrayTest (i32.add (get_local $arrayOffset_9c020000) (i32.const -8)))
(set_local $a (i32.load (i32.add (i32.add (get_local $arrayTest) (i32.const 8)) (i32.mul (i32.const 0) (i32.const 4)))))
(set_local $b (i32.load (i32.add (i32.add (get_local $arrayTest) (i32.const 8)) (i32.mul (i32.const 1) (i32.const 4)))))
(f32.store (i32.add (i32.add (i32.add (get_local $arrayTest) (i32.const 8)) (i32.mul (i32.const 0) (i32.const 16))) (i32.const 8)) (f32.convert_s/i32 (i32.sub (i32.const 0) (i32.const 100))))
(call $testStrucutre.print (call $f32.toString (f32.load (i32.add (i32.load (i32.add (i32.add (get_local $arrayTest) (i32.const 8)) (i32.mul (i32.const 0) (i32.const 4)))) (i32.const 8)))) (i32.const 441))
(call $testStrucutre.print (call $f32.toString (f32.load (i32.add (get_local $a) (i32.const 8)))) (i32.const 446))
)

(start $Application_SubNew)

)