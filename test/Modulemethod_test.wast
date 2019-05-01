(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/1/2019 10:17:34 AM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    ;; String from 1 with 11 bytes in memory
    (data (i32.const 1) "34546734853\00")

    ;; String from 13 with 16 bytes in memory
    (data (i32.const 13) "8sdjkfsdhfsdfsdf\00")

    ;; String from 30 with 5 bytes in memory
    (data (i32.const 30) "ddddd\00")
    
    

    ;; export from [Modulemethod_test]
    
    (export "arraytypeInferTest" (func $Modulemethod_test.arraytypeInferTest))
    (export "test" (func $Modulemethod_test.test))
    (export "calls" (func $Modulemethod_test.calls))
    
    
    ;; export from [module2]
    
    (export "Runapp" (func $module2.Runapp))
    (export "test" (func $module2.test))
    
     

    ;; functions in [Modulemethod_test]
    
    (func $Modulemethod_test.arraytypeInferTest  (result i32)
        ;; Public Function arraytypeInferTest() As array(Of i64)
        
    (return (call $array.push (call $array.push (call $array.push (call $array.push (call $array.new (i32.const -1)) (i64.const 2342)) (i64.const 34)) (i64.const 322)) (i64.const 343)))
    )
    (func $Modulemethod_test.test  (result i32)
        ;; Public Function test() As i32
        
    (return (i32.sub (i32.const 0) (i32.const 9999)))
    )
    (func $Modulemethod_test.calls  
        ;; Public Function calls() As void
        
    (drop (call $Modulemethod_test.test ))
    (drop (call $module2.test (call $string.string.add (call $string.string.add (i32.const 1) (call $string.i32.toString (call $Modulemethod_test.test ))) (i32.const 13))))
    )
    
    
    ;; functions in [module2]
    
    (func $module2.Runapp  
        ;; Public Function Runapp() As void
        
    (call $Modulemethod_test.calls )
    )
    (func $module2.test (param $gg i32) (result i32)
        ;; Public Function test(gg As string) As array(Of string)
        
    (return (call $array.push (call $array.push (call $array.new (i32.const -1)) (get_local $gg)) (call $string.string.add (get_local $gg) (i32.const 30))))
    )
    )