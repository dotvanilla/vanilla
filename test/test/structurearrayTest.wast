(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/7/2019 9:42:36 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 355))

    ;; Memory data for string constant
    
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 10 with 344 bytes in memory
    ;;
    ;; structure structureArrayElement.[10] circle
    ;;
    (data (i32.const 10) "eyJtZW1vcnlQdHIiOnsiVmFsdWUiOjEwfSwiY2xhc3MiOiJjaXJjbGUiLCJjbGFzc19pZCI6MTAsImZpZWxkcyI6eyJ4Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJpMzIiLCJ0eXBlIjoxfSwieSI6eyJnZW5lcmljIjpbXSwicmF3IjoiaTMyIiwidHlwZSI6MX0sInJhZGl1cyI6eyJnZW5lcmljIjpbXSwicmF3IjoiZjY0IiwidHlwZSI6NH19LCJpc1N0cnVjdCI6dHJ1ZSwibWV0aG9kcyI6e30sIm5hbWVzcGFjZSI6InN0cnVjdHVyZUFycmF5RWxlbWVudCJ9\00")

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
    (local $a i32)
    
    
    ;; Save 2 array element data to memory:
    ;; Array memory block begin at location: (get_global $global.ObjectManager)
    ;; class_id/typealias_enum i32 data: (i32.const 10)/array(Of intptr)
    (i32.store (get_global $global.ObjectManager) (i32.const 10))
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 2))
    ;; End of byte marks meta data, start write data blocks
    (set_local $arrayOffset_9c020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    (set_local $newObject_9a020000 (get_global $global.ObjectManager))
    ;; set field [structureArrayElement.circle::radius]
    (f64.store (i32.add (get_local $newObject_9a020000) (i32.const 8)) (f64.convert_s/i32 (i32.const 100)))
    ;; set field [structureArrayElement.circle::x]
    (i32.store (i32.add (get_local $newObject_9a020000) (i32.const 0)) (i32.const 0))
    ;; set field [structureArrayElement.circle::y]
    (i32.store (i32.add (get_local $newObject_9a020000) (i32.const 4)) (i32.const 0))
    ;; Offset object manager with 16 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $newObject_9a020000) (i32.const 16)))
    (i32.store (i32.add (get_local $arrayOffset_9c020000) (i32.const 0)) (get_local $newObject_9a020000))
    (set_local $newObject_9b020000 (get_global $global.ObjectManager))
    ;; set field [structureArrayElement.circle::x]
    (i32.store (i32.add (get_local $newObject_9b020000) (i32.const 0)) (i32.const 1))
    ;; set field [structureArrayElement.circle::y]
    (i32.store (i32.add (get_local $newObject_9b020000) (i32.const 4)) (i32.load (i32.add (get_local $newObject_9b020000) (i32.const 0))))
    ;; set field [structureArrayElement.circle::radius]
    (f64.store (i32.add (get_local $newObject_9b020000) (i32.const 8)) (f64.convert_s/i32 (i32.const 999)))
    ;; Offset object manager with 16 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $newObject_9b020000) (i32.const 16)))
    (i32.store (i32.add (get_local $arrayOffset_9c020000) (i32.const 4)) (get_local $newObject_9b020000))
    ;; Offset object manager with 16 bytes
    (set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9c020000) (i32.const -8)) (i32.const 16)))
    ;; Assign array memory data to another expression
    (set_local $a (i32.add (get_local $arrayOffset_9c020000) (i32.const -8)))
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