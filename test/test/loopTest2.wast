(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/11/2019 10:25:54 AM
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
    

    ;; Export methods of this module
    ;; export from VB.NET module: [loopTest2]
    
    (export "loopTest2.Main" (func $loopTest2.Main))
    
     

    ;; functions in [loopTest2]
    
    (func $loopTest2.Main  
        ;; Public Function Main() As void
        
    
    
    ;; Do ... Loop
    ;; Start Do While Block block_9a020000
    
    (block $block_9a020000 
        (loop $loop_9b020000
    
                    (drop (call $loopTest2.doNothing ))
            (br $loop_9b020000)
    
        )
    )
    ;; End Loop loop_9b020000
    )
    
    
    (func $loopTest2.doNothing  (result i32)
        ;; Public Function doNothing() As i32
        
    
    
    (return (i32.trunc_s/f64 (f64.const 8.9)))
    )
    
    
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $loopTest2.constructor )
)

(func $loopTest2.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)