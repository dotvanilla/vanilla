(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/1/2019 11:29:14 AM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    
    (global $zero (mut i64) (i64.const 0))

(global $delta (mut i32) (i32.const 3))

    ;; export from [ForLoopTest]
    
    (export "ForLoopTest.forloop" (func $ForLoopTest.forloop))
    
     

    ;; functions in [ForLoopTest]
    
    (func $ForLoopTest.forloop  (result f64)
        ;; Public Function forloop() As f64
        (local $x f64)
    (local $delta f32)
    (local $i i32)
    (set_local $x (f64.const 999))
    (set_local $delta (f32.const 0.001))
    (set_local $i (i32.wrap/i64 (get_global $zero)))
    ;; For i As Integer = zero To 100 Step ForLoopTest.delta
    
    (block $block_9a020000 
        (loop $loop_9b020000
    
                    (br_if $block_9a020000 (i32.eq (get_local $i) (i32.const 100)))
            (set_local $x (f64.add (get_local $x) (f64.promote/f32 (get_local $delta))))
            ;; For loop control step: (get_global $delta)
            (set_local $i (i32.add (get_local $i) (get_global $delta)))
            (br $loop_9b020000)
            ;; For Loop Next On loop_9b020000
    
        )
    )
    (return (get_local $x))
    )
    )