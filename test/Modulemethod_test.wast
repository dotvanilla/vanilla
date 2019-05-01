(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/1/2019 11:17:11 AM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function array.new Lib "Array" Alias "create" (size As i32) As array
    (func $array.new (import "Array" "create") (param $size i32) (result i32))
    ;; Declare Function i32_array.push Lib "Array" Alias "push" (array As array, element As i32) As array
    (func $i32_array.push (import "Array" "push") (param $array i32) (param $element i32) (result i32))
    ;; Declare Function i32_array.get Lib "Array" Alias "get" (array As array, index As i32) As i32
    (func $i32_array.get (import "Array" "get") (param $array i32) (param $index i32) (result i32))
    ;; Declare Function i32_array.set Lib "Array" Alias "set" (array As array, index As i32, value As i32) As void
    (func $i32_array.set (import "Array" "set") (param $array i32) (param $index i32) (param $value i32) )
    ;; Declare Function array.length Lib "Array" Alias "length" (array As array) As i32
    (func $array.length (import "Array" "length") (param $array i32) (result i32))
    ;; Declare Function string.replace Lib "string" Alias "replace" (input As string, find As intptr, replacement As string) As string
    (func $string.replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
    ;; Declare Function string.add Lib "string" Alias "add" (a As string, b As string) As string
    (func $string.add (import "string" "add") (param $a i32) (param $b i32) (result i32))
    ;; Declare Function string.length Lib "string" Alias "length" (text As string) As i32
    (func $string.length (import "string" "length") (param $text i32) (result i32))
    ;; Declare Function string.indexOf Lib "string" Alias "indexOf" (input As string, find As string) As i32
    (func $string.indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
    ;; Declare Function i32.toString Lib "string" Alias "toString" (x As i32) As string
    (func $i32.toString (import "string" "toString") (param $x i32) (result i32))
    ;; Declare Function string_array.push Lib "Array" Alias "push" (array As array, element As string) As array
    (func $string_array.push (import "Array" "push") (param $array i32) (param $element i32) (result i32))
    ;; Declare Function string_array.get Lib "Array" Alias "get" (array As array, index As i32) As string
    (func $string_array.get (import "Array" "get") (param $array i32) (param $index i32) (result i32))
    ;; Declare Function string_array.set Lib "Array" Alias "set" (array As array, index As i32, value As string) As void
    (func $string_array.set (import "Array" "set") (param $array i32) (param $index i32) (param $value i32) )
    
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
    
    (export "Modulemethod_test.arraytypeInferTest" (func $Modulemethod_test.arraytypeInferTest))
    (export "Modulemethod_test.test" (func $Modulemethod_test.test))
    (export "Modulemethod_test.calls" (func $Modulemethod_test.calls))
    
    
    ;; export from [module2]
    
    (export "module2.Runapp" (func $module2.Runapp))
    (export "module2.test" (func $module2.test))
    
     

    ;; functions in [Modulemethod_test]
    
    (func $Modulemethod_test.arraytypeInferTest  (result i32)
        ;; Public Function arraytypeInferTest() As array(Of i64)
        
    (return (call $array_array.push (call $array_array.push (call $array_array.push (call $array_array.push (call $array.new (i32.const -1)) (i64.const 2342)) (i64.const 34)) (i64.const 322)) (i64.const 343)))
    )
    (func $Modulemethod_test.test  (result i32)
        ;; Public Function test() As i32
        
    (return (i32.sub (i32.const 0) (i32.const 9999)))
    )
    (func $Modulemethod_test.calls  
        ;; Public Function calls() As void
        
    (drop (call $Modulemethod_test.test ))
    (drop (call $module2.test (call $string.add (call $string.add (i32.const 1) (call $i32.toString (call $Modulemethod_test.test ))) (i32.const 13))))
    )
    
    
    ;; functions in [module2]
    
    (func $module2.Runapp  
        ;; Public Function Runapp() As void
        
    (call $Modulemethod_test.calls )
    )
    (func $module2.test (param $gg i32) (result i32)
        ;; Public Function test(gg As string) As array(Of string)
        
    (return (call $array_array.push (call $array_array.push (call $array.new (i32.const -1)) (get_local $gg)) (call $string.add (get_local $gg) (i32.const 30))))
    )
    )