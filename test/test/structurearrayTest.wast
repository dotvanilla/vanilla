(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/8/2019 7:56:31 PM
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
    (global $global.ObjectManager (mut i32) (i32.const 825))

    ;; Memory data for string constant
        
    ;; String from 11 with 3 bytes in memory
    (data (i32.const 11) "red\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 15 with 460 bytes in memory
    ;;
    ;; class structureArrayElement.[15] rectangle
    ;;
    (data (i32.const 15) "eyJtZW1vcnlQdHIiOnsiVmFsdWUiOjE1fSwiY2xhc3MiOiJyZWN0YW5nbGUiLCJjbGFzc19pZCI6MTUsImZpZWxkcyI6eyJ4Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJpMzIiLCJ0eXBlIjoxfSwieSI6eyJnZW5lcmljIjpbXSwicmF3IjoiaTMyIiwidHlwZSI6MX0sInciOnsiZ2VuZXJpYyI6W10sInJhdyI6ImkzMiIsInR5cGUiOjF9LCJoIjp7ImdlbmVyaWMiOltdLCJyYXciOiJpMzIiLCJ0eXBlIjoxfSwiZmlsbCI6eyJnZW5lcmljIjpbXSwicmF3Ijoic3RyaW5nIiwidHlwZSI6NX19LCJpc1N0cnVjdCI6ZmFsc2UsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOiJzdHJ1Y3R1cmVBcnJheUVsZW1lbnQifQ==\00")
    
    ;; String from 476 with 348 bytes in memory
    ;;
    ;; structure structureArrayElement.[476] circle
    ;;
    (data (i32.const 476) "eyJtZW1vcnlQdHIiOnsiVmFsdWUiOjQ3Nn0sImNsYXNzIjoiY2lyY2xlIiwiY2xhc3NfaWQiOjQ3NiwiZmllbGRzIjp7IngiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImkzMiIsInR5cGUiOjF9LCJ5Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJpMzIiLCJ0eXBlIjoxfSwicmFkaXVzIjp7ImdlbmVyaWMiOltdLCJyYXciOiJmNjQiLCJ0eXBlIjo0fX0sImlzU3RydWN0Ijp0cnVlLCJtZXRob2RzIjp7fSwibmFtZXNwYWNlIjoic3RydWN0dXJlQXJyYXlFbGVtZW50In0=\00")

    ;; Global variables in this module
    

    ;; Export methods of this module
    ;; export from VB.NET module: [structurearrayTest]
    
    (export "structurearrayTest.createarray" (func $structurearrayTest.createarray))
    
     

    ;; functions in [structurearrayTest]
    
    (func $structurearrayTest.createarray  
        ;; Public Function createarray() As void
        
    (local $newObject_9a020000 i32)
    (local $newObject_9b020000 i32)
    (local $arrayOffset_9c020000 i32)
    (local $structCopyOf_9d020000 i32)
    (local $structCopyOf_9e020000 i32)
    (local $a i32)
    (local $newObject_9f020000 i32)
    (local $arrayOffset_a0020000 i32)
    (local $b i32)
    
    
    ;; Save 2 array element data to memory:
    ;; Array memory block begin at location: (get_global $global.ObjectManager)
    ;; class_id/typealias_enum i32 data: (i32.const 476)/array(Of intptr)
    (i32.store (get_global $global.ObjectManager) (i32.const 476))
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 2))
    ;; End of byte marks meta data, start write data blocks
    (set_local $arrayOffset_9c020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    (set_local $structCopyOf_9d020000 (i32.add (get_local $arrayOffset_9c020000) (i32.const 0)))
    (set_local $structCopyOf_9d020000 (get_global $global.ObjectManager))
    ;; set field [structureArrayElement.circle::x]
    (i32.store (i32.add (get_local $structCopyOf_9d020000) (i32.const 0)) (i32.load (i32.add (get_local $newObject_9a020000) (i32.const 0))))
    ;; set field [structureArrayElement.circle::y]
    (i32.store (i32.add (get_local $structCopyOf_9d020000) (i32.const 4)) (i32.load (i32.add (get_local $newObject_9a020000) (i32.const 4))))
    ;; set field [structureArrayElement.circle::radius]
    (f64.store (i32.add (get_local $structCopyOf_9d020000) (i32.const 8)) (f64.load (i32.add (get_local $newObject_9a020000) (i32.const 8))))
    ;; Offset object manager with 16 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $structCopyOf_9d020000) (i32.const 16)))
    (set_local $structCopyOf_9e020000 (i32.add (get_local $arrayOffset_9c020000) (i32.const 16)))
    (set_local $structCopyOf_9e020000 (get_global $global.ObjectManager))
    ;; set field [structureArrayElement.circle::x]
    (i32.store (i32.add (get_local $structCopyOf_9e020000) (i32.const 0)) (i32.load (i32.add (get_local $newObject_9b020000) (i32.const 0))))
    ;; set field [structureArrayElement.circle::y]
    (i32.store (i32.add (get_local $structCopyOf_9e020000) (i32.const 4)) (i32.load (i32.add (get_local $newObject_9b020000) (i32.const 4))))
    ;; set field [structureArrayElement.circle::radius]
    (f64.store (i32.add (get_local $structCopyOf_9e020000) (i32.const 8)) (f64.load (i32.add (get_local $newObject_9b020000) (i32.const 8))))
    ;; Offset object manager with 16 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $structCopyOf_9e020000) (i32.const 16)))
    ;; Offset object manager with 40 bytes
    (set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9c020000) (i32.const -8)) (i32.const 40)))
    ;; Assign array memory data to another expression
    (set_local $a (i32.add (get_local $arrayOffset_9c020000) (i32.const -8)))
    
    ;; Save 1 array element data to memory:
    ;; Array memory block begin at location: (get_global $global.ObjectManager)
    ;; class_id/typealias_enum i32 data: (i32.const 15)/array(Of intptr)
    (i32.store (get_global $global.ObjectManager) (i32.const 15))
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 1))
    ;; End of byte marks meta data, start write data blocks
    (set_local $arrayOffset_a0020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    (set_local $newObject_9f020000 (get_global $global.ObjectManager))
    ;; set field [structureArrayElement.rectangle::x]
    (i32.store (i32.add (get_local $newObject_9f020000) (i32.const 0)) (i32.const 1))
    ;; set field [structureArrayElement.rectangle::y]
    (i32.store (i32.add (get_local $newObject_9f020000) (i32.const 4)) (i32.const 1))
    ;; set field [structureArrayElement.rectangle::w]
    (i32.store (i32.add (get_local $newObject_9f020000) (i32.const 8)) (i32.const 0))
    ;; set field [structureArrayElement.rectangle::h]
    (i32.store (i32.add (get_local $newObject_9f020000) (i32.const 12)) (i32.const 0))
    ;; set field [structureArrayElement.rectangle::fill]
    (i32.store (i32.add (get_local $newObject_9f020000) (i32.const 16)) (i32.const 11))
    ;; Offset object manager with 20 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $newObject_9f020000) (i32.const 20)))
    (i32.store (i32.add (get_local $arrayOffset_a0020000) (i32.const 0)) (get_local $newObject_9f020000))
    ;; Offset object manager with 12 bytes
    (set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_a0020000) (i32.const -8)) (i32.const 12)))
    ;; Assign array memory data to another expression
    (set_local $b (i32.add (get_local $arrayOffset_a0020000) (i32.const -8)))
    )
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $structurearrayTest.constructor )
)

(func $structurearrayTest.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)