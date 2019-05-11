(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/11/2019 1:13:20 AM
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
    ;; export from VB.NET module: [loopTest1]
    
    (export "loopTest1.Main" (func $loopTest1.Main))
    
     

    ;; functions in [loopTest1]
    
    (func $loopTest1.Main  
        ;; Public Function Main() As void
        
    
    
    ;; Do While True
    ;; Start Do While Block block_9a020000
    
    (block $block_9a020000 
        (loop $loop_9b020000
    
                    (br_if $block_9a020000 (i32.eqz (i32.const 1)))
            (drop (call $loopTest1.doNothing ))
            (br $loop_9b020000)
    
        )
    )
    ;; End Loop loop_9b020000
    )
    (func $loopTest1.doNothing  (result i32)
        ;; Public Function doNothing() As i32
        
    
    
    (return (i32.const 0))
    )
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $loopTest1.constructor )
)

(func $loopTest1.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)