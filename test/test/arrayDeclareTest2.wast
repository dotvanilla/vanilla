(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/9/2019 10:47:01 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 11))

    ;; Memory data for string constant
    
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    

    ;; Global variables in this module
    (global $arrayDeclareTest2.len (mut i32) (i32.const 100))

    ;; Export methods of this module
    ;; export from VB.NET module: [arrayDeclareTest2]
    
    (export "arrayDeclareTest2.Main" (func $arrayDeclareTest2.Main))
    
     

    ;; functions in [arrayDeclareTest2]
    
    (func $arrayDeclareTest2.Main  
        ;; Public Function Main() As void
        
    (local $a i32)
    (local $b i32)
    
    (set_local $a (call $array.new (i32.sub (get_global $arrayDeclareTest2.len) (i32.const 1))))
    (set_local $b (call $array.new (i32.sub (get_global $arrayDeclareTest2.len) (i32.const 1))))
    (set_local $a (call $array.new (i32.const 99)))
    )
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $arrayDeclareTest2.constructor )
)

(func $arrayDeclareTest2.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)