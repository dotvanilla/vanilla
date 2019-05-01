(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/1/2019 1:40:03 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function print Lib "console" Alias "log" (info As string, color As string, size As i64) As i32
    (func $optionalParameterTest.print (import "console" "log") (param $info i32) (param $color i32) (param $size i64) (result i32))
    ;; Declare Function print Lib "console" Alias "log" (info As string) As i32
    (func $functionTest.print (import "console" "log") (param $info i32) (result i32))
    ;; Declare Function err Lib "console" Alias "error" (message As any) As void
    (func $ExportAPiModule.err (import "console" "error") (param $message i32) )
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
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    ;; String from 1 with 5 bytes in memory
    (data (i32.const 1) "Hello\00")

    ;; String from 7 with 5 bytes in memory
    (data (i32.const 7) "green\00")

    ;; String from 13 with 5 bytes in memory
    (data (i32.const 13) "green\00")

    ;; String from 19 with 6 bytes in memory
    (data (i32.const 19) "909090\00")

    ;; String from 26 with 5 bytes in memory
    (data (i32.const 26) "green\00")

    ;; String from 32 with 4 bytes in memory
    (data (i32.const 32) "size\00")

    ;; String from 37 with 5 bytes in memory
    (data (i32.const 37) "green\00")

    ;; String from 43 with 8 bytes in memory
    (data (i32.const 43) "not sure\00")

    ;; String from 52 with 3 bytes in memory
    (data (i32.const 52) "red\00")

    ;; String from 56 with 15 bytes in memory
    (data (i32.const 56) "this is message\00")

    ;; String from 72 with 36 bytes in memory
    (data (i32.const 72) "This is the optional parameter value\00")

    ;; String from 109 with 20 bytes in memory
    (data (i32.const 109) "Another string value\00")

    ;; String from 130 with 12 bytes in memory
    (data (i32.const 130) "345566777777\00")

    ;; String from 143 with 15 bytes in memory
    (data (i32.const 143) "this is message\00")
    
    

    ;; export from [optionalParameterTest]
    
    (export "optionalParameterTest.calls" (func $optionalParameterTest.calls))
    
    
    ;; export from [functionTest]
    
    (export "functionTest.outputError" (func $functionTest.outputError))
    (export "functionTest.calls" (func $functionTest.calls))
    (export "functionTest.extensionFunctiontest" (func $functionTest.extensionFunctiontest))
    (export "functionTest.Main" (func $functionTest.Main))
    
    
    ;; export from [ExportAPiModule]
    
    (export "ExportAPiModule.outputError" (func $ExportAPiModule.outputError))
    
     

    ;; functions in [optionalParameterTest]
    
    (func $optionalParameterTest.calls  
        ;; Public Function calls() As void
        (local $obj i32)
    (set_local $obj (i32.const 1))
    (drop (call $optionalParameterTest.print (get_local $obj) (i32.const 7) (i64.const 99)))
    (drop (call $optionalParameterTest.print (get_local $obj) (i32.const 13) (i64.extend_s/i32 (i32.sub (i32.const 0) (i32.const 99)))))
    (drop (call $optionalParameterTest.print (call $string.add (get_local $obj) (i32.const 19)) (i32.const 26) (i64.const 99)))
    (drop (call $optionalParameterTest.print (i32.const 32) (i32.const 37) (i64.const 88)))
    (drop (call $optionalParameterTest.print (i32.const 43) (i32.const 52) (i64.trunc_s/f64 (f64.const 77.555))))
    )
    
    
    ;; functions in [functionTest]
    
    (func $functionTest.outputError  (result f32)
        ;; Public Function outputError() As f32
        
    (call $ExportAPiModule.err (i32.const 56))
    (return (f32.demote/f64 (f64.sub (f64.const 0) (f64.const 0.0001))))
    )
    (func $functionTest.calls  
        ;; Public Function calls() As void
        (local $x i64)
    (call $functionTest.Main (i32.const 72) (i32.const -100) (i32.const 1))
    (call $functionTest.Main (i32.const 109) (i32.trunc_s/f64 (f64.const 99999.9)) (i32.const 1))
    (drop (call $functionTest.outputError ))
    (set_local $x (i64.add (i64.trunc_s/f32 (call $functionTest.outputError )) (call $ExportAPiModule.outputError )))
    (call $optionalParameterTest.calls )
    (call $functionTest.calls )
    )
    (func $functionTest.extensionFunctiontest  
        ;; Public Function extensionFunctiontest() As void
        
    (drop (call $functionTest.print (i32.const 130)))
    )
    (func $functionTest.Main (param $args i32) (param $obj i32) (param $f i32) 
        ;; Public Function Main(args As string, obj As i32, f As boolean) As void
        
    (drop (call $functionTest.print (call $i32.toString (i32.eq (i32.const 0) (get_local $f)))))
    (drop (call $functionTest.print (call $i32.toString (i32.ne (i32.const 0) (get_local $f)))))
    (drop (call $functionTest.print (call $i32.toString (i32.eqz (i32.eq (i32.const 0) (get_local $f))))))
    (drop (call $functionTest.print (call $i32.toString (i32.eqz (get_local $f)))))
    (drop (call $functionTest.print (get_local $args)))
    (drop (call $functionTest.print (call $i32.toString (get_local $obj))))
    (drop (call $functionTest.print (call $i32.toString (i32.const 1))))
    (drop (call $functionTest.print (call $i32.toString (i32.eq (get_local $args) (i32.const 0)))))
    )
    
    
    ;; functions in [ExportAPiModule]
    
    (func $ExportAPiModule.outputError  (result i64)
        ;; Public Function outputError() As i64
        
    (call $ExportAPiModule.err (i32.const 143))
    (return (i64.trunc_s/f64 (f64.sub (f64.const 0) (f64.const 10.0001))))
    )
    )