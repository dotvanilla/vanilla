(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/1/2019 12:47:26 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function Random Lib "Math" Alias "random" () As f64
    (func $boolTest.Random (import "Math" "random")  (result f64))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    
    (global $boolTest.b2 (mut i32) (i32.const 1))

(global $boolTest.threshold (mut f32) (f32.const 0.5))

    ;; export from [boolTest]
    
    (export "boolTest.logical" (func $boolTest.logical))
    
     

    ;; functions in [boolTest]
    
    (func $boolTest.logical  (result i32)
        ;; Public Function logical() As i32
        (local $b i32)
    (set_local $b (f64.ge (f64.promote/f32 (f32.demote/f64 (call $boolTest.Random ))) (f64.add (f64.promote/f32 (get_global $boolTest.threshold)) (f64.const 0.1))))
    
    (if (i32.mul (get_local $b) (get_global $boolTest.b2)) 
        (then
                    (return (i32.const 1))
        ) (else
                    (return (i32.sub (i32.const 0) (i32.const 100)))
        )
    )
    (return (i32.const 0))
    )
    )