(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 6/7/2019 2:55:18 PM
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
    (global $global.ObjectManager (mut i32) (i32.const 1104))

    ;; memory allocate in javascript runtime
    (func $global.ObjectManager.Allocate (param $sizeof i32) (param $class_id i32) (result i32)
    ;; Public Function ObjectManager.Allocate(sizeof As i32, class_id As i32) As i32
    
(local $offset i32)
(local $padding i32)

(set_local $offset (get_global $global.ObjectManager))
(set_global $global.ObjectManager (i32.add (get_local $offset) (get_local $sizeof)))
(set_local $padding (i32.rem_s (get_global $global.ObjectManager) (i32.const 8)))

(if (get_local $padding) 
    (then
                (set_local $padding (i32.sub (i32.const 8) (get_local $padding)))
        (set_global $global.ObjectManager (i32.add (get_global $global.ObjectManager) (get_local $padding)))
    ) (else
                (set_global $global.ObjectManager (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    )
)
(call $GC.addObject (get_local $offset) (get_local $class_id))
(return (get_local $offset))
)
    (func $global.GetMemorySize  (result i32)
    ;; Public Function GetMemorySize() As i32
    


(return (get_global $global.ObjectManager))
)

    ;; Memory data for string constant
        
    ;; String from 13 with 14 bytes in memory
    (data (i32.const 13) "this is a line\00")
    
    ;; String from 1080 with 2 bytes in memory
    (data (i32.const 1080) "GG\00")
    
    ;; String from 1088 with 2 bytes in memory
    (data (i32.const 1088) "#2\00")
    
    ;; String from 1096 with 3 bytes in memory
    (data (i32.const 1096) "ABC\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 32 with 348 bytes in memory
    ;;
    ;; class nestedTypes.[32] line
    ;;
    (data (i32.const 32) "eyJjb21tZW50IjpudWxsLCJjbGFzcyI6ImxpbmUiLCJjbGFzc19pZCI6MzIsImZpZWxkcyI6eyJhIjp7ImdlbmVyaWMiOltdLCJyYXciOiJbNzUyXXBvaW50IiwidHlwZSI6MTB9LCJiIjp7ImdlbmVyaWMiOltdLCJyYXciOiJbNzUyXXBvaW50IiwidHlwZSI6MTB9LCJuYW1lIjp7ImdlbmVyaWMiOltdLCJyYXciOiJbNDcyXW5hbWVPZiIsInR5cGUiOjEwfX0sImlzU3RydWN0IjpmYWxzZSwibWV0aG9kcyI6e30sIm5hbWVzcGFjZSI6Im5lc3RlZFR5cGVzIn0=\00")
    
    ;; String from 472 with 276 bytes in memory
    ;;
    ;; class nestedTypes.[472] nameOf
    ;;
    (data (i32.const 472) "eyJjb21tZW50IjpudWxsLCJjbGFzcyI6Im5hbWVPZiIsImNsYXNzX2lkIjo0NzIsImZpZWxkcyI6eyJzb3VyY2UiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImkzMiIsInR5cGUiOjF9LCJuYW1lIjp7ImdlbmVyaWMiOltdLCJyYXciOiJzdHJpbmciLCJ0eXBlIjo1fX0sImlzU3RydWN0IjpmYWxzZSwibWV0aG9kcyI6e30sIm5hbWVzcGFjZSI6Im5lc3RlZFR5cGVzIn0=\00")
    
    ;; String from 752 with 320 bytes in memory
    ;;
    ;; structure nestedTypes.[752] point
    ;;
    (data (i32.const 752) "eyJjb21tZW50IjpudWxsLCJjbGFzcyI6InBvaW50IiwiY2xhc3NfaWQiOjc1MiwiZmllbGRzIjp7InRhZyI6eyJnZW5lcmljIjpbXSwicmF3Ijoic3RyaW5nIiwidHlwZSI6NX0sInkiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImYzMiIsInR5cGUiOjN9LCJ4Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJmMzIiLCJ0eXBlIjozfX0sImlzU3RydWN0Ijp0cnVlLCJtZXRob2RzIjp7fSwibmFtZXNwYWNlIjoibmVzdGVkVHlwZXMifQ==\00")

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
    (global $nestedTest.line (mut i32) (i32.const 0))

    ;; Export methods of this module
    (export "global.GetMemorySize" (func $global.GetMemorySize))

    ;; export from VB.NET module: [nestedTest]
    
    (export "nestedTest.getArray" (func $nestedTest.getArray))
    (export "nestedTest.copytest" (func $nestedTest.copytest))
    
     

    ;; functions in [nestedTest]
    
    (func $nestedTest.getArray  (result i32)
        ;; Public Function getArray() As array(Of intptr)
        
    (local $newObject_n0000bQPcbU i32)
    (local $newObject_c0000cgWu0Y i32)
    (local $memoryCopyTo_60000d99ZJJ i32)
    (local $memorySource_O0000eeAobF i32)
    (local $memoryCopyTo_00000f5jKXh i32)
    (local $memorySource_p0000gPt1dc i32)
    (local $newObject_j0000h2ce8G i32)
    (local $newObject_B0000iHHx2Z i32)
    (local $newObject_80000j6LAx9 i32)
    (local $memoryCopyTo_40000kTg7OK i32)
    (local $memorySource_80000lcPCE7 i32)
    (local $memoryCopyTo_Z0000m57BR3 i32)
    (local $memorySource_t0000nyW67b i32)
    (local $arrayOffset_S0000oaC9bJ i32)
    (local $itemOffset_S0000pDPkDX i32)
    
    
    ;; Save (i32.const 2) array element data to memory:
    ;; Array memory block begin at location: (get_local $arrayOffset_S0000oaC9bJ)
    (set_local $arrayOffset_S0000oaC9bJ (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 2) (i32.const 4))) (i32.const 7)))
    ;; class_id/typealias_enum i32 data: (i32.const 32)/array(Of intptr)
    (i32.store (get_local $arrayOffset_S0000oaC9bJ) (i32.const 32))
    (i32.store (i32.add (get_local $arrayOffset_S0000oaC9bJ) (i32.const 4)) (i32.const 2))
    ;; End of byte marks meta data, start write data blocks
    (set_local $itemOffset_S0000pDPkDX (i32.add (get_local $arrayOffset_S0000oaC9bJ) (i32.const 8)))
    (set_local $newObject_n0000bQPcbU (call $global.ObjectManager.Allocate (i32.const 28) (i32.const 32)))
    ;; Copy memory of structure value:
    (set_local $memorySource_O0000eeAobF (call $nestedTest.newPoint ))
    (set_local $memoryCopyTo_60000d99ZJJ (i32.add (get_local $newObject_n0000bQPcbU) (i32.const 0)))
    ;; set field [nestedTypes.point::tag]
    (i32.store (i32.add (get_local $memoryCopyTo_60000d99ZJJ) (i32.const 0)) (i32.load (i32.add (get_local $memorySource_O0000eeAobF) (i32.const 0))))
    ;; set field [nestedTypes.point::y]
    (f32.store (i32.add (get_local $memoryCopyTo_60000d99ZJJ) (i32.const 4)) (f32.load (i32.add (get_local $memorySource_O0000eeAobF) (i32.const 4))))
    ;; set field [nestedTypes.point::x]
    (f32.store (i32.add (get_local $memoryCopyTo_60000d99ZJJ) (i32.const 8)) (f32.load (i32.add (get_local $memorySource_O0000eeAobF) (i32.const 8))))
    (set_local $newObject_c0000cgWu0Y (call $global.ObjectManager.Allocate (i32.const 12) (i32.const 752)))
    ;; set field [nestedTypes.point::tag]
    (i32.store (i32.add (get_local $newObject_c0000cgWu0Y) (i32.const 0)) (i32.const 1080))
    ;; set field [nestedTypes.point::y]
    (f32.store (i32.add (get_local $newObject_c0000cgWu0Y) (i32.const 4)) (f32.const 0))
    ;; set field [nestedTypes.point::x]
    (f32.store (i32.add (get_local $newObject_c0000cgWu0Y) (i32.const 8)) (f32.const 0))
    ;; Copy memory of structure value:
    (set_local $memorySource_p0000gPt1dc (get_local $newObject_c0000cgWu0Y))
    (set_local $memoryCopyTo_00000f5jKXh (i32.add (get_local $newObject_n0000bQPcbU) (i32.const 12)))
    ;; set field [nestedTypes.point::tag]
    (i32.store (i32.add (get_local $memoryCopyTo_00000f5jKXh) (i32.const 0)) (i32.load (i32.add (get_local $memorySource_p0000gPt1dc) (i32.const 0))))
    ;; set field [nestedTypes.point::y]
    (f32.store (i32.add (get_local $memoryCopyTo_00000f5jKXh) (i32.const 4)) (f32.load (i32.add (get_local $memorySource_p0000gPt1dc) (i32.const 4))))
    ;; set field [nestedTypes.point::x]
    (f32.store (i32.add (get_local $memoryCopyTo_00000f5jKXh) (i32.const 8)) (f32.load (i32.add (get_local $memorySource_p0000gPt1dc) (i32.const 8))))
    ;; set field [nestedTypes.line::name]
    (i32.store (i32.add (get_local $newObject_n0000bQPcbU) (i32.const 24)) (i32.const 0))
    (i32.store (i32.add (get_local $itemOffset_S0000pDPkDX) (i32.const 0)) (get_local $newObject_n0000bQPcbU))
    (set_local $newObject_j0000h2ce8G (call $global.ObjectManager.Allocate (i32.const 28) (i32.const 32)))
    (set_local $newObject_B0000iHHx2Z (call $global.ObjectManager.Allocate (i32.const 12) (i32.const 752)))
    ;; set field [nestedTypes.point::tag]
    (i32.store (i32.add (get_local $newObject_B0000iHHx2Z) (i32.const 0)) (i32.const 0))
    ;; set field [nestedTypes.point::y]
    (f32.store (i32.add (get_local $newObject_B0000iHHx2Z) (i32.const 4)) (f32.const 0))
    ;; set field [nestedTypes.point::x]
    (f32.store (i32.add (get_local $newObject_B0000iHHx2Z) (i32.const 8)) (f32.const 0))
    ;; Copy memory of structure value:
    (set_local $memorySource_80000lcPCE7 (get_local $newObject_B0000iHHx2Z))
    (set_local $memoryCopyTo_40000kTg7OK (i32.add (get_local $newObject_j0000h2ce8G) (i32.const 12)))
    ;; set field [nestedTypes.point::tag]
    (i32.store (i32.add (get_local $memoryCopyTo_40000kTg7OK) (i32.const 0)) (i32.load (i32.add (get_local $memorySource_80000lcPCE7) (i32.const 0))))
    ;; set field [nestedTypes.point::y]
    (f32.store (i32.add (get_local $memoryCopyTo_40000kTg7OK) (i32.const 4)) (f32.load (i32.add (get_local $memorySource_80000lcPCE7) (i32.const 4))))
    ;; set field [nestedTypes.point::x]
    (f32.store (i32.add (get_local $memoryCopyTo_40000kTg7OK) (i32.const 8)) (f32.load (i32.add (get_local $memorySource_80000lcPCE7) (i32.const 8))))
    ;; Copy memory of structure value:
    (set_local $memorySource_t0000nyW67b (call $nestedTest.newPoint ))
    (set_local $memoryCopyTo_Z0000m57BR3 (i32.add (get_local $newObject_j0000h2ce8G) (i32.const 0)))
    ;; set field [nestedTypes.point::tag]
    (i32.store (i32.add (get_local $memoryCopyTo_Z0000m57BR3) (i32.const 0)) (i32.load (i32.add (get_local $memorySource_t0000nyW67b) (i32.const 0))))
    ;; set field [nestedTypes.point::y]
    (f32.store (i32.add (get_local $memoryCopyTo_Z0000m57BR3) (i32.const 4)) (f32.load (i32.add (get_local $memorySource_t0000nyW67b) (i32.const 4))))
    ;; set field [nestedTypes.point::x]
    (f32.store (i32.add (get_local $memoryCopyTo_Z0000m57BR3) (i32.const 8)) (f32.load (i32.add (get_local $memorySource_t0000nyW67b) (i32.const 8))))
    (set_local $newObject_80000j6LAx9 (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 472)))
    ;; set field [nestedTypes.nameOf::name]
    (i32.store (i32.add (get_local $newObject_80000j6LAx9) (i32.const 4)) (i32.const 1088))
    ;; set field [nestedTypes.nameOf::source]
    (i32.store (i32.add (get_local $newObject_80000j6LAx9) (i32.const 0)) (i32.const 2))
    ;; set field [nestedTypes.line::name]
    (i32.store (i32.add (get_local $newObject_j0000h2ce8G) (i32.const 24)) (get_local $newObject_80000j6LAx9))
    (i32.store (i32.add (get_local $itemOffset_S0000pDPkDX) (i32.const 4)) (get_local $newObject_j0000h2ce8G))
    ;; Assign array memory data to another expression
    (return (get_local $arrayOffset_S0000oaC9bJ))
    )
    
    
    (func $nestedTest.newPoint  (result i32)
        ;; Public Function newPoint() As intptr
        
    (local $newObject_00000q83geh i32)
    
    
    ;; Initialize a object instance of [[752]point]
    ;; Object memory block begin at location: (get_local $newObject_00000q83geh)
    (set_local $newObject_00000q83geh (call $global.ObjectManager.Allocate (i32.const 12) (i32.const 752)))
    ;; set field [nestedTypes.point::x]
    (f32.store (i32.add (get_local $newObject_00000q83geh) (i32.const 8)) (f32.convert_s/i32 (i32.sub (i32.const 0) (i32.const 1))))
    ;; set field [nestedTypes.point::y]
    (f32.store (i32.add (get_local $newObject_00000q83geh) (i32.const 4)) (f32.convert_s/i32 (i32.sub (i32.const 0) (i32.const 1))))
    ;; set field [nestedTypes.point::tag]
    (i32.store (i32.add (get_local $newObject_00000q83geh) (i32.const 0)) (i32.const 0))
    ;; Initialize an object memory block with 12 bytes data
    
    (return (get_local $newObject_00000q83geh))
    )
    
    
    (func $nestedTest.copytest  
        ;; Public Function copytest() As void
        
    (local $newObject_r0000r7cKFF i32)
    (local $memoryCopyTo_90000sO0v4Y i32)
    (local $memorySource_I0000tAnU4F i32)
    
    
    ;; Initialize a object instance of [[32]line]
    ;; Object memory block begin at location: (get_local $newObject_r0000r7cKFF)
    (set_local $newObject_r0000r7cKFF (call $global.ObjectManager.Allocate (i32.const 28) (i32.const 32)))
    ;; Copy memory of structure value:
    (set_local $memorySource_I0000tAnU4F (call $nestedTest.newPoint ))
    (set_local $memoryCopyTo_90000sO0v4Y (i32.add (get_local $newObject_r0000r7cKFF) (i32.const 0)))
    ;; set field [nestedTypes.point::tag]
    (i32.store (i32.add (get_local $memoryCopyTo_90000sO0v4Y) (i32.const 0)) (i32.load (i32.add (get_local $memorySource_I0000tAnU4F) (i32.const 0))))
    ;; set field [nestedTypes.point::y]
    (f32.store (i32.add (get_local $memoryCopyTo_90000sO0v4Y) (i32.const 4)) (f32.load (i32.add (get_local $memorySource_I0000tAnU4F) (i32.const 4))))
    ;; set field [nestedTypes.point::x]
    (f32.store (i32.add (get_local $memoryCopyTo_90000sO0v4Y) (i32.const 8)) (f32.load (i32.add (get_local $memorySource_I0000tAnU4F) (i32.const 8))))
    ;; Structure value is nothing!
    ;; set field [nestedTypes.line::name]
    (i32.store (i32.add (get_local $newObject_r0000r7cKFF) (i32.const 24)) (i32.const 0))
    ;; Initialize an object memory block with 28 bytes data
    
    (set_global $nestedTest.line (get_local $newObject_r0000r7cKFF))
    )
    
    
    


    ;; Application Initialize
    ;; 
    ;; Sub New
    (func $Application_SubNew
        ;; call of the global variable initialize
        (call $global.initializer )

        (call $nestedTest.constructor )
    )

    ;; Initializer for global variables if it is not a primitive abstract type
    (func $global.initializer  
    ;; Public Function initializer() As void
    


(set_global $nameOf.source (i32.sub (i32.const 0) (i32.const 99999)))
)

    (func $nestedTest.constructor  
    ;; Public Function constructor() As void
    
(local $newObject_v0000uQ2U9B i32)
(local $newObject_a0000v3S8DD i32)
(local $memoryCopyTo_90000wKd753 i32)
(local $memorySource_60000xWeAWm i32)
(local $newObject_m0000yL0k8h i32)


;; Initialize a object instance of [[32]line]
;; Object memory block begin at location: (get_local $newObject_v0000uQ2U9B)
(set_local $newObject_v0000uQ2U9B (call $global.ObjectManager.Allocate (i32.const 28) (i32.const 32)))
(set_local $newObject_a0000v3S8DD (call $global.ObjectManager.Allocate (i32.const 12) (i32.const 752)))
;; set field [nestedTypes.point::x]
(f32.store (i32.add (get_local $newObject_a0000v3S8DD) (i32.const 8)) (f32.convert_s/i32 (i32.const 99)))
;; set field [nestedTypes.point::y]
(f32.store (i32.add (get_local $newObject_a0000v3S8DD) (i32.const 4)) (f32.convert_s/i32 (i32.const 88)))
;; set field [nestedTypes.point::tag]
(i32.store (i32.add (get_local $newObject_a0000v3S8DD) (i32.const 0)) (i32.const 0))
;; Copy memory of structure value:
(set_local $memorySource_60000xWeAWm (get_local $newObject_a0000v3S8DD))
(set_local $memoryCopyTo_90000wKd753 (i32.add (get_local $newObject_v0000uQ2U9B) (i32.const 0)))
;; set field [nestedTypes.point::tag]
(i32.store (i32.add (get_local $memoryCopyTo_90000wKd753) (i32.const 0)) (i32.load (i32.add (get_local $memorySource_60000xWeAWm) (i32.const 0))))
;; set field [nestedTypes.point::y]
(f32.store (i32.add (get_local $memoryCopyTo_90000wKd753) (i32.const 4)) (f32.load (i32.add (get_local $memorySource_60000xWeAWm) (i32.const 4))))
;; set field [nestedTypes.point::x]
(f32.store (i32.add (get_local $memoryCopyTo_90000wKd753) (i32.const 8)) (f32.load (i32.add (get_local $memorySource_60000xWeAWm) (i32.const 8))))
;; Structure value is nothing!
;; set field [nestedTypes.line::name]
(i32.store (i32.add (get_local $newObject_v0000uQ2U9B) (i32.const 24)) (i32.const 0))
;; Initialize an object memory block with 28 bytes data

(set_global $nestedTest.line (get_local $newObject_v0000uQ2U9B))
(set_local $newObject_m0000yL0k8h (call $global.ObjectManager.Allocate (i32.const 12) (i32.const 752)))
;; set field [nestedTypes.point::x]
(f32.store (i32.add (get_local $newObject_m0000yL0k8h) (i32.const 8)) (f32.convert_s/i32 (i32.const 100)))
;; set field [nestedTypes.point::y]
(f32.store (i32.add (get_local $newObject_m0000yL0k8h) (i32.const 4)) (f32.convert_s/i32 (i32.const 50000)))
;; set field [nestedTypes.point::tag]
(i32.store (i32.add (get_local $newObject_m0000yL0k8h) (i32.const 0)) (i32.const 1096))
(i32.store (i32.add (get_global $nestedTest.line) (i32.const 12)) (get_local $newObject_m0000yL0k8h))
)

    (start $Application_SubNew)
)