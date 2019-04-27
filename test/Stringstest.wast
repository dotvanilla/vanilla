(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 4/27/2019 3:13:58 PM

    ;; imports must occur before all non-import definitions

    ;; Declare Function Print Lib "console" Alias "log" (text As string) As i32
    (func $Print (import "console" "log") (param $text i32) (result i32))
    ;; Declare Function string_add Lib "string" Alias "add" (a As string, b As string) As string
    (func $string_add (import "string" "add") (param $a i32) (param $b i32) (result i32))
    ;; Declare Function i32_toString Lib "string" Alias "toString" (s As i32) As string
    (func $i32_toString (import "string" "toString") (param $s i32) (result i32))
    ;; Declare Function string_replace Lib "string" Alias "replace" (input As string, find As intptr, replacement As string) As string
    (func $string_replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
    ;; Declare Function string_length Lib "string" Alias "length" (text As string) As i32
    (func $string_length (import "string" "length") (param $text i32) (result i32))
    ;; Declare Function string_indexOf Lib "string" Alias "indexOf" (input As string, find As string) As i32
    (func $string_indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
    ;; Declare Function i64_toString Lib "string" Alias "toString" (s As i64) As string
    (func $i64_toString (import "string" "toString") (param $s i64) (result i32))
    ;; Declare Function f64_toString Lib "string" Alias "toString" (s As f64) As string
    (func $f64_toString (import "string" "toString") (param $s f64) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    ;; String from 1 with 4 bytes in memory
    (data (i32.const 1) "let \00")

    ;; String from 6 with 3 bytes in memory
    (data (i32.const 6) " + \00")

    ;; String from 10 with 3 bytes in memory
    (data (i32.const 10) " / \00")

    ;; String from 14 with 3 bytes in memory
    (data (i32.const 14) " = \00")
    
    (global $a (mut i32) (i32.const 99))

(global $b (mut i32) (i32.const 100))

    ;; export from [Stringstest]
    
    (export "Main" (func $Main))
    (export "Hello" (func $Hello))
    (export "World" (func $World))
    
     

    ;; functions in [Stringstest]
    
    (func $Main  (result i32)
        ;; Public Function Main() As string
        (local $str i32)
    (local $C i64)
    (local $format i32)
    (set_local $str (call $i32_toString (call $string_add (call $string_add (call $Hello ) (i32.const  )) (call $World ))))
    (set_local $C (i64.const 8888888888888))
    (set_local $format (call $i32_toString (call $string_add (call $string_add (call $string_add (call $string_add (call $string_add (call $string_add (call $string_add (i32.const 1) (call $i32_toString (get_global $a))) (i32.const 6)) (call $i32_toString (get_global $b))) (i32.const 10)) (call $i64_toString (get_local $C))) (i32.const 14)) (call $f64_toString (f64.add (f64.convert_s/i32 (get_global $a)) (f64.div (f64.convert_s/i32 (get_global $b)) (f64.convert_s/i64 (get_local $C))))))))
    (drop (call $Print (call $i32_toString (get_local $str))))
    (drop (call $Print (call $i32_toString (get_local $format))))
    (return (call $i32_toString (get_local $str)))
    )
    (func $Hello  (result i32)
        ;; Public Function Hello() As string
        
    (return (call $i32_toString (i32.const Hello)))
    )
    (func $World  (result i32)
        ;; Public Function World() As string
        
    (return (call $i32_toString (i32.const World)))
    )
    )