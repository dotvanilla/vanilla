(module ;; Module js_object

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 6/3/2019 10:46:34 PM
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
    (global $global.ObjectManager (mut i32) (i32.const 1808))

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
        
    ;; String from 1144 with 8 bytes in memory
    (data (i32.const 1144) "99999999\00")
    
    ;; String from 1424 with 28 bytes in memory
    (data (i32.const 1424) "directly create a structurte\00")
    
    ;; String from 1456 with 20 bytes in memory
    (data (i32.const 1456) "this is a structure!\00")
    
    ;; String from 1480 with 11 bytes in memory
    (data (i32.const 1480) "test object\00")
    
    ;; String from 1496 with 8 bytes in memory
    (data (i32.const 1496) "AAAAAAAA\00")
    
    ;; String from 1512 with 8 bytes in memory
    (data (i32.const 1512) "BBBBBBBB\00")
    
    ;; String from 1528 with 10 bytes in memory
    (data (i32.const 1528) "CCCCCCCCCC\00")
    
    ;; String from 1544 with 10 bytes in memory
    (data (i32.const 1544) "DDDDDDDDDD\00")
    
    ;; String from 1560 with 2 bytes in memory
    (data (i32.const 1560) "EE\00")
    
    ;; String from 1568 with 7 bytes in memory
    (data (i32.const 1568) "FFFFFFF\00")
    
    ;; String from 1576 with 8 bytes in memory
    (data (i32.const 1576) "GGGGGGGG\00")
    
    ;; String from 1592 with 14 bytes in memory
    (data (i32.const 1592) "js_object demo\00")
    
    ;; String from 1608 with 55 bytes in memory
    (data (i32.const 1608) "Javascript object generate from VB.NET WebAssembly demo\00")
    
    ;; String from 1664 with 13 bytes in memory
    (data (i32.const 1664) "vanillavb.app\00")
    
    ;; String from 1680 with 9 bytes in memory
    (data (i32.const 1680) "js_object\00")
    
    ;; String from 1696 with 32 bytes in memory
    (data (i32.const 1696) "Copyright (c) vanillavb.app 2019\00")
    
    ;; String from 1736 with 10 bytes in memory
    (data (i32.const 1736) "dotvanilla\00")
    
    ;; String from 1752 with 36 bytes in memory
    (data (i32.const 1752) "62b3389d-5109-4740-9c6a-35bb022355b9\00")
    
    ;; String from 1792 with 7 bytes in memory
    (data (i32.const 1792) "1.0.0.0\00")
    
    ;; String from 1800 with 7 bytes in memory
    (data (i32.const 1800) "1.0.0.0\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 13 with 444 bytes in memory
    ;;
    ;; class [13] circle
    ;;
    (data (i32.const 13) "eyJjb21tZW50IjpudWxsLCJjbGFzcyI6ImNpcmNsZSIsImNsYXNzX2lkIjoxMywiZmllbGRzIjp7IngiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImYzMiIsInR5cGUiOjN9LCJ5Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJmMzIiLCJ0eXBlIjozfSwiciI6eyJnZW5lcmljIjpbXSwicmF3IjoiaTMyIiwidHlwZSI6MX0sIm5hbWVPZlgiOnsiZ2VuZXJpYyI6W10sInJhdyI6IlsxMTYwXW5hbWUiLCJ0eXBlIjoxMH0sIm5hbWVPZlkiOnsiZ2VuZXJpYyI6W10sInJhdyI6IlsxMTYwXW5hbWUiLCJ0eXBlIjoxMH19LCJpc1N0cnVjdCI6ZmFsc2UsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOm51bGx9\00")
    
    ;; String from 520 with 556 bytes in memory
    ;;
    ;; class [520] rectangle
    ;;
    (data (i32.const 520) "eyJjb21tZW50IjpudWxsLCJjbGFzcyI6InJlY3RhbmdsZSIsImNsYXNzX2lkIjo1MjAsImZpZWxkcyI6eyJuYW1lIjp7ImdlbmVyaWMiOltdLCJyYXciOiJbMTE2MF1uYW1lIiwidHlwZSI6MTB9LCJoIjp7ImdlbmVyaWMiOltdLCJyYXciOiJpMzIiLCJ0eXBlIjoxfSwidyI6eyJnZW5lcmljIjpbXSwicmF3IjoiaTMyIiwidHlwZSI6MX0sInkiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImY2NCIsInR5cGUiOjR9LCJ4Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJmNjQiLCJ0eXBlIjo0fSwicmFkaXVzIjp7ImdlbmVyaWMiOltdLCJyYXciOiJmMzIiLCJ0eXBlIjozfSwiaW5uZXIiOnsiZ2VuZXJpYyI6W10sInJhdyI6IlsxM11jaXJjbGUiLCJ0eXBlIjoxMH19LCJpc1N0cnVjdCI6ZmFsc2UsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOm51bGx9\00")
    
    ;; String from 1160 with 260 bytes in memory
    ;;
    ;; structure [1160] name
    ;;
    (data (i32.const 1160) "eyJjb21tZW50IjpudWxsLCJjbGFzcyI6Im5hbWUiLCJjbGFzc19pZCI6MTE2MCwiZmllbGRzIjp7InNvdXJjZSI6eyJnZW5lcmljIjpbXSwicmF3IjoiaTMyIiwidHlwZSI6MX0sIm5hbWUiOnsiZ2VuZXJpYyI6W10sInJhdyI6InN0cmluZyIsInR5cGUiOjV9fSwiaXNTdHJ1Y3QiOnRydWUsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOm51bGx9\00")

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
    (global $objectGC.cx (mut f64) (f64.const 1000))
(global $objectGC.test (mut i32) (i32.const 0))
(global $name.demoNameString (mut i32) (i32.const 1144))

    ;; Export methods of this module
    ;; export from VB.NET module: [objectGC]
    
    (export "objectGC.newCircle" (func $objectGC.newCircle))
    (export "objectGC.getObject" (func $objectGC.getObject))
    
    
    ;; export from VB.NET module: [numberArray]
    
    (export "numberArray.createVector" (func $numberArray.createVector))
    (export "numberArray.namesVector" (func $numberArray.namesVector))
    (export "numberArray.structures" (func $numberArray.structures))
    
    
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
    
     

    ;; functions in [objectGC]
    
    (func $objectGC.newCircle  (result i32)
        ;; Public Function newCircle() As intptr
        
    (local $newObject_9a020000 i32)
    (local $newObject_9b020000 i32)
    (local $newObject_9c020000 i32)
    (local $memoryCopyTo_9d020000 i32)
    (local $memorySource_9e020000 i32)
    (local $memoryCopyTo_9f020000 i32)
    (local $memorySource_a0020000 i32)
    
    (set_global $objectGC.cx (f64.mul (get_global $objectGC.cx) (f64.convert_s/i32 (i32.const 2))))
    
    ;; Initialize a object instance of [[13]circle]
    ;; Object memory block begin at location: (get_local $newObject_9b020000)
    (set_local $newObject_9b020000 (call $global.ObjectManager.Allocate (i32.const 28) (i32.const 13)))
    ;; set field [circle::x]
    (f32.store (i32.add (get_local $newObject_9b020000) (i32.const 0)) (f32.demote/f64 (get_global $objectGC.cx)))
    ;; set field [circle::y]
    (f32.store (i32.add (get_local $newObject_9b020000) (i32.const 4)) (f32.convert_s/i32 (i32.const 9999)))
    ;; Copy memory of structure value:
    (set_local $memorySource_9e020000 (call $objectGC.newStruct ))
    (set_local $memoryCopyTo_9d020000 (i32.add (get_local $newObject_9b020000) (i32.const 12)))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $memoryCopyTo_9d020000) (i32.const 0)) (i32.load (i32.add (get_local $memorySource_9e020000) (i32.const 0))))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $memoryCopyTo_9d020000) (i32.const 4)) (i32.load (i32.add (get_local $memorySource_9e020000) (i32.const 4))))
    (set_local $newObject_9c020000 (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 1160)))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $newObject_9c020000) (i32.const 4)) (i32.const 1424))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $newObject_9c020000) (i32.const 0)) (i32.const 0))
    ;; Copy memory of structure value:
    (set_local $memorySource_a0020000 (get_local $newObject_9c020000))
    (set_local $memoryCopyTo_9f020000 (i32.add (get_local $newObject_9b020000) (i32.const 20)))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $memoryCopyTo_9f020000) (i32.const 0)) (i32.load (i32.add (get_local $memorySource_a0020000) (i32.const 0))))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $memoryCopyTo_9f020000) (i32.const 4)) (i32.load (i32.add (get_local $memorySource_a0020000) (i32.const 4))))
    ;; set field [circle::r]
    (i32.store (i32.add (get_local $newObject_9b020000) (i32.const 8)) (i32.const 100))
    ;; Initialize an object memory block with 28 bytes data
    
    (return (get_local $newObject_9b020000))
    )
    
    
    (func $objectGC.newStruct  (result i32)
        ;; Public Function newStruct() As intptr
        
    (local $newObject_a1020000 i32)
    
    
    ;; Initialize a object instance of [[1160]name]
    ;; Object memory block begin at location: (get_local $newObject_a1020000)
    (set_local $newObject_a1020000 (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 1160)))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $newObject_a1020000) (i32.const 4)) (i32.const 1456))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $newObject_a1020000) (i32.const 0)) (i32.const 111111))
    ;; Initialize an object memory block with 8 bytes data
    
    (return (get_local $newObject_a1020000))
    )
    
    
    (func $objectGC.getObject  (result i32)
        ;; Public Function getObject() As intptr
        
    
    
    (return (get_global $objectGC.test))
    )
    
    
    
    
    ;; functions in [numberArray]
    
    (func $numberArray.createVector  (result i32)
        ;; Public Function createVector() As array(Of i32)
        
    (local $newObject_a2020000 i32)
    (local $arrayOffset_a3020000 i32)
    (local $itemOffset_a4020000 i32)
    
    
    ;; Save (i32.const 10) array element data to memory:
    ;; Array memory block begin at location: (get_local $arrayOffset_a3020000)
    (set_local $arrayOffset_a3020000 (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 10) (i32.const 4))) (i32.const 7)))
    ;; class_id/typealias_enum i32 data: (i32.const 1)/array(Of i32)
    (i32.store (get_local $arrayOffset_a3020000) (i32.const 1))
    (i32.store (i32.add (get_local $arrayOffset_a3020000) (i32.const 4)) (i32.const 10))
    ;; End of byte marks meta data, start write data blocks
    (set_local $itemOffset_a4020000 (i32.add (get_local $arrayOffset_a3020000) (i32.const 8)))
    (i32.store (i32.add (get_local $itemOffset_a4020000) (i32.const 0)) (i32.const 1))
    (i32.store (i32.add (get_local $itemOffset_a4020000) (i32.const 4)) (i32.const 2))
    (i32.store (i32.add (get_local $itemOffset_a4020000) (i32.const 8)) (i32.const 3))
    (i32.store (i32.add (get_local $itemOffset_a4020000) (i32.const 12)) (i32.const 4))
    (i32.store (i32.add (get_local $itemOffset_a4020000) (i32.const 16)) (i32.const 5))
    (i32.store (i32.add (get_local $itemOffset_a4020000) (i32.const 20)) (i32.const 6))
    (i32.store (i32.add (get_local $itemOffset_a4020000) (i32.const 24)) (i32.const 7))
    (i32.store (i32.add (get_local $itemOffset_a4020000) (i32.const 28)) (i32.const 8))
    (i32.store (i32.add (get_local $itemOffset_a4020000) (i32.const 32)) (i32.const 9))
    (i32.store (i32.add (get_local $itemOffset_a4020000) (i32.const 36)) (i32.const 0))
    ;; Assign array memory data to another expression
    (return (get_local $arrayOffset_a3020000))
    )
    
    
    (func $numberArray.namesVector  (result i32)
        ;; Public Function namesVector() As array(Of string)
        
    (local $arrayOffset_a5020000 i32)
    (local $itemOffset_a6020000 i32)
    
    
    ;; Save (i32.const 7) array element data to memory:
    ;; Array memory block begin at location: (get_local $arrayOffset_a5020000)
    (set_local $arrayOffset_a5020000 (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 7) (i32.const 4))) (i32.const 7)))
    ;; class_id/typealias_enum i32 data: (i32.const 5)/array(Of string)
    (i32.store (get_local $arrayOffset_a5020000) (i32.const 5))
    (i32.store (i32.add (get_local $arrayOffset_a5020000) (i32.const 4)) (i32.const 7))
    ;; End of byte marks meta data, start write data blocks
    (set_local $itemOffset_a6020000 (i32.add (get_local $arrayOffset_a5020000) (i32.const 8)))
    (i32.store (i32.add (get_local $itemOffset_a6020000) (i32.const 0)) (i32.const 1496))
    (i32.store (i32.add (get_local $itemOffset_a6020000) (i32.const 4)) (i32.const 1512))
    (i32.store (i32.add (get_local $itemOffset_a6020000) (i32.const 8)) (i32.const 1528))
    (i32.store (i32.add (get_local $itemOffset_a6020000) (i32.const 12)) (i32.const 1544))
    (i32.store (i32.add (get_local $itemOffset_a6020000) (i32.const 16)) (i32.const 1560))
    (i32.store (i32.add (get_local $itemOffset_a6020000) (i32.const 20)) (i32.const 1568))
    (i32.store (i32.add (get_local $itemOffset_a6020000) (i32.const 24)) (i32.const 1576))
    ;; Assign array memory data to another expression
    (return (get_local $arrayOffset_a5020000))
    )
    
    
    (func $numberArray.structures  (result i32)
        ;; Public Function structures() As array(Of intptr)
        
    (local $names i32)
    (local $newObject_a7020000 i32)
    (local $newObject_a8020000 i32)
    (local $newObject_a9020000 i32)
    (local $arrayOffset_aa020000 i32)
    (local $itemOffset_ab020000 i32)
    (local $structCopyOf_ac020000 i32)
    (local $structCopyOf_ad020000 i32)
    (local $structCopyOf_ae020000 i32)
    
    (set_local $names (call $numberArray.namesVector ))
    
    ;; Save (i32.const 3) array element data to memory:
    ;; Array memory block begin at location: (get_local $arrayOffset_aa020000)
    (set_local $arrayOffset_aa020000 (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 3) (i32.const 8))) (i32.const 7)))
    ;; class_id/typealias_enum i32 data: (i32.const 1160)/array(Of intptr)
    (i32.store (get_local $arrayOffset_aa020000) (i32.const 1160))
    (i32.store (i32.add (get_local $arrayOffset_aa020000) (i32.const 4)) (i32.const 3))
    ;; End of byte marks meta data, start write data blocks
    (set_local $itemOffset_ab020000 (i32.add (get_local $arrayOffset_aa020000) (i32.const 8)))
    (set_local $structCopyOf_ac020000 (i32.add (get_local $itemOffset_ab020000) (i32.const 0)))
    (set_local $newObject_a7020000 (get_local $structCopyOf_ac020000))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $newObject_a7020000) (i32.const 4)) (i32.load (i32.add (i32.add (get_local $names) (i32.const 8)) (i32.mul (i32.const 0) (i32.const 4)))))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $newObject_a7020000) (i32.const 0)) (i32.const 0))
    (set_local $structCopyOf_ad020000 (i32.add (get_local $itemOffset_ab020000) (i32.const 8)))
    (set_local $newObject_a8020000 (get_local $structCopyOf_ad020000))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $newObject_a8020000) (i32.const 4)) (i32.load (i32.add (i32.add (get_local $names) (i32.const 8)) (i32.mul (i32.const 1) (i32.const 4)))))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $newObject_a8020000) (i32.const 0)) (i32.const 1))
    (set_local $structCopyOf_ae020000 (i32.add (get_local $itemOffset_ab020000) (i32.const 16)))
    (set_local $newObject_a9020000 (get_local $structCopyOf_ae020000))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $newObject_a9020000) (i32.const 4)) (i32.load (i32.add (i32.add (get_local $names) (i32.const 8)) (i32.mul (i32.const 2) (i32.const 4)))))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $newObject_a9020000) (i32.const 0)) (i32.const 2))
    ;; Assign array memory data to another expression
    (return (get_local $arrayOffset_aa020000))
    )
    
    
    
    
    ;; functions in [AssemblyInfo]
    
    (func $AssemblyInfo.AssemblyTitle  (result i32)
        ;; Public Function AssemblyTitle() As string
        
    
    
    (return (i32.const 1592))
    )
    
    
    (func $AssemblyInfo.AssemblyDescription  (result i32)
        ;; Public Function AssemblyDescription() As string
        
    
    
    (return (i32.const 1608))
    )
    
    
    (func $AssemblyInfo.AssemblyCompany  (result i32)
        ;; Public Function AssemblyCompany() As string
        
    
    
    (return (i32.const 1664))
    )
    
    
    (func $AssemblyInfo.AssemblyProduct  (result i32)
        ;; Public Function AssemblyProduct() As string
        
    
    
    (return (i32.const 1680))
    )
    
    
    (func $AssemblyInfo.AssemblyCopyright  (result i32)
        ;; Public Function AssemblyCopyright() As string
        
    
    
    (return (i32.const 1696))
    )
    
    
    (func $AssemblyInfo.AssemblyTrademark  (result i32)
        ;; Public Function AssemblyTrademark() As string
        
    
    
    (return (i32.const 1736))
    )
    
    
    (func $AssemblyInfo.Guid  (result i32)
        ;; Public Function Guid() As string
        
    
    
    (return (i32.const 1752))
    )
    
    
    (func $AssemblyInfo.AssemblyVersion  (result i32)
        ;; Public Function AssemblyVersion() As string
        
    
    
    (return (i32.const 1792))
    )
    
    
    (func $AssemblyInfo.AssemblyFileVersion  (result i32)
        ;; Public Function AssemblyFileVersion() As string
        
    
    
    (return (i32.const 1800))
    )
    
    
    


    ;; Application Initialize
    ;; 
    ;; Sub New
    (func $Application_SubNew
        ;; call of the global variable initialize
        (call $global.initializer )

        (call $objectGC.constructor )

(call $numberArray.constructor )
    )

    ;; Initializer for global variables if it is not a primitive abstract type
    (func $global.initializer  
    ;; Public Function initializer() As void
    
(local $newObject_9a020000 i32)


;; Initialize a object instance of [[520]rectangle]
;; Object memory block begin at location: (get_local $newObject_9a020000)
(set_local $newObject_9a020000 (call $global.ObjectManager.Allocate (i32.const 40) (i32.const 520)))
;; set field [rectangle::x]
(f64.store (i32.add (get_local $newObject_9a020000) (i32.const 24)) (f64.convert_s/i32 (i32.const 2147483647)))
;; set field [rectangle::y]
(f64.store (i32.add (get_local $newObject_9a020000) (i32.const 16)) (f64.convert_s/i32 (i32.const 10)))
;; Structure value is nothing!
;; set field [rectangle::h]
(i32.store (i32.add (get_local $newObject_9a020000) (i32.const 8)) (i32.const 1000))
;; set field [rectangle::w]
(i32.store (i32.add (get_local $newObject_9a020000) (i32.const 12)) (i32.const 1000))
;; set field [rectangle::radius]
(f32.store (i32.add (get_local $newObject_9a020000) (i32.const 32)) (f32.const -99))
;; set field [rectangle::inner]
(i32.store (i32.add (get_local $newObject_9a020000) (i32.const 36)) (i32.const 0))
;; Initialize an object memory block with 40 bytes data

(set_global $objectGC.test (get_local $newObject_9a020000))
)

    (func $objectGC.constructor  
    ;; Public Function constructor() As void
    
(local $newObject_a2020000 i32)

(set_local $newObject_a2020000 (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 1160)))
;; set field [name::name]
(i32.store (i32.add (get_local $newObject_a2020000) (i32.const 4)) (i32.const 1480))
;; set field [name::source]
(i32.store (i32.add (get_local $newObject_a2020000) (i32.const 0)) (i32.const 888888888))
(i32.store (i32.add (get_global $objectGC.test) (i32.const 0)) (get_local $newObject_a2020000))
(i32.store (i32.add (get_global $objectGC.test) (i32.const 36)) (call $objectGC.newCircle ))
)

(func $numberArray.constructor  
    ;; Public Function constructor() As void
    



)

    (start $Application_SubNew)
)