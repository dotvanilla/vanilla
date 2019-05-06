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

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 168))

    ;; Memory data for string constant
    
    ;; String from 10 with 5 bytes in memory
    (data (i32.const 10) "Hello\00")

    ;; String from 16 with 5 bytes in memory
    (data (i32.const 16) "green\00")

    ;; String from 22 with 5 bytes in memory
    (data (i32.const 22) "green\00")

    ;; String from 28 with 6 bytes in memory
    (data (i32.const 28) "909090\00")

    ;; String from 35 with 5 bytes in memory
    (data (i32.const 35) "green\00")

    ;; String from 41 with 4 bytes in memory
    (data (i32.const 41) "size\00")

    ;; String from 46 with 5 bytes in memory
    (data (i32.const 46) "green\00")

    ;; String from 52 with 8 bytes in memory
    (data (i32.const 52) "not sure\00")

    ;; String from 61 with 3 bytes in memory
    (data (i32.const 61) "red\00")

    ;; String from 65 with 15 bytes in memory
    (data (i32.const 65) "this is message\00")

    ;; String from 81 with 36 bytes in memory
    (data (i32.const 81) "This is the optional parameter value\00")

    ;; String from 118 with 20 bytes in memory
    (data (i32.const 118) "Another string value\00")

    ;; String from 139 with 12 bytes in memory
    (data (i32.const 139) "345566777777\00")

    ;; String from 152 with 15 bytes in memory
    (data (i32.const 152) "this is message\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    

    ;; Global variables in this module
    

    ;; Export methods of this module
    ;; export from VB.NET module: [optionalParameterTest]
    
    (export "optionalParameterTest.calls" (func $optionalParameterTest.calls))
    
    
    ;; export from VB.NET module: [functionTest]
    
    (export "functionTest.outputError" (func $functionTest.outputError))
    (export "functionTest.calls" (func $functionTest.calls))
    (export "functionTest.extensionFunctiontest" (func $functionTest.extensionFunctiontest))
    (export "functionTest.Main" (func $functionTest.Main))
    
    
    ;; export from VB.NET module: [ExportAPiModule]
    
    (export "ExportAPiModule.outputError" (func $ExportAPiModule.outputError))
    
     

    ;; functions in [optionalParameterTest]
    
    (func $optionalParameterTest.calls  
        ;; Public Function calls() As void
        
    (local $obj i32)
    
    (set_local $obj (i32.const 10))
    (drop (call $optionalParameterTest.print (get_local $obj) (i32.const 16) (i64.const 99)))
    (drop (call $optionalParameterTest.print (get_local $obj) (i32.const 22) (i64.extend_s/i32 (i32.sub (i32.const 0) (i32.const 99)))))
    (drop (call $optionalParameterTest.print (call $string.add (get_local $obj) (i32.const 28)) (i32.const 35) (i64.const 99)))
    (drop (call $optionalParameterTest.print (i32.const 41) (i32.const 46) (i64.const 88)))
    (drop (call $optionalParameterTest.print (i32.const 52) (i32.const 61) (i64.trunc_s/f64 (f64.const 77.555))))
    )
    
    
    ;; functions in [functionTest]
    
    (func $functionTest.outputError  (result f32)
        ;; Public Function outputError() As f32
        
    
    
    (call $ExportAPiModule.err (i32.const 65))
    (return (f32.demote/f64 (f64.sub (f64.const 0) (f64.const 0.0001))))
    )
    (func $functionTest.calls  
        ;; Public Function calls() As void
        
    (local $x i64)
    
    (call $functionTest.Main (i32.const 81) (i32.const -100) (i32.const 1))
    (call $functionTest.Main (i32.const 118) (i32.trunc_s/f64 (f64.const 99999.9)) (i32.const 1))
    (drop (call $functionTest.outputError ))
    (set_local $x (i64.add (i64.trunc_s/f32 (call $functionTest.outputError )) (call $ExportAPiModule.outputError )))
    (call $optionalParameterTest.calls )
    (call $functionTest.calls )
    )
    (func $functionTest.extensionFunctiontest  
        ;; Public Function extensionFunctiontest() As void
        
    
    
    (drop (call $functionTest.print (i32.const 139)))
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
        
    
    
    (call $ExportAPiModule.err (i32.const 152))
    (return (i64.trunc_s/f64 (f64.sub (f64.const 0) (f64.const 10.0001))))
    )
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $optionalParameterTest.constructor )

(call $functionTest.constructor )

(call $ExportAPiModule.constructor )
)

(func $optionalParameterTest.constructor  
    ;; Public Function constructor() As void
    



)

(func $functionTest.constructor  
    ;; Public Function constructor() As void
    



)

(func $ExportAPiModule.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)