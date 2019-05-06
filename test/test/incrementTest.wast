(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/6/2019 8:37:27 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 10))

    ;; Memory data for string constant
    
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    

    ;; Global variables in this module
    

    ;; Export methods of this module
    ;; export from VB.NET module: [incrementTest]
    
    (export "incrementTest.runAdd" (func $incrementTest.runAdd))
    (export "incrementTest.show" (func $incrementTest.show))
    
     

    ;; functions in [incrementTest]
    
    (func $incrementTest.runAdd  (result f64)
        ;; Public Function runAdd() As f64
        
    (local $i i32)
    (local $x i32)
    
    (set_local $i (i32.const 999))
    (drop (call $incrementTest.show (i32.add (i32.const 0) (i32.add (i32.const 0) (get_local $i)))))
    (set_local $x (i32.add (i32.const 0) (i32.add (i32.const 0) (get_local $i))))
    (return (f64.convert_s/i32 (get_local $i)))
    )
    (func $incrementTest.show (param $x i32) (result i32)
        ;; Public Function show(x As i32) As i32
        
    
    
    (return (i32.const 0))
    )
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $incrementTest.constructor )
)

(func $incrementTest.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)