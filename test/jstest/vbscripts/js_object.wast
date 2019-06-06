(module ;; Module js_object

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 6/7/2019 12:07:51 AM
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
;; Declare Function print Lib "console" Alias "log" (item As intptr) As void
    (func $loopOnArray.print (import "console" "log") (param $item i32) )
;; Declare Function println Lib "console" Alias "log" (info As string) As void
    (func $loopOnArray.println (import "console" "log") (param $info i32) )
;; Declare Function i32.toString Lib "string" Alias "toString" (x As i32) As string
    (func $i32.toString (import "string" "toString") (param $x i32) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 1840))

    ;; memory allocate in javascript runtime
    (func $global.ObjectManager.Allocate (param $sizeof i32) (param $class_id i32) (result i32)
    ;; Public Function ObjectManager.Allocate(sizeof As i32, class_id As i32) As i32
    
(local $offset i32)

(set_local $offset (get_global $global.ObjectManager))
(set_global $global.ObjectManager (i32.add (get_local $offset) (get_local $sizeof)))
(call $GC.addObject (get_local $offset) (get_local $class_id))
(return (get_local $offset))
)
    (func $global.GetMemorySize  (result i32)
    ;; Public Function GetMemorySize() As i32
    


(return (get_global $global.ObjectManager))
)

    ;; Memory data for string constant
        
    ;; String from 1144 with 8 bytes in memory
    (data (i32.const 1144) "99999999\00")
    
    ;; String from 1424 with 3 bytes in memory
    (data (i32.const 1424) "123\00")
    
    ;; String from 1432 with 5 bytes in memory
    (data (i32.const 1432) "first\00")
    
    ;; String from 1440 with 6 bytes in memory
    (data (i32.const 1440) "second\00")
    
    ;; String from 1448 with 7 bytes in memory
    (data (i32.const 1448) "index=#\00")
    
    ;; String from 1456 with 28 bytes in memory
    (data (i32.const 1456) "directly create a structurte\00")
    
    ;; String from 1488 with 20 bytes in memory
    (data (i32.const 1488) "this is a structure!\00")
    
    ;; String from 1512 with 11 bytes in memory
    (data (i32.const 1512) "test object\00")
    
    ;; String from 1528 with 8 bytes in memory
    (data (i32.const 1528) "AAAAAAAA\00")
    
    ;; String from 1544 with 8 bytes in memory
    (data (i32.const 1544) "BBBBBBBB\00")
    
    ;; String from 1560 with 10 bytes in memory
    (data (i32.const 1560) "CCCCCCCCCC\00")
    
    ;; String from 1576 with 10 bytes in memory
    (data (i32.const 1576) "DDDDDDDDDD\00")
    
    ;; String from 1592 with 2 bytes in memory
    (data (i32.const 1592) "EE\00")
    
    ;; String from 1600 with 7 bytes in memory
    (data (i32.const 1600) "FFFFFFF\00")
    
    ;; String from 1608 with 8 bytes in memory
    (data (i32.const 1608) "GGGGGGGG\00")
    
    ;; String from 1624 with 14 bytes in memory
    (data (i32.const 1624) "js_object demo\00")
    
    ;; String from 1640 with 55 bytes in memory
    (data (i32.const 1640) "Javascript object generate from VB.NET WebAssembly demo\00")
    
    ;; String from 1696 with 13 bytes in memory
    (data (i32.const 1696) "vanillavb.app\00")
    
    ;; String from 1712 with 9 bytes in memory
    (data (i32.const 1712) "js_object\00")
    
    ;; String from 1728 with 32 bytes in memory
    (data (i32.const 1728) "Copyright (c) vanillavb.app 2019\00")
    
    ;; String from 1768 with 10 bytes in memory
    (data (i32.const 1768) "dotvanilla\00")
    
    ;; String from 1784 with 36 bytes in memory
    (data (i32.const 1784) "62b3389d-5109-4740-9c6a-35bb022355b9\00")
    
    ;; String from 1824 with 7 bytes in memory
    (data (i32.const 1824) "1.0.0.0\00")
    
    ;; String from 1832 with 7 bytes in memory
    (data (i32.const 1832) "1.0.0.0\00")
    
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
(global $loopOnArray.rect (mut i32) (i32.const 0))
(global $objectGC.rect (mut i32) (i32.const 0))
(global $name.demoNameString (mut i32) (i32.const 1144))

    ;; Export methods of this module
    (export "global.GetMemorySize" (func $global.GetMemorySize))

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
        
    (local $newObject_T0000aSOcos i32)
    (local $newObject_80000b53SRW i32)
    (local $newObject_50000c8bePi i32)
    (local $memoryCopyTo_00000dCiDql i32)
    (local $memorySource_j0000eEM1Ae i32)
    (local $newObject_p0000fm1Pvg i32)
    (local $memoryCopyTo_q0000g3VqQe i32)
    (local $memorySource_g0000hkeesV i32)
    (local $newObject_I0000iJZPMZ i32)
    (local $newObject_80000j9U839 i32)
    (local $memoryCopyTo_L0000k3fSLo i32)
    (local $memorySource_Z0000lZW8p6 i32)
    (local $arrayOffset_N0000mtvK0J i32)
    (local $itemOffset_P0000n7ZT76 i32)
    (local $newObject_G0000o23a5v i32)
    (local $i i32)
    (local $newObject_L0000r8S248 i32)
    (local $newObject_V0000s1km0T i32)
    (local $memoryCopyTo_w0000tv2x9h i32)
    (local $memorySource_P0000u5vDeT i32)
    (local $memoryCopyTo_e0000vYi6Nv i32)
    (local $memorySource_20000w9EUt7 i32)
    
    (set_global $objectGC.cx (f64.mul (get_global $objectGC.cx) (f64.convert_s/i32 (i32.const 2))))
    
    ;; Initialize a object instance of [[13]circle]
    ;; Object memory block begin at location: (get_local $newObject_L0000r8S248)
    (set_local $newObject_L0000r8S248 (call $global.ObjectManager.Allocate (i32.const 28) (i32.const 13)))
    ;; set field [circle::x]
    (f32.store (i32.add (get_local $newObject_L0000r8S248) (i32.const 0)) (f32.demote/f64 (get_global $objectGC.cx)))
    ;; set field [circle::y]
    (f32.store (i32.add (get_local $newObject_L0000r8S248) (i32.const 4)) (f32.convert_s/i32 (i32.const 9999)))
    ;; Copy memory of structure value:
    (set_local $memorySource_P0000u5vDeT (call $objectGC.newStruct ))
    (set_local $memoryCopyTo_w0000tv2x9h (i32.add (get_local $newObject_L0000r8S248) (i32.const 12)))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $memoryCopyTo_w0000tv2x9h) (i32.const 0)) (i32.load (i32.add (get_local $memorySource_P0000u5vDeT) (i32.const 0))))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $memoryCopyTo_w0000tv2x9h) (i32.const 4)) (i32.load (i32.add (get_local $memorySource_P0000u5vDeT) (i32.const 4))))
    (set_local $newObject_V0000s1km0T (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 1160)))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $newObject_V0000s1km0T) (i32.const 4)) (i32.const 1456))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $newObject_V0000s1km0T) (i32.const 0)) (i32.const 0))
    ;; Copy memory of structure value:
    (set_local $memorySource_20000w9EUt7 (get_local $newObject_V0000s1km0T))
    (set_local $memoryCopyTo_e0000vYi6Nv (i32.add (get_local $newObject_L0000r8S248) (i32.const 20)))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $memoryCopyTo_e0000vYi6Nv) (i32.const 0)) (i32.load (i32.add (get_local $memorySource_20000w9EUt7) (i32.const 0))))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $memoryCopyTo_e0000vYi6Nv) (i32.const 4)) (i32.load (i32.add (get_local $memorySource_20000w9EUt7) (i32.const 4))))
    ;; set field [circle::r]
    (i32.store (i32.add (get_local $newObject_L0000r8S248) (i32.const 8)) (i32.const 100))
    ;; Initialize an object memory block with 28 bytes data
    
    (return (get_local $newObject_L0000r8S248))
    )
    
    
    (func $objectGC.newStruct  (result i32)
        ;; Public Function newStruct() As intptr
        
    (local $newObject_x0000x25d6b i32)
    
    
    ;; Initialize a object instance of [[1160]name]
    ;; Object memory block begin at location: (get_local $newObject_x0000x25d6b)
    (set_local $newObject_x0000x25d6b (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 1160)))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $newObject_x0000x25d6b) (i32.const 4)) (i32.const 1488))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $newObject_x0000x25d6b) (i32.const 0)) (i32.const 111111))
    ;; Initialize an object memory block with 8 bytes data
    
    (return (get_local $newObject_x0000x25d6b))
    )
    
    
    (func $objectGC.getObject  (result i32)
        ;; Public Function getObject() As intptr
        
    
    
    (return (get_global $objectGC.rect))
    )
    
    
    
    
    ;; functions in [numberArray]
    
    (func $numberArray.createVector  (result i32)
        ;; Public Function createVector() As array(Of i32)
        
    (local $newObject_n0000yvVj21 i32)
    (local $arrayOffset_A0000z1yrCl i32)
    (local $itemOffset_800010g3OAm i32)
    
    
    ;; Save (i32.const 10) array element data to memory:
    ;; Array memory block begin at location: (get_local $arrayOffset_A0000z1yrCl)
    (set_local $arrayOffset_A0000z1yrCl (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 10) (i32.const 4))) (i32.const 7)))
    ;; class_id/typealias_enum i32 data: (i32.const 1)/array(Of i32)
    (i32.store (get_local $arrayOffset_A0000z1yrCl) (i32.const 1))
    (i32.store (i32.add (get_local $arrayOffset_A0000z1yrCl) (i32.const 4)) (i32.const 10))
    ;; End of byte marks meta data, start write data blocks
    (set_local $itemOffset_800010g3OAm (i32.add (get_local $arrayOffset_A0000z1yrCl) (i32.const 8)))
    (i32.store (i32.add (get_local $itemOffset_800010g3OAm) (i32.const 0)) (i32.const 1))
    (i32.store (i32.add (get_local $itemOffset_800010g3OAm) (i32.const 4)) (i32.const 2))
    (i32.store (i32.add (get_local $itemOffset_800010g3OAm) (i32.const 8)) (i32.const 3))
    (i32.store (i32.add (get_local $itemOffset_800010g3OAm) (i32.const 12)) (i32.const 4))
    (i32.store (i32.add (get_local $itemOffset_800010g3OAm) (i32.const 16)) (i32.const 5))
    (i32.store (i32.add (get_local $itemOffset_800010g3OAm) (i32.const 20)) (i32.const 6))
    (i32.store (i32.add (get_local $itemOffset_800010g3OAm) (i32.const 24)) (i32.const 7))
    (i32.store (i32.add (get_local $itemOffset_800010g3OAm) (i32.const 28)) (i32.const 8))
    (i32.store (i32.add (get_local $itemOffset_800010g3OAm) (i32.const 32)) (i32.const 9))
    (i32.store (i32.add (get_local $itemOffset_800010g3OAm) (i32.const 36)) (i32.const 0))
    ;; Assign array memory data to another expression
    (return (get_local $arrayOffset_A0000z1yrCl))
    )
    
    
    (func $numberArray.namesVector  (result i32)
        ;; Public Function namesVector() As array(Of string)
        
    (local $arrayOffset_X00011N9DZb i32)
    (local $itemOffset_f00012E73Cb i32)
    
    
    ;; Save (i32.const 7) array element data to memory:
    ;; Array memory block begin at location: (get_local $arrayOffset_X00011N9DZb)
    (set_local $arrayOffset_X00011N9DZb (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 7) (i32.const 4))) (i32.const 7)))
    ;; class_id/typealias_enum i32 data: (i32.const 5)/array(Of string)
    (i32.store (get_local $arrayOffset_X00011N9DZb) (i32.const 5))
    (i32.store (i32.add (get_local $arrayOffset_X00011N9DZb) (i32.const 4)) (i32.const 7))
    ;; End of byte marks meta data, start write data blocks
    (set_local $itemOffset_f00012E73Cb (i32.add (get_local $arrayOffset_X00011N9DZb) (i32.const 8)))
    (i32.store (i32.add (get_local $itemOffset_f00012E73Cb) (i32.const 0)) (i32.const 1528))
    (i32.store (i32.add (get_local $itemOffset_f00012E73Cb) (i32.const 4)) (i32.const 1544))
    (i32.store (i32.add (get_local $itemOffset_f00012E73Cb) (i32.const 8)) (i32.const 1560))
    (i32.store (i32.add (get_local $itemOffset_f00012E73Cb) (i32.const 12)) (i32.const 1576))
    (i32.store (i32.add (get_local $itemOffset_f00012E73Cb) (i32.const 16)) (i32.const 1592))
    (i32.store (i32.add (get_local $itemOffset_f00012E73Cb) (i32.const 20)) (i32.const 1600))
    (i32.store (i32.add (get_local $itemOffset_f00012E73Cb) (i32.const 24)) (i32.const 1608))
    ;; Assign array memory data to another expression
    (return (get_local $arrayOffset_X00011N9DZb))
    )
    
    
    (func $numberArray.structures  (result i32)
        ;; Public Function structures() As array(Of intptr)
        
    (local $names i32)
    (local $newObject_2000137t7sn i32)
    (local $newObject_900014zlG5X i32)
    (local $newObject_N000151Dnw9 i32)
    (local $arrayOffset_Z00016WTrE3 i32)
    (local $itemOffset_e00017M3Ti0 i32)
    (local $structCopyOf_d00018L01L4 i32)
    (local $structCopyOf_Z00019oF4yS i32)
    (local $structCopyOf_00001a94JPp i32)
    
    (set_local $names (call $numberArray.namesVector ))
    
    ;; Save (i32.const 3) array element data to memory:
    ;; Array memory block begin at location: (get_local $arrayOffset_Z00016WTrE3)
    (set_local $arrayOffset_Z00016WTrE3 (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 3) (i32.const 8))) (i32.const 7)))
    ;; class_id/typealias_enum i32 data: (i32.const 1160)/array(Of intptr)
    (i32.store (get_local $arrayOffset_Z00016WTrE3) (i32.const 1160))
    (i32.store (i32.add (get_local $arrayOffset_Z00016WTrE3) (i32.const 4)) (i32.const 3))
    ;; End of byte marks meta data, start write data blocks
    (set_local $itemOffset_e00017M3Ti0 (i32.add (get_local $arrayOffset_Z00016WTrE3) (i32.const 8)))
    (set_local $structCopyOf_d00018L01L4 (i32.add (get_local $itemOffset_e00017M3Ti0) (i32.const 0)))
    (call $GC.addObject (get_local $structCopyOf_d00018L01L4) (i32.const 1160))
    (set_local $newObject_2000137t7sn (get_local $structCopyOf_d00018L01L4))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $newObject_2000137t7sn) (i32.const 4)) (i32.load (i32.add (i32.add (get_local $names) (i32.const 8)) (i32.mul (i32.const 0) (i32.const 4)))))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $newObject_2000137t7sn) (i32.const 0)) (i32.const 0))
    (set_local $structCopyOf_Z00019oF4yS (i32.add (get_local $itemOffset_e00017M3Ti0) (i32.const 8)))
    (call $GC.addObject (get_local $structCopyOf_Z00019oF4yS) (i32.const 1160))
    (set_local $newObject_900014zlG5X (get_local $structCopyOf_Z00019oF4yS))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $newObject_900014zlG5X) (i32.const 4)) (i32.load (i32.add (i32.add (get_local $names) (i32.const 8)) (i32.mul (i32.const 1) (i32.const 4)))))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $newObject_900014zlG5X) (i32.const 0)) (i32.const 1))
    (set_local $structCopyOf_00001a94JPp (i32.add (get_local $itemOffset_e00017M3Ti0) (i32.const 16)))
    (call $GC.addObject (get_local $structCopyOf_00001a94JPp) (i32.const 1160))
    (set_local $newObject_N000151Dnw9 (get_local $structCopyOf_00001a94JPp))
    ;; set field [name::name]
    (i32.store (i32.add (get_local $newObject_N000151Dnw9) (i32.const 4)) (i32.load (i32.add (i32.add (get_local $names) (i32.const 8)) (i32.mul (i32.const 2) (i32.const 4)))))
    ;; set field [name::source]
    (i32.store (i32.add (get_local $newObject_N000151Dnw9) (i32.const 0)) (i32.const 2))
    ;; Assign array memory data to another expression
    (return (get_local $arrayOffset_Z00016WTrE3))
    )
    
    
    
    
    ;; functions in [AssemblyInfo]
    
    (func $AssemblyInfo.AssemblyTitle  (result i32)
        ;; Public Function AssemblyTitle() As string
        
    
    
    (return (i32.const 1624))
    )
    
    
    (func $AssemblyInfo.AssemblyDescription  (result i32)
        ;; Public Function AssemblyDescription() As string
        
    
    
    (return (i32.const 1640))
    )
    
    
    (func $AssemblyInfo.AssemblyCompany  (result i32)
        ;; Public Function AssemblyCompany() As string
        
    
    
    (return (i32.const 1696))
    )
    
    
    (func $AssemblyInfo.AssemblyProduct  (result i32)
        ;; Public Function AssemblyProduct() As string
        
    
    
    (return (i32.const 1712))
    )
    
    
    (func $AssemblyInfo.AssemblyCopyright  (result i32)
        ;; Public Function AssemblyCopyright() As string
        
    
    
    (return (i32.const 1728))
    )
    
    
    (func $AssemblyInfo.AssemblyTrademark  (result i32)
        ;; Public Function AssemblyTrademark() As string
        
    
    
    (return (i32.const 1768))
    )
    
    
    (func $AssemblyInfo.Guid  (result i32)
        ;; Public Function Guid() As string
        
    
    
    (return (i32.const 1784))
    )
    
    
    (func $AssemblyInfo.AssemblyVersion  (result i32)
        ;; Public Function AssemblyVersion() As string
        
    
    
    (return (i32.const 1824))
    )
    
    
    (func $AssemblyInfo.AssemblyFileVersion  (result i32)
        ;; Public Function AssemblyFileVersion() As string
        
    
    
    (return (i32.const 1832))
    )
    
    
    


    ;; Application Initialize
    ;; 
    ;; Sub New
    (func $Application_SubNew
        ;; call of the global variable initialize
        (call $global.initializer )

        (call $loopOnArray.constructor )

(call $objectGC.constructor )

(call $numberArray.constructor )
    )

    ;; Initializer for global variables if it is not a primitive abstract type
    (func $global.initializer  
    ;; Public Function initializer() As void
    
(local $arrayOffset_N0000mtvK0J i32)
(local $itemOffset_P0000n7ZT76 i32)
(local $newObject_T0000aSOcos i32)
(local $newObject_80000b53SRW i32)
(local $newObject_50000c8bePi i32)
(local $memorySource_j0000eEM1Ae i32)
(local $memoryCopyTo_00000dCiDql i32)
(local $newObject_p0000fm1Pvg i32)
(local $memorySource_g0000hkeesV i32)
(local $memoryCopyTo_q0000g3VqQe i32)
(local $newObject_I0000iJZPMZ i32)
(local $newObject_80000j9U839 i32)
(local $memorySource_Z0000lZW8p6 i32)
(local $memoryCopyTo_L0000k3fSLo i32)
(local $newObject_G0000o23a5v i32)


;; Save (i32.const 2) array element data to memory:
;; Array memory block begin at location: (get_local $arrayOffset_N0000mtvK0J)
(set_local $arrayOffset_N0000mtvK0J (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 2) (i32.const 4))) (i32.const 7)))
;; class_id/typealias_enum i32 data: (i32.const 520)/array(Of intptr)
(i32.store (get_local $arrayOffset_N0000mtvK0J) (i32.const 520))
(i32.store (i32.add (get_local $arrayOffset_N0000mtvK0J) (i32.const 4)) (i32.const 2))
;; End of byte marks meta data, start write data blocks
(set_local $itemOffset_P0000n7ZT76 (i32.add (get_local $arrayOffset_N0000mtvK0J) (i32.const 8)))
(set_local $newObject_T0000aSOcos (call $global.ObjectManager.Allocate (i32.const 40) (i32.const 520)))
;; set field [rectangle::h]
(i32.store (i32.add (get_local $newObject_T0000aSOcos) (i32.const 8)) (i32.const 100))
(set_local $newObject_80000b53SRW (call $global.ObjectManager.Allocate (i32.const 28) (i32.const 13)))
(set_local $newObject_50000c8bePi (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 1160)))
;; set field [name::name]
(i32.store (i32.add (get_local $newObject_50000c8bePi) (i32.const 4)) (i32.const 1424))
;; set field [name::source]
(i32.store (i32.add (get_local $newObject_50000c8bePi) (i32.const 0)) (i32.const 0))
;; Copy memory of structure value:
(set_local $memorySource_j0000eEM1Ae (get_local $newObject_50000c8bePi))
(set_local $memoryCopyTo_00000dCiDql (i32.add (get_local $newObject_80000b53SRW) (i32.const 12)))
;; set field [name::source]
(i32.store (i32.add (get_local $memoryCopyTo_00000dCiDql) (i32.const 0)) (i32.load (i32.add (get_local $memorySource_j0000eEM1Ae) (i32.const 0))))
;; set field [name::name]
(i32.store (i32.add (get_local $memoryCopyTo_00000dCiDql) (i32.const 4)) (i32.load (i32.add (get_local $memorySource_j0000eEM1Ae) (i32.const 4))))
;; set field [circle::x]
(f32.store (i32.add (get_local $newObject_80000b53SRW) (i32.const 0)) (f32.const 0))
;; set field [circle::y]
(f32.store (i32.add (get_local $newObject_80000b53SRW) (i32.const 4)) (f32.const 0))
;; set field [circle::r]
(i32.store (i32.add (get_local $newObject_80000b53SRW) (i32.const 8)) (i32.const 100))
;; Structure value is nothing!
;; set field [rectangle::inner]
(i32.store (i32.add (get_local $newObject_T0000aSOcos) (i32.const 36)) (get_local $newObject_80000b53SRW))
;; set field [rectangle::radius]
(f32.store (i32.add (get_local $newObject_T0000aSOcos) (i32.const 32)) (f32.convert_s/i32 (i32.const 999)))
(set_local $newObject_p0000fm1Pvg (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 1160)))
;; set field [name::name]
(i32.store (i32.add (get_local $newObject_p0000fm1Pvg) (i32.const 4)) (i32.const 1432))
;; set field [name::source]
(i32.store (i32.add (get_local $newObject_p0000fm1Pvg) (i32.const 0)) (i32.const 0))
;; Copy memory of structure value:
(set_local $memorySource_g0000hkeesV (get_local $newObject_p0000fm1Pvg))
(set_local $memoryCopyTo_q0000g3VqQe (i32.add (get_local $newObject_T0000aSOcos) (i32.const 0)))
;; set field [name::source]
(i32.store (i32.add (get_local $memoryCopyTo_q0000g3VqQe) (i32.const 0)) (i32.load (i32.add (get_local $memorySource_g0000hkeesV) (i32.const 0))))
;; set field [name::name]
(i32.store (i32.add (get_local $memoryCopyTo_q0000g3VqQe) (i32.const 4)) (i32.load (i32.add (get_local $memorySource_g0000hkeesV) (i32.const 4))))
;; set field [rectangle::w]
(i32.store (i32.add (get_local $newObject_T0000aSOcos) (i32.const 12)) (i32.const 1000))
;; set field [rectangle::y]
(f64.store (i32.add (get_local $newObject_T0000aSOcos) (i32.const 16)) (f64.const 0))
;; set field [rectangle::x]
(f64.store (i32.add (get_local $newObject_T0000aSOcos) (i32.const 24)) (f64.const 0))
(i32.store (i32.add (get_local $itemOffset_P0000n7ZT76) (i32.const 0)) (get_local $newObject_T0000aSOcos))
(set_local $newObject_I0000iJZPMZ (call $global.ObjectManager.Allocate (i32.const 40) (i32.const 520)))
(set_local $newObject_80000j9U839 (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 1160)))
;; set field [name::name]
(i32.store (i32.add (get_local $newObject_80000j9U839) (i32.const 4)) (i32.const 1440))
;; set field [name::source]
(i32.store (i32.add (get_local $newObject_80000j9U839) (i32.const 0)) (i32.const 0))
;; Copy memory of structure value:
(set_local $memorySource_Z0000lZW8p6 (get_local $newObject_80000j9U839))
(set_local $memoryCopyTo_L0000k3fSLo (i32.add (get_local $newObject_I0000iJZPMZ) (i32.const 0)))
;; set field [name::source]
(i32.store (i32.add (get_local $memoryCopyTo_L0000k3fSLo) (i32.const 0)) (i32.load (i32.add (get_local $memorySource_Z0000lZW8p6) (i32.const 0))))
;; set field [name::name]
(i32.store (i32.add (get_local $memoryCopyTo_L0000k3fSLo) (i32.const 4)) (i32.load (i32.add (get_local $memorySource_Z0000lZW8p6) (i32.const 4))))
;; set field [rectangle::h]
(i32.store (i32.add (get_local $newObject_I0000iJZPMZ) (i32.const 8)) (i32.const 1000))
;; set field [rectangle::w]
(i32.store (i32.add (get_local $newObject_I0000iJZPMZ) (i32.const 12)) (i32.const 1000))
;; set field [rectangle::y]
(f64.store (i32.add (get_local $newObject_I0000iJZPMZ) (i32.const 16)) (f64.const 0))
;; set field [rectangle::x]
(f64.store (i32.add (get_local $newObject_I0000iJZPMZ) (i32.const 24)) (f64.const 0))
;; set field [rectangle::radius]
(f32.store (i32.add (get_local $newObject_I0000iJZPMZ) (i32.const 32)) (f32.const -99))
;; set field [rectangle::inner]
(i32.store (i32.add (get_local $newObject_I0000iJZPMZ) (i32.const 36)) (i32.const 0))
(i32.store (i32.add (get_local $itemOffset_P0000n7ZT76) (i32.const 4)) (get_local $newObject_I0000iJZPMZ))
;; Assign array memory data to another expression
(set_global $loopOnArray.rect (get_local $arrayOffset_N0000mtvK0J))

;; Initialize a object instance of [[520]rectangle]
;; Object memory block begin at location: (get_local $newObject_G0000o23a5v)
(set_local $newObject_G0000o23a5v (call $global.ObjectManager.Allocate (i32.const 40) (i32.const 520)))
;; set field [rectangle::x]
(f64.store (i32.add (get_local $newObject_G0000o23a5v) (i32.const 24)) (f64.convert_s/i32 (i32.const 2147483647)))
;; set field [rectangle::y]
(f64.store (i32.add (get_local $newObject_G0000o23a5v) (i32.const 16)) (f64.convert_s/i32 (i32.const 10)))
;; Structure value is nothing!
;; set field [rectangle::h]
(i32.store (i32.add (get_local $newObject_G0000o23a5v) (i32.const 8)) (i32.const 1000))
;; set field [rectangle::w]
(i32.store (i32.add (get_local $newObject_G0000o23a5v) (i32.const 12)) (i32.const 1000))
;; set field [rectangle::radius]
(f32.store (i32.add (get_local $newObject_G0000o23a5v) (i32.const 32)) (f32.const -99))
;; set field [rectangle::inner]
(i32.store (i32.add (get_local $newObject_G0000o23a5v) (i32.const 36)) (i32.const 0))
;; Initialize an object memory block with 40 bytes data

(set_global $objectGC.rect (get_local $newObject_G0000o23a5v))
)

    (func $loopOnArray.constructor  
    ;; Public Function constructor() As void
    
(local $newObject_T0000aSOcos i32)
(local $newObject_80000b53SRW i32)
(local $newObject_50000c8bePi i32)
(local $memoryCopyTo_00000dCiDql i32)
(local $memorySource_j0000eEM1Ae i32)
(local $newObject_p0000fm1Pvg i32)
(local $memoryCopyTo_q0000g3VqQe i32)
(local $memorySource_g0000hkeesV i32)
(local $newObject_I0000iJZPMZ i32)
(local $newObject_80000j9U839 i32)
(local $memoryCopyTo_L0000k3fSLo i32)
(local $memorySource_Z0000lZW8p6 i32)
(local $arrayOffset_N0000mtvK0J i32)
(local $itemOffset_P0000n7ZT76 i32)
(local $newObject_G0000o23a5v i32)
(local $i i32)

(set_local $i (i32.const 0))
;; For i As Integer = 0 To rect.Length - 1

(block $block_Y0000p1Dt40 
    (loop $loop_k0000q5leuG

                (br_if $block_Y0000p1Dt40 (i32.gt_s (get_local $i) (i32.sub (i32.load (i32.add (get_global $loopOnArray.rect) (i32.const 4))) (i32.const 1))))
        (call $loopOnArray.println (call $string.add (i32.const 1448) (call $i32.toString (get_local $i))))
        (call $loopOnArray.print (i32.load (i32.add (i32.add (get_global $loopOnArray.rect) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))))
        ;; For loop control step: (i32.const 1)
        (set_local $i (i32.add (get_local $i) (i32.const 1)))
        (br $loop_k0000q5leuG)
        ;; For Loop Next On loop_k0000q5leuG

    )
)
)

(func $objectGC.constructor  
    ;; Public Function constructor() As void
    
(local $newObject_n0000yvVj21 i32)

(set_local $newObject_n0000yvVj21 (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 1160)))
;; set field [name::name]
(i32.store (i32.add (get_local $newObject_n0000yvVj21) (i32.const 4)) (i32.const 1512))
;; set field [name::source]
(i32.store (i32.add (get_local $newObject_n0000yvVj21) (i32.const 0)) (i32.const 888888888))
(i32.store (i32.add (get_global $objectGC.rect) (i32.const 0)) (get_local $newObject_n0000yvVj21))
(i32.store (i32.add (get_global $objectGC.rect) (i32.const 36)) (call $objectGC.newCircle ))
)

(func $numberArray.constructor  
    ;; Public Function constructor() As void
    



)

    (start $Application_SubNew)
)