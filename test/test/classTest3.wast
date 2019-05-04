(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/4/2019 3:10:11 PM
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
    (global $global.ObjectManager (mut i32) (i32.const 238))

    ;; Memory data for string constant
    
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    
    ;; String from 1 with 236 bytes in memory
    (data (i32.const 1) "eyJtZW1vcnlQdHIiOnsiVmFsdWUiOjF9LCJDbGFzcyI6ImNpcmNsZSIsIkZpZWxkcyI6eyJ4Ijp7ImdlbmVyaWMiOltdfSwieSI6eyJnZW5lcmljIjpbXX0sInoiOnsiZ2VuZXJpYyI6W119LCJyYWRpdXMiOnsiZ2VuZXJpYyI6W119fSwiTWV0aG9kcyI6e30sIk5hbWVzcGFjZSI6InRlc3ROYW1lc3BhY2UifQ==\00")

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
    (set_global $classArrayTest.circles (call $intptr_array.push (call $array.new (i32.const -1)) (get_local $newObject_9a020000)))
    )
    

;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (local $newObject_9b020000 i32)

;; Initialize a object instance of [circle]
;; Object memory block begin at location: (get_local $newObject_9b020000)
(set_local $newObject_9b020000 (get_global $global.ObjectManager))
;; set field [testNamespace.circle::x]
(f32.store (i32.add (get_local $newObject_9b020000) (i32.const 0)) (f32.convert_s/i32 (i32.const 1)))
;; set field [testNamespace.circle::y]
(f32.store (i32.add (get_local $newObject_9b020000) (i32.const 4)) (f32.load (i32.add (get_local $newObject_9b020000) (i32.const 0))))
;; set field [testNamespace.circle::z]
(f32.store (i32.add (get_local $newObject_9b020000) (i32.const 8)) (f32.add (f32.load (i32.add (get_local $newObject_9b020000) (i32.const 0))) (f32.load (i32.add (get_local $newObject_9b020000) (i32.const 4)))))
;; set field [testNamespace.circle::radius]
(f64.store (i32.add (get_local $newObject_9b020000) (i32.const 12)) (f64.const 999))
;; Offset object manager with 20 bytes.
(set_global $global.ObjectManager (i32.add (get_local $newObject_9b020000) (i32.const 20)))
;; Initialize an object memory block with 20 bytes data

(set_global $classTest3.circle (get_local $newObject_9b020000))
)

(start $Application_SubNew)

)