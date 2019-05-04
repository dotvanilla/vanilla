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
    (global $classTest3.circle (mut i32) (i32.const 0))

    ;; Export methods of this module
     



;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (local $newObject_9a020000 i32)

;; Initialize a object instance of [circle]
;; Object memory block begin at location: (get_local $newObject_9a020000)
(set_local $newObject_9a020000 (get_global $global.ObjectManager))
;; set field [testNamespace.circle::x]
(f32.store (i32.add (get_local $newObject_9a020000) (i32.const 0)) (f32.convert_s/i32 (i32.const 1)))
;; set field [testNamespace.circle::y]
(f32.store (i32.add (get_local $newObject_9a020000) (i32.const 4)) (f32.load (i32.add (get_local $newObject_9a020000) (i32.const 0))))
;; set field [testNamespace.circle::z]
(f32.store (i32.add (get_local $newObject_9a020000) (i32.const 8)) (f32.add (f32.load (i32.add (get_local $newObject_9a020000) (i32.const 0))) (f32.load (i32.add (get_local $newObject_9a020000) (i32.const 4)))))
;; set field [testNamespace.circle::radius]
(f64.store (i32.add (get_local $newObject_9a020000) (i32.const 12)) (f64.const 999))
;; Offset object manager with 20 bytes.
(set_global $global.ObjectManager (i32.add (get_local $newObject_9a020000) (i32.const 20)))
;; Initialize an object memory block with 20 bytes data

(set_global $classTest3.circle (get_local $newObject_9a020000))
)

(start $Application_SubNew)

)