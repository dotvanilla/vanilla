(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/18/2019 2:22:27 PM
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
    (global $Math.E (mut f64) (f64.const 2.7182818284590451))
(global $Math.PI (mut f64) (f64.const 3.1415926535897931))
(global $Integer.MaxValue (mut i32) (i32.const 2147483647))
(global $Long.MaxValue (mut i64) (i64.const 9223372036854775807))
(global $Single.MaxValue (mut f32) (f32.const 3.40282347e+38))
(global $Double.MaxValue (mut f64) (f64.const 1.7976931348623157e+308))
(global $Integer.MinValue (mut i32) (i32.const -2147483648))
(global $Long.MinValue (mut i64) (i64.const -9223372036854775808))
(global $Single.MinValue (mut f32) (f32.const -3.40282347e+38))
(global $Double.MinValue (mut f64) (f64.const -1.7976931348623157e+308))

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
                    (br_if $block_9a020000 (i32.const 1))
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