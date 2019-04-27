(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 4/27/2019 3:01:16 PM

    ;; imports must occur before all non-import definitions

    
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    
    

    ;; export from [EnumTest]
    
    (export "DoAdd" (func $DoAdd))
    (export "Add1" (func $Add1))
    
     

    ;; functions in [EnumTest]
    
    (func $DoAdd  (result i32)
        ;; Public Function DoAdd() As any
        
    (return (call $Add1 (i64.add (i64.add (i64.extend_s/i32 (i32.const 3)) (i64.const 4)) (i64.extend_s/i32 (i32.const 999)))))
    )
    (func $Add1 (param $i i32) (result i32)
        ;; Public Function Add1(i As any) As any
        (local $x i32)
    (local $a i32)
    (set_local $x (any.add (get_local $i) (i32.const 1)))
    (set_local $a (get_local $x))
    (return (get_local $a))
    )
    )