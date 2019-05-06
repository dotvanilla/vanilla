(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/6/2019 8:37:27 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function Print Lib "console" Alias "log" (text As string) As i32
    (func $Stringstest.Print (import "console" "log") (param $text i32) (result i32))
;; Declare Function string.replace Lib "string" Alias "replace" (input As string, find As intptr, replacement As string) As string
    (func $string.replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
;; Declare Function string.add Lib "string" Alias "add" (a As string, b As string) As string
    (func $string.add (import "string" "add") (param $a i32) (param $b i32) (result i32))
;; Declare Function string.length Lib "string" Alias "length" (text As string) As i32
    (func $string.length (import "string" "length") (param $text i32) (result i32))
;; Declare Function string.indexOf Lib "string" Alias "indexOf" (input As string, find As string) As i32
    (func $string.indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
;; Declare Function string.trim Lib "string" Alias "trim" (s As string) As string
    (func $string.trim (import "string" "trim") (param $s i32) (result i32))
;; Declare Function f64.toString Lib "string" Alias "toString" (x As f64) As string
    (func $f64.toString (import "string" "toString") (param $x f64) (result i32))
;; Declare Function i32.toString Lib "string" Alias "toString" (x As i32) As string
    (func $i32.toString (import "string" "toString") (param $x i32) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 45))

    ;; Memory data for string constant
    
    ;; String from 10 with 3 bytes in memory
    (data (i32.const 10) "AAA\00")

    ;; String from 14 with 1 bytes in memory
    (data (i32.const 14) " \00")

    ;; String from 16 with 4 bytes in memory
    (data (i32.const 16) "let \00")

    ;; String from 21 with 3 bytes in memory
    (data (i32.const 21) " + \00")

    ;; String from 25 with 3 bytes in memory
    (data (i32.const 25) " / \00")

    ;; String from 29 with 3 bytes in memory
    (data (i32.const 29) " = \00")

    ;; String from 33 with 5 bytes in memory
    (data (i32.const 33) "Hello\00")

    ;; String from 39 with 5 bytes in memory
    (data (i32.const 39) "World\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    

    ;; Global variables in this module
    (global $Stringstest.C (mut f64) (f64.const 8888888888888))
(global $Stringstest.a (mut i32) (i32.const 99))
(global $Stringstest.b (mut i32) (i32.const 100))

    ;; Export methods of this module
    ;; export from VB.NET module: [Stringstest]
    
    (export "Stringstest.stringmemberTest" (func $Stringstest.stringmemberTest))
    (export "Stringstest.Main" (func $Stringstest.Main))
    (export "Stringstest.Hello" (func $Stringstest.Hello))
    (export "Stringstest.World" (func $Stringstest.World))
    
     

    ;; functions in [Stringstest]
    
    (func $Stringstest.stringmemberTest  
        ;; Public Function stringmemberTest() As void
        
    (local $length i64)
    (local $lenPlus100 i64)
    
    (drop (call $Stringstest.Print (call $f64.toString (f64.add (get_global $Stringstest.C) (f64.convert_s/i32 (call $string.length (call $string.trim (call $string.replace (call $Stringstest.Hello ) (i32.const 10) (i32.const 0)))))))))
    (set_local $length (i64.extend_s/i32 (call $string.length (call $Stringstest.Hello ))))
    (set_local $lenPlus100 (i64.add (i64.extend_s/i32 (get_global $Stringstest.b)) (get_local $length)))
    )
    (func $Stringstest.Main  (result i32)
        ;; Public Function Main() As string
        
    (local $str i32)
    (local $format i32)
    
    (set_local $str (call $string.add (call $string.add (call $Stringstest.Hello ) (i32.const 14)) (call $Stringstest.World )))
    (set_local $format (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 16) (call $i32.toString (get_global $Stringstest.a))) (i32.const 21)) (call $i32.toString (get_global $Stringstest.b))) (i32.const 25)) (call $f64.toString (get_global $Stringstest.C))) (i32.const 29)) (call $f64.toString (f64.add (f64.convert_s/i32 (get_global $Stringstest.a)) (f64.div (f64.convert_s/i32 (get_global $Stringstest.b)) (get_global $Stringstest.C))))))
    (drop (call $Stringstest.Print (get_local $str)))
    (drop (call $Stringstest.Print (get_local $format)))
    (return (get_local $str))
    )
    (func $Stringstest.Hello  (result i32)
        ;; Public Function Hello() As string
        
    
    
    (return (i32.const 33))
    )
    (func $Stringstest.World  (result i32)
        ;; Public Function World() As string
        
    
    
    (return (i32.const 39))
    )
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $Stringstest.constructor )
)

(func $Stringstest.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)