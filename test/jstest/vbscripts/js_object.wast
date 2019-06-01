(module ;; Module js_object

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/26/2019 4:24:57 PM
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
    (global $global.ObjectManager (mut i32) (i32.const 1585))

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
        
    ;; String from 1360 with 20 bytes in memory
    (data (i32.const 1360) "this is a structure!\00")
    
    ;; String from 1381 with 11 bytes in memory
    (data (i32.const 1381) "test object\00")
    
    ;; String from 1393 with 14 bytes in memory
    (data (i32.const 1393) "js_object demo\00")
    
    ;; String from 1408 with 55 bytes in memory
    (data (i32.const 1408) "Javascript object generate from VB.NET WebAssembly demo\00")
    
    ;; String from 1464 with 13 bytes in memory
    (data (i32.const 1464) "vanillavb.app\00")
    
    ;; String from 1478 with 9 bytes in memory
    (data (i32.const 1478) "js_object\00")
    
    ;; String from 1488 with 32 bytes in memory
    (data (i32.const 1488) "Copyright (c) vanillavb.app 2019\00")
    
    ;; String from 1521 with 10 bytes in memory
    (data (i32.const 1521) "dotvanilla\00")
    
    ;; String from 1532 with 36 bytes in memory
    (data (i32.const 1532) "62b3389d-5109-4740-9c6a-35bb022355b9\00")
    
    ;; String from 1569 with 7 bytes in memory
    (data (i32.const 1569) "1.0.0.0\00")
    
    ;; String from 1577 with 7 bytes in memory
    (data (i32.const 1577) "1.0.0.0\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 13 with 412 bytes in memory
    ;;
    ;; class [13] circle
    ;;
    (data (i32.const 13) "eyJjb21tZW50IjpudWxsLCJtZW1vcnlQdHIiOnsiVmFsdWUiOjEzfSwiY2xhc3MiOiJjaXJjbGUiLCJjbGFzc19pZCI6MTMsImZpZWxkcyI6eyJ4Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJmMzIiLCJ0eXBlIjozfSwieSI6eyJnZW5lcmljIjpbXSwicmF3IjoiZjMyIiwidHlwZSI6M30sInIiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImkzMiIsInR5cGUiOjF9LCJzdHJ1Y3QiOnsiZ2VuZXJpYyI6W10sInJhdyI6IlsxMDA3XXN0cnVjdFRlc3QiLCJ0eXBlIjoxMH19LCJpc1N0cnVjdCI6ZmFsc2UsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOm51bGx9\00")
    
    ;; String from 422 with 584 bytes in memory
    ;;
    ;; class [422] rectangle
    ;;
    (data (i32.const 422) "eyJjb21tZW50IjpudWxsLCJtZW1vcnlQdHIiOnsiVmFsdWUiOjQyMn0sImNsYXNzIjoicmVjdGFuZ2xlIiwiY2xhc3NfaWQiOjQyMiwiZmllbGRzIjp7ImgiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImkzMiIsInR5cGUiOjF9LCJ3Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJpMzIiLCJ0eXBlIjoxfSwieSI6eyJnZW5lcmljIjpbXSwicmF3IjoiZjY0IiwidHlwZSI6NH0sIngiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImY2NCIsInR5cGUiOjR9LCJuYW1lIjp7ImdlbmVyaWMiOltdLCJyYXciOiJzdHJpbmciLCJ0eXBlIjo1fSwicmFkaXVzIjp7ImdlbmVyaWMiOltdLCJyYXciOiJmMzIiLCJ0eXBlIjozfSwiaW5uZXIiOnsiZ2VuZXJpYyI6W10sInJhdyI6IlsxM11jaXJjbGUiLCJ0eXBlIjoxMH19LCJpc1N0cnVjdCI6ZmFsc2UsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOm51bGx9\00")
    
    ;; String from 1007 with 352 bytes in memory
    ;;
    ;; structure [1007] structTest
    ;;
    (data (i32.const 1007) "eyJjb21tZW50IjpudWxsLCJtZW1vcnlQdHIiOnsiVmFsdWUiOjEwMDd9LCJjbGFzcyI6InN0cnVjdFRlc3QiLCJjbGFzc19pZCI6MTAwNywiZmllbGRzIjp7ImFycmF5Ijp7ImdlbmVyaWMiOlt7ImdlbmVyaWMiOltdLCJyYXciOiJmNjQiLCJ0eXBlIjo0fV0sInJhdyI6ImY2NFtdIiwidHlwZSI6N30sIm5hbWUiOnsiZ2VuZXJpYyI6W10sInJhdyI6InN0cmluZyIsInR5cGUiOjV9fSwiaXNTdHJ1Y3QiOnRydWUsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOm51bGx9\00")

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
    (global $Module1.test (mut i32) (i32.const 0))

    ;; Export methods of this module
    ;; export from VB.NET module: [Module1]
    
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
    
    
    ;; Initialize a object instance of [[13]circle]
    ;; Object memory block begin at location: (get_local $newObject_9b020000)
    (set_local $newObject_9b020000 (get_global $global.ObjectManager))
    ;; Offset object manager with 20 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $newObject_9b020000) (i32.const 20)))
    ;; set field [circle::x]
    (f32.store (i32.add (get_local $newObject_9b020000) (i32.const 0)) (f32.convert_s/i32 (i32.const 100)))
    ;; set field [circle::y]
    (f32.store (i32.add (get_local $newObject_9b020000) (i32.const 4)) (f32.convert_s/i32 (i32.const 9999)))
    ;; set field [circle::struct]
    (i32.store (i32.add (get_local $newObject_9b020000) (i32.const 12)) (call $Module1.newStruct ))
    ;; set field [circle::r]
    (i32.store (i32.add (get_local $newObject_9b020000) (i32.const 8)) (i32.const 100))
    ;; Initialize an object memory block with 20 bytes data
    
    (return (get_local $newObject_9b020000))
    )
    
    
    (func $Module1.newStruct  (result i32)
        ;; Public Function newStruct() As intptr
        
    (local $newObject_9c020000 i32)
    
    
    ;; Initialize a object instance of [[1007]structTest]
    ;; Object memory block begin at location: (get_local $newObject_9c020000)
    (set_local $newObject_9c020000 (get_global $global.ObjectManager))
    ;; Offset object manager with 8 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $newObject_9c020000) (i32.const 8)))
    ;; set field [structTest::array]
    (i32.store (i32.add (get_local $newObject_9c020000) (i32.const 0)) (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $array.new (i32.const -1)) (f64.convert_s/i32 (i32.const 1))) (f64.convert_s/i32 (i32.const 2))) (f64.convert_s/i32 (i32.const 3))) (f64.convert_s/i32 (i32.const 4))) (f64.convert_s/i32 (i32.const 5))))
    ;; set field [structTest::name]
    (i32.store (i32.add (get_local $newObject_9c020000) (i32.const 4)) (i32.const 1360))
    ;; Initialize an object memory block with 8 bytes data
    
    (return (get_local $newObject_9c020000))
    )
    
    
    (func $Module1.getObject  (result i32)
        ;; Public Function getObject() As intptr
        
    
    
    (return (get_global $Module1.test))
    )
    
    
    
    
    ;; functions in [AssemblyInfo]
    
    (func $AssemblyInfo.AssemblyTitle  (result i32)
        ;; Public Function AssemblyTitle() As string
        
    
    
    (return (i32.const 1393))
    )
    
    
    (func $AssemblyInfo.AssemblyDescription  (result i32)
        ;; Public Function AssemblyDescription() As string
        
    
    
    (return (i32.const 1408))
    )
    
    
    (func $AssemblyInfo.AssemblyCompany  (result i32)
        ;; Public Function AssemblyCompany() As string
        
    
    
    (return (i32.const 1464))
    )
    
    
    (func $AssemblyInfo.AssemblyProduct  (result i32)
        ;; Public Function AssemblyProduct() As string
        
    
    
    (return (i32.const 1478))
    )
    
    
    (func $AssemblyInfo.AssemblyCopyright  (result i32)
        ;; Public Function AssemblyCopyright() As string
        
    
    
    (return (i32.const 1488))
    )
    
    
    (func $AssemblyInfo.AssemblyTrademark  (result i32)
        ;; Public Function AssemblyTrademark() As string
        
    
    
    (return (i32.const 1521))
    )
    
    
    (func $AssemblyInfo.Guid  (result i32)
        ;; Public Function Guid() As string
        
    
    
    (return (i32.const 1532))
    )
    
    
    (func $AssemblyInfo.AssemblyVersion  (result i32)
        ;; Public Function AssemblyVersion() As string
        
    
    
    (return (i32.const 1569))
    )
    
    
    (func $AssemblyInfo.AssemblyFileVersion  (result i32)
        ;; Public Function AssemblyFileVersion() As string
        
    
    
    (return (i32.const 1577))
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


;; Initialize a object instance of [[422]rectangle]
;; Object memory block begin at location: (get_local $newObject_9a020000)
(set_local $newObject_9a020000 (get_global $global.ObjectManager))
;; Offset object manager with 36 bytes.
(set_global $global.ObjectManager (i32.add (get_local $newObject_9a020000) (i32.const 36)))
;; set field [rectangle::x]
(f64.store (i32.add (get_local $newObject_9a020000) (i32.const 16)) (f64.convert_s/i32 (i32.const 2147483647)))
;; set field [rectangle::y]
(f64.store (i32.add (get_local $newObject_9a020000) (i32.const 8)) (f64.convert_s/i32 (i32.const 10)))
;; set field [rectangle::h]
(i32.store (i32.add (get_local $newObject_9a020000) (i32.const 0)) (i32.const 1000))
;; set field [rectangle::w]
(i32.store (i32.add (get_local $newObject_9a020000) (i32.const 4)) (i32.const 1000))
;; set field [rectangle::name]
(i32.store (i32.add (get_local $newObject_9a020000) (i32.const 24)) (i32.const 0))
;; set field [rectangle::radius]
(f32.store (i32.add (get_local $newObject_9a020000) (i32.const 28)) (f32.const -99))
;; set field [rectangle::inner]
(i32.store (i32.add (get_local $newObject_9a020000) (i32.const 32)) (i32.const 0))
;; Initialize an object memory block with 36 bytes data

(set_global $Module1.test (get_local $newObject_9a020000))
)

    (func $Module1.constructor  
    ;; Public Function constructor() As void
    


(i32.store (i32.add (get_global $Module1.test) (i32.const 24)) (i32.const 1381))
(i32.store (i32.add (get_global $Module1.test) (i32.const 32)) (call $Module1.newCircle ))
)

    (start $Application_SubNew)
)