(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 4/27/2019 10:28:25 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    
    

    ;; export from [ForLoopTest]
    
    (export "forloop" (func $forloop))
    
     

    ;; functions in [ForLoopTest]
    
    (func $forloop  (result f64)
        ;; Public Function forloop() As f64
        (local $x f64)
    (local $i i32)
    (set_local $x (f64.convert_s/i32 (i32.const 999)))
    (set_local $i (i32.const 0))
    ;; For i As Integer = 0 To 100 Step 2
    
    (block $block_9a020000 
        (loop $loop_9b020000
    
                    (br_if $block_9a020000 (i32.lt_s (get_local $i) (i32.const 100)))
            (set_local $x (f64.add (get_local $x) (f64.const 0.01)))
            (set_local $i (i32.add (get_local $i) (i32.const 2)))
            (br $loop_9b020000)
            ;; For Loop Next On loop_9b020000
    
        )
    )
    (return (get_local $x))
    )
    )