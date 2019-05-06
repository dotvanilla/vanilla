(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/6/2019 8:26:10 PM
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
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 22))

    ;; Memory data for string constant
    
    ;; String from 10 with 3 bytes in memory
    (data (i32.const 10) "sdf\00")

    ;; String from 14 with 3 bytes in memory
    (data (i32.const 14) "sdf\00")

    ;; String from 18 with 3 bytes in memory
    (data (i32.const 18) "sdf\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    

    ;; Global variables in this module
    (global $multipleModuleContructortest1.a (mut i32) (i32.const 0))
(global $multipleModuleContructortest2.a (mut i32) (i32.const 0))
(global $multipleModuleContructortest3.a (mut i32) (i32.const 0))

    ;; Export methods of this module
     



;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $multipleModuleContructortest1.constructor )

(call $multipleModuleContructortest2.constructor )

(call $multipleModuleContructortest3.constructor )
)

(start $Application_SubNew)

)