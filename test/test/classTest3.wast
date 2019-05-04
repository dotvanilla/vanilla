(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/4/2019 5:24:37 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function array.new Lib "Array" Alias "create" (size As i32) As list
    (func $array.new (import "Array" "create") (param $size i32) (result i32))
;; Declare Function intptr_array.push Lib "Array" Alias "push" (array As list, element As intptr) As list
    (func $intptr_array.push (import "Array" "push") (param $array i32) (param $element i32) (result i32))
;; Declare Function intptr_array.get Lib "Array" Alias "get" (array As list, index As i32) As intptr
    (func $intptr_array.get (import "Array" "get") (param $array i32) (param $index i32) (result i32))
;; Declare Function intptr_array.set Lib "Array" Alias "set" (array As list, index As i32, value As intptr) As void
    (func $intptr_array.set (import "Array" "set") (param $array i32) (param $index i32) (param $value i32) )
;; Declare Function array.length Lib "Array" Alias "length" (array As list) As i32
    (func $array.length (import "Array" "length") (param $array i32) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 247))

    ;; Memory data for string constant
    
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    
    ;; String from 10 with 236 bytes in memory
    (data (i32.const 10) "eyJtZW1vcnlQdHIiOnsiVmFsdWUiOjEwfSwiQ2xhc3MiOiJjaXJjbGUiLCJGaWVsZHMiOnsieCI6eyJnZW5lcmljIjpbXX0sInkiOnsiZ2VuZXJpYyI6W119LCJ6Ijp7ImdlbmVyaWMiOltdfSwicmFkaXVzIjp7ImdlbmVyaWMiOltdfX0sIk1ldGhvZHMiOnt9LCJOYW1lc3BhY2UiOiJ0ZXN0TmFtZXNwYWNlIn0=\00")

    ;; Global variables in this module
    (global $classArrayTest.circles (mut i32) (i32.const 0))
(global $classTest3.circle (mut i32) (i32.const 0))

    ;; Export methods of this module
    ;; export from VB.NET module: [classArrayTest]
    
    (export "classArrayTest.initializeArray" (func $classArrayTest.initializeArray))
    
     

    ;; functions in [classArrayTest]
    
    (func $classArrayTest.initializeArray  
        ;; Public Function initializeArray() As void
        (local $newObject_9a020000 i32)
    (local $c2 i32)
    (local $newObject_9b020000 i32)
    (local $arrayOffset_9c020000 i32)
    
    ;; Initialize a object instance of [[10]circle]
    ;; Object memory block begin at location: (get_local $newObject_9a020000)
    (set_local $newObject_9a020000 (get_global $global.ObjectManager))
    ;; set field [testNamespace.circle::radius]
    (f64.store (i32.add (get_local $newObject_9a020000) (i32.const 12)) (f64.convert_s/i32 (i32.const 100)))
    ;; set field [testNamespace.circle::x]
    (f32.store (i32.add (get_local $newObject_9a020000) (i32.const 0)) (f32.const 0))
    ;; set field [testNamespace.circle::y]
    (f32.store (i32.add (get_local $newObject_9a020000) (i32.const 4)) (f32.const 0))
    ;; set field [testNamespace.circle::z]
    (f32.store (i32.add (get_local $newObject_9a020000) (i32.const 8)) (f32.const 0))
    ;; Offset object manager with 20 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $newObject_9a020000) (i32.const 20)))
    ;; Initialize an object memory block with 20 bytes data
    
    (set_local $c2 (get_local $newObject_9a020000))
    
    ;; Save 2 array element data to memory:
    ;; Array memory block begin at location: (get_global $global.ObjectManager)
    ;; class_id/typealias_enum i32 data: (i32.const 10)/array(Of intptr)
    (i32.store (get_global $global.ObjectManager) (i32.const 10))
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 2))
    ;; End of byte marks meta data, start write data blocks
    (set_local $arrayOffset_9c020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    (i32.store (i32.add (get_local $arrayOffset_9c020000) (i32.const 0)) (get_local $newObject_9b020000))
    (i32.store (i32.add (get_local $arrayOffset_9c020000) (i32.const 4)) (get_local $c2))
    ;; Offset object manager with 16 bytes
    (set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9c020000) (i32.const -8)) (i32.const 16)))
    ;; Assign array memory data to another expression
    (set_global $classArrayTest.circles (i32.add (get_local $arrayOffset_9c020000) (i32.const -8)))
    )
    

;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (local $newObject_9d020000 i32)

;; Initialize a object instance of [[10]circle]
;; Object memory block begin at location: (get_local $newObject_9d020000)
(set_local $newObject_9d020000 (get_global $global.ObjectManager))
;; set field [testNamespace.circle::x]
(f32.store (i32.add (get_local $newObject_9d020000) (i32.const 0)) (f32.convert_s/i32 (i32.const 1)))
;; set field [testNamespace.circle::y]
(f32.store (i32.add (get_local $newObject_9d020000) (i32.const 4)) (f32.load (i32.add (get_local $newObject_9d020000) (i32.const 0))))
;; set field [testNamespace.circle::z]
(f32.store (i32.add (get_local $newObject_9d020000) (i32.const 8)) (f32.add (f32.load (i32.add (get_local $newObject_9d020000) (i32.const 0))) (f32.load (i32.add (get_local $newObject_9d020000) (i32.const 4)))))
;; set field [testNamespace.circle::radius]
(f64.store (i32.add (get_local $newObject_9d020000) (i32.const 12)) (f64.const 999))
;; Offset object manager with 20 bytes.
(set_global $global.ObjectManager (i32.add (get_local $newObject_9d020000) (i32.const 20)))
;; Initialize an object memory block with 20 bytes data

(set_global $classTest3.circle (get_local $newObject_9d020000))
)

(start $Application_SubNew)

)