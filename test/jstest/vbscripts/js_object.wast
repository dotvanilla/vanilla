(module ;; Module js_object

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 6/2/2019 10:11:29 AM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function Math.pow Lib "Math" Alias "pow" (a As f64, b As f64) As f64
    (func $Math.pow (import "Math" "pow") (param $a f64) (param $b f64) (result f64))
;; Declare Function Math.sqrt Lib "Math" Alias "sqrt" (a As f64) As f64
    (func $Math.sqrt (import "Math" "sqrt") (param $a f64) (result f64))
;; Declare Function Math.exp Lib "Math" Alias "exp" (x As f64) As f64
    (func $Math.exp (import "Math" "exp") (param $x f64) (result f64))
;; Declare Function Math.cos Lib "Math" Alias "cos" (x As f64) As f64
    (func $Math.cos (import "Math" "cos") (param $x f64) (result f64))
;; Declare Function Math.random Lib "Math" Alias "random" () As f64
    (func $Math.random (import "Math" "random")  (result f64))
;; Declare Function Math.ceil Lib "Math" Alias "ceil" (x As f64) As f64
    (func $Math.ceil (import "Math" "ceil") (param $x f64) (result f64))
;; Declare Function Math.floor Lib "Math" Alias "floor" (x As f64) As f64
    (func $Math.floor (import "Math" "floor") (param $x f64) (result f64))
;; Declare Function GC.addObject Lib "GC" Alias "addObject" (offset As i32, class_id As i32) As void
    (func $GC.addObject (import "GC" "addObject") (param $offset i32) (param $class_id i32) )
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
    (global $global.ObjectManager (mut i32) (i32.const 1582))

    ;; memory allocate in javascript runtime
    (func $global.ObjectManager.Allocate (param $sizeof i32) (param $class_id i32) (result i32)
    ;; Public Function ObjectManager.Allocate(sizeof As i32, class_id As i32) As i32
    
(local $offset i32)

(set_local $offset (get_global $global.ObjectManager))
(set_global $global.ObjectManager (i32.add (get_local $offset) (get_local $sizeof)))
(call $GC.addObject (get_local $offset) (get_local $class_id))
(return (get_local $offset))
)

    ;; Memory data for string constant
        
    ;; String from 1328 with 28 bytes in memory
    (data (i32.const 1328) "directly create a structurte\00")
    
    ;; String from 1357 with 20 bytes in memory
    (data (i32.const 1357) "this is a structure!\00")
    
    ;; String from 1378 with 11 bytes in memory
    (data (i32.const 1378) "test object\00")
    
    ;; String from 1390 with 14 bytes in memory
    (data (i32.const 1390) "js_object demo\00")
    
    ;; String from 1405 with 55 bytes in memory
    (data (i32.const 1405) "Javascript object generate from VB.NET WebAssembly demo\00")
    
    ;; String from 1461 with 13 bytes in memory
    (data (i32.const 1461) "vanillavb.app\00")
    
    ;; String from 1475 with 9 bytes in memory
    (data (i32.const 1475) "js_object\00")
    
    ;; String from 1485 with 32 bytes in memory
    (data (i32.const 1485) "Copyright (c) vanillavb.app 2019\00")
    
    ;; String from 1518 with 10 bytes in memory
    (data (i32.const 1518) "dotvanilla\00")
    
    ;; String from 1529 with 36 bytes in memory
    (data (i32.const 1529) "62b3389d-5109-4740-9c6a-35bb022355b9\00")
    
    ;; String from 1566 with 7 bytes in memory
    (data (i32.const 1566) "1.0.0.0\00")
    
    ;; String from 1574 with 7 bytes in memory
    (data (i32.const 1574) "1.0.0.0\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 13 with 492 bytes in memory
    ;;
    ;; class [13] circle
    ;;
    (data (i32.const 13) "eyJjb21tZW50IjpudWxsLCJtZW1vcnlQdHIiOnsiVmFsdWUiOjEzfSwiY2xhc3MiOiJjaXJjbGUiLCJjbGFzc19pZCI6MTMsImZpZWxkcyI6eyJ4Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJmMzIiLCJ0eXBlIjozfSwieSI6eyJnZW5lcmljIjpbXSwicmF3IjoiZjMyIiwidHlwZSI6M30sInIiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImkzMiIsInR5cGUiOjF9LCJzdHJ1Y3QiOnsiZ2VuZXJpYyI6W10sInJhdyI6IlsxMDgzXXN0cnVjdFRlc3QiLCJ0eXBlIjoxMH0sInN0cnVjdDIiOnsiZ2VuZXJpYyI6W10sInJhdyI6IlsxMDgzXXN0cnVjdFRlc3QiLCJ0eXBlIjoxMH19LCJpc1N0cnVjdCI6ZmFsc2UsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOm51bGx9\00")
    
    ;; String from 498 with 584 bytes in memory
    ;;
    ;; class [498] rectangle
    ;;
    (data (i32.const 498) "eyJjb21tZW50IjpudWxsLCJtZW1vcnlQdHIiOnsiVmFsdWUiOjQ5OH0sImNsYXNzIjoicmVjdGFuZ2xlIiwiY2xhc3NfaWQiOjQ5OCwiZmllbGRzIjp7Im5hbWUiOnsiZ2VuZXJpYyI6W10sInJhdyI6InN0cmluZyIsInR5cGUiOjV9LCJoIjp7ImdlbmVyaWMiOltdLCJyYXciOiJpMzIiLCJ0eXBlIjoxfSwidyI6eyJnZW5lcmljIjpbXSwicmF3IjoiaTMyIiwidHlwZSI6MX0sInkiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImY2NCIsInR5cGUiOjR9LCJ4Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJmNjQiLCJ0eXBlIjo0fSwicmFkaXVzIjp7ImdlbmVyaWMiOltdLCJyYXciOiJmMzIiLCJ0eXBlIjozfSwiaW5uZXIiOnsiZ2VuZXJpYyI6W10sInJhdyI6IlsxM11jaXJjbGUiLCJ0eXBlIjoxMH19LCJpc1N0cnVjdCI6ZmFsc2UsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOm51bGx9\00")
    
    ;; String from 1083 with 244 bytes in memory
    ;;
    ;; structure [1083] structTest
    ;;
    (data (i32.const 1083) "eyJjb21tZW50IjpudWxsLCJtZW1vcnlQdHIiOnsiVmFsdWUiOjEwODN9LCJjbGFzcyI6InN0cnVjdFRlc3QiLCJjbGFzc19pZCI6MTA4MywiZmllbGRzIjp7Im5hbWUiOnsiZ2VuZXJpYyI6W10sInJhdyI6InN0cmluZyIsInR5cGUiOjV9fSwiaXNTdHJ1Y3QiOnRydWUsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOm51bGx9\00")

    ;; Pre-defined constant values
    (global $Math.E (mut f64) (f64.const 2.7182818284590451))
(global $Math.PI (mut f64) (f64.const 3.1415926535897931))
(global $Integer.MaxValue (mut i32) (i32.const 2147483647))
(global $Long.MaxValue (mut i64) (i64.const 9223372036854775807))
(global $Single.MaxValue (mut f32) (f32.const 3.40282347e+38))
(global $Double.MaxValue (mut f64) (f64.const 1.7976931348623157e+308))
(global $Integer.MinValue (mut i32) (i32.const -2147483648))
(global $Long.MinValue (mut i64) (i64.const -9223372036854775808))
(global $Single.MinValue (mut f32) (f32.const -3.40282347e+38))
(global $Double.MinValue (mut f64) (f64.const -1.7976931348623157e+308))

    ;; Global variables in this module
    (global $Module1.cx (mut f64) (f64.const 1000))
(global $Module1.test (mut i32) (i32.const 0))

    ;; Export methods of this module
    ;; export from VB.NET module: [Module1]
    
    (export "Module1.newCircle" (func $Module1.newCircle))
    (export "Module1.getObject" (func $Module1.getObject))
    
    
    ;; export from VB.NET module: [AssemblyInfo]
    
    (export "AssemblyInfo.AssemblyTitle" (func $AssemblyInfo.AssemblyTitle))
    (export "AssemblyInfo.AssemblyDescription" (func $AssemblyInfo.AssemblyDescription))
    (export "AssemblyInfo.AssemblyCompany" (func $AssemblyInfo.AssemblyCompany))
    (export "AssemblyInfo.AssemblyProduct" (func $AssemblyInfo.AssemblyProduct))
    (export "AssemblyInfo.AssemblyCopyright" (func $AssemblyInfo.AssemblyCopyright))
    (export "AssemblyInfo.AssemblyTrademark" (func $AssemblyInfo.AssemblyTrademark))
    (export "AssemblyInfo.Guid" (func $AssemblyInfo.Guid))
    (export "AssemblyInfo.AssemblyVersion" (func $AssemblyInfo.AssemblyVersion))
    (export "AssemblyInfo.AssemblyFileVersion" (func $AssemblyInfo.AssemblyFileVersion))
    
     

    ;; functions in [Module1]
    
    (func $Module1.newCircle  (result i32)
        ;; Public Function newCircle() As intptr
        
    (local $newObject_9a020000 i32)
    (local $newObject_9b020000 i32)
    (local $newObject_9c020000 i32)
    (local $memoryCopy_9d020000 i32)
    (local $memoryCopy_9e020000 i32)
    
    (set_global $Module1.cx (f64.mul (get_global $Module1.cx) (f64.convert_s/i32 (i32.const 2))))
    
    ;; Initialize a object instance of [[13]circle]
    ;; Object memory block begin at location: (get_local $newObject_9b020000)
    (set_local $newObject_9b020000 (call $global.ObjectManager.Allocate (i32.const 20) (i32.const 13)))
    ;; set field [circle::x]
    (f32.store (i32.add (get_local $newObject_9b020000) (i32.const 0)) (f32.demote/f64 (get_global $Module1.cx)))
    ;; set field [circle::y]
    (f32.store (i32.add (get_local $newObject_9b020000) (i32.const 4)) (f32.convert_s/i32 (i32.const 9999)))
    ;; Copy memory of structure value:
    (set_local $memoryCopy_9d020000 (i32.add (get_local $newObject_9b020000) (i32.const 12)))
    ;; set field [structTest::name]
    (i32.store (i32.add (get_local $memoryCopy_9d020000) (i32.const 0)) (i32.load (i32.add (call $Module1.newStruct ) (i32.const 0))))
    (set_local $newObject_9c020000 (call $global.ObjectManager.Allocate (i32.const 4) (i32.const 1083)))
    ;; set field [structTest::name]
    (i32.store (i32.add (get_local $newObject_9c020000) (i32.const 0)) (i32.const 1328))
    ;; Copy memory of structure value:
    (set_local $memoryCopy_9e020000 (i32.add (get_local $newObject_9b020000) (i32.const 16)))
    ;; set field [structTest::name]
    (i32.store (i32.add (get_local $memoryCopy_9e020000) (i32.const 0)) (i32.load (i32.add (get_local $newObject_9c020000) (i32.const 0))))
    ;; set field [circle::r]
    (i32.store (i32.add (get_local $newObject_9b020000) (i32.const 8)) (i32.const 100))
    ;; Initialize an object memory block with 20 bytes data
    
    (return (get_local $newObject_9b020000))
    )
    
    
    (func $Module1.newStruct  (result i32)
        ;; Public Function newStruct() As intptr
        
    (local $newObject_9f020000 i32)
    
    
    ;; Initialize a object instance of [[1083]structTest]
    ;; Object memory block begin at location: (get_local $newObject_9f020000)
    (set_local $newObject_9f020000 (call $global.ObjectManager.Allocate (i32.const 4) (i32.const 1083)))
    ;; set field [structTest::name]
    (i32.store (i32.add (get_local $newObject_9f020000) (i32.const 0)) (i32.const 1357))
    ;; Initialize an object memory block with 4 bytes data
    
    (return (get_local $newObject_9f020000))
    )
    
    
    (func $Module1.getObject  (result i32)
        ;; Public Function getObject() As intptr
        
    
    
    (return (get_global $Module1.test))
    )
    
    
    
    
    ;; functions in [AssemblyInfo]
    
    (func $AssemblyInfo.AssemblyTitle  (result i32)
        ;; Public Function AssemblyTitle() As string
        
    
    
    (return (i32.const 1390))
    )
    
    
    (func $AssemblyInfo.AssemblyDescription  (result i32)
        ;; Public Function AssemblyDescription() As string
        
    
    
    (return (i32.const 1405))
    )
    
    
    (func $AssemblyInfo.AssemblyCompany  (result i32)
        ;; Public Function AssemblyCompany() As string
        
    
    
    (return (i32.const 1461))
    )
    
    
    (func $AssemblyInfo.AssemblyProduct  (result i32)
        ;; Public Function AssemblyProduct() As string
        
    
    
    (return (i32.const 1475))
    )
    
    
    (func $AssemblyInfo.AssemblyCopyright  (result i32)
        ;; Public Function AssemblyCopyright() As string
        
    
    
    (return (i32.const 1485))
    )
    
    
    (func $AssemblyInfo.AssemblyTrademark  (result i32)
        ;; Public Function AssemblyTrademark() As string
        
    
    
    (return (i32.const 1518))
    )
    
    
    (func $AssemblyInfo.Guid  (result i32)
        ;; Public Function Guid() As string
        
    
    
    (return (i32.const 1529))
    )
    
    
    (func $AssemblyInfo.AssemblyVersion  (result i32)
        ;; Public Function AssemblyVersion() As string
        
    
    
    (return (i32.const 1566))
    )
    
    
    (func $AssemblyInfo.AssemblyFileVersion  (result i32)
        ;; Public Function AssemblyFileVersion() As string
        
    
    
    (return (i32.const 1574))
    )
    
    
    


    ;; Application Initialize
    ;; 
    ;; Sub New
    (func $Application_SubNew
        ;; call of the global variable initialize
        (call $global.initializer )

        (call $Module1.constructor )
    )

    ;; Initializer for global variables if it is not a primitive abstract type
    (func $global.initializer  
    ;; Public Function initializer() As void
    
(local $newObject_9a020000 i32)


;; Initialize a object instance of [[498]rectangle]
;; Object memory block begin at location: (get_local $newObject_9a020000)
(set_local $newObject_9a020000 (call $global.ObjectManager.Allocate (i32.const 36) (i32.const 498)))
;; set field [rectangle::x]
(f64.store (i32.add (get_local $newObject_9a020000) (i32.const 20)) (f64.convert_s/i32 (i32.const 2147483647)))
;; set field [rectangle::y]
(f64.store (i32.add (get_local $newObject_9a020000) (i32.const 12)) (f64.convert_s/i32 (i32.const 10)))
;; set field [rectangle::name]
(i32.store (i32.add (get_local $newObject_9a020000) (i32.const 0)) (i32.const 0))
;; set field [rectangle::h]
(i32.store (i32.add (get_local $newObject_9a020000) (i32.const 4)) (i32.const 1000))
;; set field [rectangle::w]
(i32.store (i32.add (get_local $newObject_9a020000) (i32.const 8)) (i32.const 1000))
;; set field [rectangle::radius]
(f32.store (i32.add (get_local $newObject_9a020000) (i32.const 28)) (f32.const -99))
;; set field [rectangle::inner]
(i32.store (i32.add (get_local $newObject_9a020000) (i32.const 32)) (i32.const 0))
;; Initialize an object memory block with 36 bytes data

(set_global $Module1.test (get_local $newObject_9a020000))
)

    (func $Module1.constructor  
    ;; Public Function constructor() As void
    


(i32.store (i32.add (get_global $Module1.test) (i32.const 0)) (i32.const 1378))
(i32.store (i32.add (get_global $Module1.test) (i32.const 32)) (call $Module1.newCircle ))
)

    (start $Application_SubNew)
)