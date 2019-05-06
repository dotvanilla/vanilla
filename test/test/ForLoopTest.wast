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
    (global $ForLoopTest.zero (mut i64) (i64.const 0))
(global $ForLoopTest.delta (mut i32) (i32.const 3))

    ;; Export methods of this module
    ;; export from VB.NET module: [ForLoopTest]
    
    (export "ForLoopTest.forloop" (func $ForLoopTest.forloop))
    
     

    ;; functions in [ForLoopTest]
    
    (func $ForLoopTest.forloop  (result f64)
        ;; Public Function forloop() As f64
        
    (local $x f64)
    (local $delta f32)
    (local $i i32)
    
    (set_local $x (f64.convert_s/i32 (i32.const 999)))
    (set_local $delta (f32.const 0.001))
    (set_local $i (i32.wrap/i64 (get_global $ForLoopTest.zero)))
    ;; For i As Integer = zero To 100 Step ForLoopTest.delta
    
    (block $block_9a020000 
        (loop $loop_9b020000
    
                    (br_if $block_9a020000 (i32.eq (get_local $i) (i32.const 100)))
            (set_local $x (f64.add (get_local $x) (f64.promote/f32 (get_local $delta))))
            ;; For loop control step: (get_global $ForLoopTest.delta)
            (set_local $i (i32.add (get_local $i) (get_global $ForLoopTest.delta)))
            (br $loop_9b020000)
            ;; For Loop Next On loop_9b020000
    
        )
    )
    (return (get_local $x))
    )
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $ForLoopTest.constructor )
)

(func $ForLoopTest.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)