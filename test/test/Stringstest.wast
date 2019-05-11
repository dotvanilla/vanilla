(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/11/2019 3:43:13 PM
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
    (global $global.ObjectManager (mut i32) (i32.const 46))

    ;; Memory data for string constant
        
    ;; String from 11 with 3 bytes in memory
    (data (i32.const 11) "AAA\00")
    
    ;; String from 15 with 1 bytes in memory
    (data (i32.const 15) " \00")
    
    ;; String from 17 with 4 bytes in memory
    (data (i32.const 17) "let \00")
    
    ;; String from 22 with 3 bytes in memory
    (data (i32.const 22) " + \00")
    
    ;; String from 26 with 3 bytes in memory
    (data (i32.const 26) " / \00")
    
    ;; String from 30 with 3 bytes in memory
    (data (i32.const 30) " = \00")
    
    ;; String from 34 with 5 bytes in memory
    (data (i32.const 34) "Hello\00")
    
    ;; String from 40 with 5 bytes in memory
    (data (i32.const 40) "World\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    

    ;; Global variables in this module
    (global $Math.E (mut f64) (f64.const 2.71828182845905))
(global $Math.PI (mut f64) (f64.const 3.14159265358979))
(global $Integer.MaxValue (mut i32) (i32.const 2147483647))
(global $Long.MaxValue (mut i64) (i64.const 9223372036854775807))
(global $Single.MaxValue (mut f32) (f32.const 340282356779733623858607532500980858880))
(global $Double.MaxValue (mut f64) (f64.const 179769313486231570814527423731704356798070567525844996598917476803157260780028538760589558632766878171540458953514382464234321326889464182768467546703537516986049910576551282076245490090389328944075868508455133942304583236903222948165808559332123348274797826204144723168738177180919299881250404026184124858368))
(global $Integer.MinValue (mut i32) (i32.const -2147483648))
(global $Long.MinValue (mut i64) (i64.const -9223372036854775808))
(global $Single.MinValue (mut f32) (f32.const -340282356779733623858607532500980858880))
(global $Double.MinValue (mut f64) (f64.const -179769313486231570814527423731704356798070567525844996598917476803157260780028538760589558632766878171540458953514382464234321326889464182768467546703537516986049910576551282076245490090389328944075868508455133942304583236903222948165808559332123348274797826204144723168738177180919299881250404026184124858368))
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
    
    (drop (call $Stringstest.Print (call $f64.toString (f64.add (get_global $Stringstest.C) (f64.convert_s/i32 (call $string.length (call $string.trim (call $string.replace (call $Stringstest.Hello ) (i32.const 11) (i32.const 0)))))))))
    (set_local $length (i64.extend_s/i32 (call $string.length (call $Stringstest.Hello ))))
    (set_local $lenPlus100 (i64.add (i64.extend_s/i32 (get_global $Stringstest.b)) (get_local $length)))
    )
    
    
    (func $Stringstest.Main  (result i32)
        ;; Public Function Main() As string
        
    (local $str i32)
    (local $format i32)
    
    (set_local $str (call $string.add (call $string.add (call $Stringstest.Hello ) (i32.const 15)) (call $Stringstest.World )))
    (set_local $format (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 17) (call $i32.toString (get_global $Stringstest.a))) (i32.const 22)) (call $i32.toString (get_global $Stringstest.b))) (i32.const 26)) (call $f64.toString (get_global $Stringstest.C))) (i32.const 30)) (call $f64.toString (f64.add (f64.convert_s/i32 (get_global $Stringstest.a)) (f64.div (f64.convert_s/i32 (get_global $Stringstest.b)) (get_global $Stringstest.C))))))
    (drop (call $Stringstest.Print (get_local $str)))
    (drop (call $Stringstest.Print (get_local $format)))
    (return (get_local $str))
    )
    
    
    (func $Stringstest.Hello  (result i32)
        ;; Public Function Hello() As string
        
    
    
    (return (i32.const 34))
    )
    
    
    (func $Stringstest.World  (result i32)
        ;; Public Function World() As string
        
    
    
    (return (i32.const 40))
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