(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 4/27/2019 5:40:36 PM

    ;; imports must occur before all non-import definitions

    ;; Declare Function print Lib "console" Alias "log" (info As string) As i32
    (func $print (import "console" "log") (param $info i32) (result i32))
    ;; Declare Function err Lib "console" Alias "error" (message As any) As void
    (func $err (import "console" "error") (param $message i32) )
    ;; Declare Function string_replace Lib "string" Alias "replace" (input As string, find As intptr, replacement As string) As string
    (func $string_replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
    ;; Declare Function string_add Lib "string" Alias "add" (a As string, b As string) As string
    (func $string_add (import "string" "add") (param $a i32) (param $b i32) (result i32))
    ;; Declare Function string_length Lib "string" Alias "length" (text As string) As i32
    (func $string_length (import "string" "length") (param $text i32) (result i32))
    ;; Declare Function string_indexOf Lib "string" Alias "indexOf" (input As string, find As string) As i32
    (func $string_indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
    ;; Declare Function i32.toString Lib "string" Alias "toString" (x As i32) As string
    (func $i32.toString (import "string" "toString") (param $x i32) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    ;; String from 1 with 36 bytes in memory
    (data (i32.const 1) "This is the optional parameter value\00")

    ;; String from 38 with 20 bytes in memory
    (data (i32.const 38) "Another string value\00")

    ;; String from 59 with 12 bytes in memory
    (data (i32.const 59) "345566777777\00")

    ;; String from 72 with 15 bytes in memory
    (data (i32.const 72) "this is message\00")
    
    

    ;; export from [functionTest]
    
    (export "calls" (func $calls))
    (export "extensionFunctiontest" (func $extensionFunctiontest))
    (export "Main" (func $Main))
    (export "outputError" (func $outputError))
    
     

    ;; functions in [functionTest]
    
    (func $calls  
        ;; Public Function calls() As void
        
    (call $Main (i32.const 1) (i32.const -100) (i32.const 1))
    (call $Main (i32.const 38) (i32.const 999999) (i32.const 1))
    (drop (call $outputError ))
    )
    (func $extensionFunctiontest  
        ;; Public Function extensionFunctiontest() As void
        
    (drop (call $print (i32.const 59)))
    )
    (func $Main (param $args i32) (param $obj i32) (param $f i32) 
        ;; Public Function Main(args As string, obj As i32, f As boolean) As void
        
    (drop (call $print (call $i32.toString (i32.eq (i32.const 0) (get_local $f)))))
    (drop (call $print (call $i32.toString (i32.ne (i32.const 0) (get_local $f)))))
    (drop (call $print (call $i32.toString (i32.eqz (i32.eq (i32.const 0) (get_local $f))))))
    (drop (call $print (call $i32.toString (i32.eqz (get_local $f)))))
    (drop (call $print (get_local $args)))
    (drop (call $print (call $i32.toString (get_local $obj))))
    (drop (call $print (call $i32.toString (i32.const 1))))
    (drop (call $print (call $i32.toString (i32.eq (get_local $args) (i32.const 0)))))
    )
    (func $outputError  (result f32)
        ;; Public Function outputError() As f32
        
    (call $err (i32.const 72))
    (return (f32.demote/f64 (f64.sub (f64.const 0) (f64.const 0.0001))))
    )
    )