(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 4/27/2019 5:57:01 PM

    ;; imports must occur before all non-import definitions

    ;; Declare Function Print Lib "console" Alias "log" (text As string) As i32
    (func $Print (import "console" "log") (param $text i32) (result i32))
    ;; Declare Function string_length Lib "string" Alias "length" (text As string) As i32
    (func $string_length (import "string" "length") (param $text i32) (result i32))
    ;; Declare Function string_replace Lib "string" Alias "replace" (input As string, find As intptr, replacement As string) As string
    (func $string_replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
    ;; Declare Function string_add Lib "string" Alias "add" (a As string, b As string) As string
    (func $string_add (import "string" "add") (param $a i32) (param $b i32) (result i32))
    ;; Declare Function string_indexOf Lib "string" Alias "indexOf" (input As string, find As string) As i32
    (func $string_indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
    ;; Declare Function i32.toString Lib "string" Alias "toString" (x As i32) As string
    (func $i32.toString (import "string" "toString") (param $x i32) (result i32))
    ;; Declare Function f64.toString Lib "string" Alias "toString" (x As f64) As string
    (func $f64.toString (import "string" "toString") (param $x f64) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    ;; String from 1 with 1 bytes in memory
    (data (i32.const 1) " \00")

    ;; String from 3 with 4 bytes in memory
    (data (i32.const 3) "let \00")

    ;; String from 8 with 3 bytes in memory
    (data (i32.const 8) " + \00")

    ;; String from 12 with 3 bytes in memory
    (data (i32.const 12) " / \00")

    ;; String from 16 with 3 bytes in memory
    (data (i32.const 16) " = \00")

    ;; String from 20 with 5 bytes in memory
    (data (i32.const 20) "Hello\00")

    ;; String from 26 with 5 bytes in memory
    (data (i32.const 26) "World\00")
    
    (global $C (mut f64) (f64.const 8888888888888))

(global $a (mut i32) (i32.const 99))

(global $b (mut i32) (i32.const 100))

    ;; export from [Stringstest]
    
    (export "stringmemberTest" (func $stringmemberTest))
    (export "Main" (func $Main))
    (export "Hello" (func $Hello))
    (export "World" (func $World))
    
     

    ;; functions in [Stringstest]
    
    (func $stringmemberTest  
        ;; Public Function stringmemberTest() As void
        (local $lenPlus100 i32)
    (set_local $lenPlus100 (i32.add (get_global $b) (call $string_length (call $Hello ))))
    )
    (func $Main  (result i32)
        ;; Public Function Main() As string
        (local $str i32)
    (local $format i32)
    (set_local $str (call $string_add (call $string_add (call $Hello ) (i32.const 1)) (call $World )))
    (set_local $format (call $string_add (call $string_add (call $string_add (call $string_add (call $string_add (call $string_add (call $string_add (i32.const 3) (call $i32.toString (get_global $a))) (i32.const 8)) (call $i32.toString (get_global $b))) (i32.const 12)) (call $f64.toString (get_global $C))) (i32.const 16)) (call $f64.toString (f64.add (f64.convert_s/i32 (get_global $a)) (f64.div (f64.convert_s/i32 (get_global $b)) (get_global $C))))))
    (drop (call $Print (get_local $str)))
    (drop (call $Print (get_local $format)))
    (return (get_local $str))
    )
    (func $Hello  (result i32)
        ;; Public Function Hello() As string
        
    (return (i32.const 20))
    )
    (func $World  (result i32)
        ;; Public Function World() As string
        
    (return (i32.const 26))
    )
    )