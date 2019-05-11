(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/11/2019 11:08:28 AM
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
    ;; export from VB.NET module: [multipleLevelLoops]
    
    (export "multipleLevelLoops.Main" (func $multipleLevelLoops.Main))
    
     

    ;; functions in [multipleLevelLoops]
    
    (func $multipleLevelLoops.Main  
        ;; Public Function Main() As void
        
    (local $i i32)
    
    ;; Do ... Loop
    ;; Start Do While Block block_9a020000
    
    (block $block_9a020000 
        (loop $loop_9b020000
    
                    ;; Do While i < 999
            ;; Start Do While Block block_9c020000
            
    (block $block_9c020000 
        (loop $loop_9d020000
    
                    (br_if $block_9c020000 (i32.eqz (i32.lt_s (get_local $i) (i32.const 999))))
            ;; Do ... Loop
            ;; Start Do While Block block_9e020000
            
    (block $block_9e020000 
        (loop $loop_9f020000
    
                    (set_local $i (i32.add (get_local $i) (i32.const 1)))
            
    (if (i32.eq (i32.rem_s (get_local $i) (i32.const 3)) (i32.const 0)) 
        (then
                    (br_if $block_9e020000 (i32.const 1))
        ) 
    )
            (br_if $block_9e020000 (i32.eqz (i32.gt_s (get_local $i) (i32.const 50))))
            (br $loop_9f020000)
    
        )
    )
            ;; End Loop loop_9f020000
            (br $loop_9d020000)
    
        )
    )
            ;; End Loop loop_9d020000
            
    (if (i32.gt_s (get_local $i) (i32.const 10)) 
        (then
                    (br_if $block_9e020000 (i32.const 1))
        ) 
    )
            (br $loop_9b020000)
    
        )
    )
    ;; End Loop loop_9b020000
    )
    
    
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $multipleLevelLoops.constructor )
)

(func $multipleLevelLoops.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)