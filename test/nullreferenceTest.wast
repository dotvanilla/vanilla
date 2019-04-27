(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 4/27/2019 4:46:02 PM

    ;; imports must occur before all non-import definitions

    ;; Declare Function DOMbyId Lib "document" Alias "getElementById" (id As string) As i32
    (func $DOMbyId (import "document" "getElementById") (param $id i32) (result i32))
    ;; Declare Function setAttr Lib "document" Alias "setAttribute" (node As i32, name As string, value As string) As i32
    (func $setAttr (import "document" "setAttribute") (param $node i32) (param $name i32) (param $value i32) (result i32))
    ;; Declare Function print Lib "console" Alias "log" (message As string) As i32
    (func $print (import "console" "log") (param $message i32) (result i32))
    ;; Declare Function string_replace Lib "string" Alias "replace" (input As string, find As intptr, replacement As string) As string
    (func $string_replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
    ;; Declare Function string_add Lib "string" Alias "add" (a As string, b As string) As string
    (func $string_add (import "string" "add") (param $a i32) (param $b i32) (result i32))
    ;; Declare Function string_length Lib "string" Alias "length" (text As string) As i32
    (func $string_length (import "string" "length") (param $text i32) (result i32))
    ;; Declare Function string_indexOf Lib "string" Alias "indexOf" (input As string, find As string) As i32
    (func $string_indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
    ;; Declare Function i32_toString Lib "string" Alias "toString" (s As i32) As string
    (func $i32_toString (import "string" "toString") (param $s i32) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    ;; String from 1 with 7 bytes in memory
    (data (i32.const 1) "Nothing\00")

    ;; String from 9 with 4 bytes in memory
    (data (i32.const 9) "test\00")

    ;; String from 14 with 1 bytes in memory
    (data (i32.const 14) "a\00")

    ;; String from 16 with 1 bytes in memory
    (data (i32.const 16) "b\00")

    ;; String from 18 with 1 bytes in memory
    (data (i32.const 18) "a\00")

    ;; String from 20 with 1 bytes in memory
    (data (i32.const 20) "b\00")
    
    

    ;; export from [nullreferenceTest]
    
    (export "noReturns" (func $noReturns))
    (export "test" (func $test))
    
     

    ;; functions in [nullreferenceTest]
    
    (func $noReturns  
        ;; Public Function noReturns() As void
        
    (drop (call $print (call $i32_toString (i32.const 1))))
    (drop (call $print (i32.const 0)))
    )
    (func $test  (result i32)
        ;; Public Function test() As i32
        (local $node i32)
    (set_local $node (call $DOMbyId (call $i32_toString (i32.const 9))))
    (drop (call $setAttr (get_local $node) (call $i32_toString (i32.const 14)) (call $i32_toString (i32.const 16))))
    (drop (call $setAttr (i32.const 0) (call $i32_toString (i32.const 18)) (call $i32_toString (i32.const 20))))
    (return (i32.const 0))
    )
    )