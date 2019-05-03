(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/3/2019 10:33:40 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function string.replace Lib "string" Alias "replace" (input As string, find As intptr, replacement As string) As string
    (func $string.replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
    ;; Declare Function string.add Lib "string" Alias "add" (a As string, b As string) As string
    (func $string.add (import "string" "add") (param $a i32) (param $b i32) (result i32))
    ;; Declare Function string.length Lib "string" Alias "length" (text As string) As i32
    (func $string.length (import "string" "length") (param $text i32) (result i32))
    ;; Declare Function string.indexOf Lib "string" Alias "indexOf" (input As string, find As string) As i32
    (func $string.indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
    ;; Declare Function print Lib "console" Alias "log" (data As f64) As void
    (func $Runtest.print (import "console" "log") (param $data f64) )
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 273))

    ;; Memory data for string constant
    
    ;; String from 1 with 5 bytes in memory
    (data (i32.const 1) "55555\00")

    ;; String from 256 with 16 bytes in memory
    (data (i32.const 256) "{55, 55, 555, 5}\00")
    
    

    ;; export from VB.NET module: [Runtest]
    
    (export "Runtest.test" (func $Runtest.test))
    
     

    ;; functions in [Runtest]
    
    (func $Runtest.test  
        ;; Public Function test() As void
        (local $s i32)
    
    ;; Initialize a object instance of [CircleModel]
    ;; Object memory block begin at location: (get_global $global.ObjectManager)
    ;; set field [moduleContainer.name1.CircleModel::radius]
    (f64.store (i32.add (get_global $global.ObjectManager) (i32.const 12)) (f64.convert_s/i32 (i32.const 100001)))
    ;; set field [moduleContainer.name1.CircleModel::x]
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.sub (i32.const 0) (i32.const 1)))
    ;; set field [moduleContainer.name1.CircleModel::y]
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 8)) (i32.trunc_s/f64 (f64.const 1.0009)))
    ;; set field [moduleContainer.name1.CircleModel::nodeName]
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 0)) (i32.const 256))
    ;; Offset object manager with 20 bytes.
    (set_global $global.ObjectManager (i32.add (get_global $global.ObjectManager) (i32.const 20)))
    ;; Initialize an object memory block with 20 bytes data
    
    (set_local $s (get_global $global.ObjectManager))
    (call $Runtest.print (f64.load (i32.add (get_local $s) (i32.const 12))))
    )
    

;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew


)

(start $Application_SubNew)

)