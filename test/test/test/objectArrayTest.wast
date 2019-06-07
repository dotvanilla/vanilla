(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 6/7/2019 11:54:47 AM
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
;; Declare Function dump Lib "console" Alias "log" (obj As intptr) As void
    (func $objectArrayTest.dump (import "console" "log") (param $obj i32) )
;; Declare Function println Lib "console" Alias "log" (info As string) As void
    (func $objectArrayTest.println (import "console" "log") (param $info i32) )
;; Declare Function warning Lib "console" Alias "warn" (info As string) As void
    (func $objectArrayTest.warning (import "console" "warn") (param $info i32) )
;; Declare Function viewLine Lib "console" Alias "log" (line As intptr) As void
    (func $objectArrayTest.viewLine (import "console" "log") (param $line i32) )
;; Declare Function i32.toString Lib "string" Alias "toString" (x As i32) As string
    (func $i32.toString (import "string" "toString") (param $x i32) (result i32))
;; Declare Function f64.toString Lib "string" Alias "toString" (x As f64) As string
    (func $f64.toString (import "string" "toString") (param $x f64) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 992))

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
        
    ;; String from 13 with 3 bytes in memory
    (data (i32.const 13) "ABC\00")
    
    ;; String from 696 with 11 bytes in memory
    (data (i32.const 696) "Hello world\00")
    
    ;; String from 712 with 7 bytes in memory
    (data (i32.const 712) "b:Point\00")
    
    ;; String from 720 with 7 bytes in memory
    (data (i32.const 720) "c:Point\00")
    
    ;; String from 728 with 1 bytes in memory
    (data (i32.const 728) "#\00")
    
    ;; String from 736 with 13 bytes in memory
    (data (i32.const 736) " addressOf:=&\00")
    
    ;; String from 752 with 8 bytes in memory
    (data (i32.const 752) ", name:=\00")
    
    ;; String from 768 with 11 bytes in memory
    (data (i32.const 768) "   [x,y]:=[\00")
    
    ;; String from 784 with 1 bytes in memory
    (data (i32.const 784) ",\00")
    
    ;; String from 792 with 1 bytes in memory
    (data (i32.const 792) "]\00")
    
    ;; String from 800 with 98 bytes in memory
    (data (i32.const 800) "As the point and line object is reference type, so the point value in lines array is also changed!\00")
    
    ;; String from 904 with 24 bytes in memory
    (data (i32.const 904) "Assign a random [x,y]:=[\00")
    
    ;; String from 936 with 1 bytes in memory
    (data (i32.const 936) ",\00")
    
    ;; String from 944 with 1 bytes in memory
    (data (i32.const 944) "]\00")
    
    ;; String from 952 with 1 bytes in memory
    (data (i32.const 952) "#\00")
    
    ;; String from 960 with 13 bytes in memory
    (data (i32.const 960) " addressOf:=&\00")
    
    ;; String from 976 with 8 bytes in memory
    (data (i32.const 976) ", name:=\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 24 with 320 bytes in memory
    ;;
    ;; class arrayObjects.[24] Point
    ;;
    (data (i32.const 24) "eyJjb21tZW50IjpudWxsLCJjbGFzcyI6IlBvaW50IiwiY2xhc3NfaWQiOjI0LCJmaWVsZHMiOnsieCI6eyJnZW5lcmljIjpbXSwicmF3IjoiZjY0IiwidHlwZSI6NH0sInkiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImY2NCIsInR5cGUiOjR9LCJuYW1lIjp7ImdlbmVyaWMiOltdLCJyYXciOiJzdHJpbmciLCJ0eXBlIjo1fX0sImlzU3RydWN0IjpmYWxzZSwibWV0aG9kcyI6e30sIm5hbWVzcGFjZSI6ImFycmF5T2JqZWN0cyJ9\00")
    
    ;; String from 352 with 280 bytes in memory
    ;;
    ;; class arrayObjects.[352] line
    ;;
    (data (i32.const 352) "eyJjb21tZW50IjpudWxsLCJjbGFzcyI6ImxpbmUiLCJjbGFzc19pZCI6MzUyLCJmaWVsZHMiOnsiYiI6eyJnZW5lcmljIjpbXSwicmF3IjoiWzI0XVBvaW50IiwidHlwZSI6MTB9LCJhIjp7ImdlbmVyaWMiOltdLCJyYXciOiJbMjRdUG9pbnQiLCJ0eXBlIjoxMH19LCJpc1N0cnVjdCI6ZmFsc2UsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOiJhcnJheU9iamVjdHMifQ==\00")

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
    (global $objectArrayTest.lines (mut i32) (i32.const 0))
(global $objectArrayTest.ps (mut i32) (i32.const 0))

    ;; Export methods of this module
    (export "global.GetMemorySize" (func $global.GetMemorySize))

    ;; export from VB.NET module: [objectArrayTest]
    
    (export "objectArrayTest.printArray" (func $objectArrayTest.printArray))
    (export "objectArrayTest.GetPoints" (func $objectArrayTest.GetPoints))
    
     

    ;; functions in [objectArrayTest]
    
    (func $objectArrayTest.printArray  
        ;; Public Function printArray() As void
        
    (local $newObject_Q0000bp33uP i32)
    (local $newObject_l0000cq9sD3 i32)
    (local $newObject_Q0000d4Yk0f i32)
    (local $newObject_50000eKQe28 i32)
    (local $arrayOffset_40000f1m6fP i32)
    (local $itemOffset_60000gpggBr i32)
    (local $i i32)
    (local $j i32)
    
    (call $objectArrayTest.modifyArray )
    (set_local $i (i32.const 0))
    ;; For i As Integer = 0 To ps.Length - 1
    
    (block $block_C0000h0Z5ii 
        (loop $loop_H0000iP9XI9
    
                    (br_if $block_C0000h0Z5ii (i32.gt_s (get_local $i) (i32.sub (i32.load (i32.add (get_global $objectArrayTest.ps) (i32.const 4))) (i32.const 1))))
            (call $objectArrayTest.println (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 728) (call $i32.toString (get_local $i))) (i32.const 736)) (call $i32.toString (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))))) (i32.const 752)) (call $i32.toString (i32.load (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 16))))))
            (call $objectArrayTest.println (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 768) (call $f64.toString (f64.load (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 0))))) (i32.const 784)) (call $f64.toString (f64.load (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 8))))) (i32.const 792)))
            (call $objectArrayTest.dump (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))))
            ;; For loop control step: (i32.const 1)
            (set_local $i (i32.add (get_local $i) (i32.const 1)))
            (br $loop_H0000iP9XI9)
            ;; For Loop Next On loop_H0000iP9XI9
    
        )
    )
    (call $objectArrayTest.println (i32.const 800))
    (set_local $j (i32.const 0))
    ;; For j As Integer = 0 To lines.Length - 1
    
    (block $block_70000jZXYK3 
        (loop $loop_a0000kM382A
    
                    (br_if $block_70000jZXYK3 (i32.gt_s (get_local $j) (i32.sub (i32.load (i32.add (get_global $objectArrayTest.lines) (i32.const 4))) (i32.const 1))))
            (call $objectArrayTest.viewLine (i32.load (i32.add (i32.add (get_global $objectArrayTest.lines) (i32.const 8)) (i32.mul (get_local $j) (i32.const 4)))))
            ;; For loop control step: (i32.const 1)
            (set_local $j (i32.add (get_local $j) (i32.const 1)))
            (br $loop_a0000kM382A)
            ;; For Loop Next On loop_a0000kM382A
    
        )
    )
    )
    
    
    (func $objectArrayTest.modifyArray  
        ;; Public Function modifyArray() As void
        
    (local $i i32)
    
    (set_local $i (i32.const 0))
    ;; For i As Integer = 0 To ps.Length - 1
    
    (block $block_y0000l8oRCB 
        (loop $loop_00000mOmm50
    
                    (br_if $block_y0000l8oRCB (i32.gt_s (get_local $i) (i32.sub (i32.load (i32.add (get_global $objectArrayTest.ps) (i32.const 4))) (i32.const 1))))
            (f64.store (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 0)) (call $Math.random ))
            (f64.store (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 8)) (f64.mul (call $Math.random ) (f64.convert_s/i32 (i32.const 100))))
            (call $objectArrayTest.warning (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 904) (call $f64.toString (f64.load (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 0))))) (i32.const 936)) (call $f64.toString (f64.load (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 8))))) (i32.const 944)))
            ;; For loop control step: (i32.const 1)
            (set_local $i (i32.add (get_local $i) (i32.const 1)))
            (br $loop_00000mOmm50)
            ;; For Loop Next On loop_00000mOmm50
    
        )
    )
    )
    
    
    (func $objectArrayTest.GetPoints  (result i32)
        ;; Public Function GetPoints() As array(Of intptr)
        
    
    
    (return (get_global $objectArrayTest.ps))
    )
    
    
    


    ;; Application Initialize
    ;; 
    ;; Sub New
    (func $Application_SubNew
        ;; call of the global variable initialize
        (call $global.initializer )

        (call $objectArrayTest.constructor )
    )

    ;; Initializer for global variables if it is not a primitive abstract type
    (func $global.initializer  
    ;; Public Function initializer() As void
    
(local $arrayOffset_40000f1m6fP i32)
(local $itemOffset_60000gpggBr i32)
(local $newObject_Q0000bp33uP i32)
(local $newObject_l0000cq9sD3 i32)
(local $newObject_Q0000d4Yk0f i32)
(local $newObject_50000eKQe28 i32)


;; Save (i32.const 4) array element data to memory:
;; Array memory block begin at location: (get_local $arrayOffset_40000f1m6fP)
(set_local $arrayOffset_40000f1m6fP (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 4) (i32.const 4))) (i32.const 7)))
;; class_id/typealias_enum i32 data: (i32.const 24)/array(Of intptr)
(i32.store (get_local $arrayOffset_40000f1m6fP) (i32.const 24))
(i32.store (i32.add (get_local $arrayOffset_40000f1m6fP) (i32.const 4)) (i32.const 4))
;; End of byte marks meta data, start write data blocks
(set_local $itemOffset_60000gpggBr (i32.add (get_local $arrayOffset_40000f1m6fP) (i32.const 8)))
(set_local $newObject_Q0000bp33uP (call $global.ObjectManager.Allocate (i32.const 20) (i32.const 24)))
;; set field [arrayObjects.Point::x]
(f64.store (i32.add (get_local $newObject_Q0000bp33uP) (i32.const 0)) (f64.convert_s/i32 (i32.const 100)))
;; set field [arrayObjects.Point::y]
(f64.store (i32.add (get_local $newObject_Q0000bp33uP) (i32.const 8)) (f64.convert_s/i32 (i32.const 500)))
;; set field [arrayObjects.Point::name]
(i32.store (i32.add (get_local $newObject_Q0000bp33uP) (i32.const 16)) (i32.const 13))
(i32.store (i32.add (get_local $itemOffset_60000gpggBr) (i32.const 0)) (get_local $newObject_Q0000bp33uP))
(set_local $newObject_l0000cq9sD3 (call $global.ObjectManager.Allocate (i32.const 20) (i32.const 24)))
;; set field [arrayObjects.Point::x]
(f64.store (i32.add (get_local $newObject_l0000cq9sD3) (i32.const 0)) (f64.convert_s/i32 (i32.const 1)))
;; set field [arrayObjects.Point::y]
(f64.store (i32.add (get_local $newObject_l0000cq9sD3) (i32.const 8)) (f64.mul (f64.load (i32.add (get_local $newObject_l0000cq9sD3) (i32.const 0))) (f64.convert_s/i32 (i32.const 999))))
;; set field [arrayObjects.Point::name]
(i32.store (i32.add (get_local $newObject_l0000cq9sD3) (i32.const 16)) (i32.const 696))
(i32.store (i32.add (get_local $itemOffset_60000gpggBr) (i32.const 4)) (get_local $newObject_l0000cq9sD3))
(set_local $newObject_Q0000d4Yk0f (call $global.ObjectManager.Allocate (i32.const 20) (i32.const 24)))
;; set field [arrayObjects.Point::name]
(i32.store (i32.add (get_local $newObject_Q0000d4Yk0f) (i32.const 16)) (i32.const 712))
;; set field [arrayObjects.Point::x]
(f64.store (i32.add (get_local $newObject_Q0000d4Yk0f) (i32.const 0)) (f64.const 0))
;; set field [arrayObjects.Point::y]
(f64.store (i32.add (get_local $newObject_Q0000d4Yk0f) (i32.const 8)) (f64.const 0))
(i32.store (i32.add (get_local $itemOffset_60000gpggBr) (i32.const 8)) (get_local $newObject_Q0000d4Yk0f))
(set_local $newObject_50000eKQe28 (call $global.ObjectManager.Allocate (i32.const 20) (i32.const 24)))
;; set field [arrayObjects.Point::name]
(i32.store (i32.add (get_local $newObject_50000eKQe28) (i32.const 16)) (i32.const 720))
;; set field [arrayObjects.Point::x]
(f64.store (i32.add (get_local $newObject_50000eKQe28) (i32.const 0)) (f64.const 0))
;; set field [arrayObjects.Point::y]
(f64.store (i32.add (get_local $newObject_50000eKQe28) (i32.const 8)) (f64.const 0))
(i32.store (i32.add (get_local $itemOffset_60000gpggBr) (i32.const 12)) (get_local $newObject_50000eKQe28))
;; Assign array memory data to another expression
(set_global $objectArrayTest.ps (get_local $arrayOffset_40000f1m6fP))
)

    (func $objectArrayTest.constructor  
    ;; Public Function constructor() As void
    
(local $item i32)
(local $i i32)
(local $newObject_p0000ptGUEB i32)
(local $newObject_R0000qnd3bG i32)
(local $arrayOffset_A0000r68HQO i32)
(local $itemOffset_Z0000sUW55t i32)

(set_local $item (i32.const 0))
(set_local $i (i32.const 0))
;; For i As Integer = 0 To ps.Length - 1

(block $block_20000nI3V8T 
    (loop $loop_C0000oJmi7c

                (br_if $block_20000nI3V8T (i32.gt_s (get_local $i) (i32.sub (i32.load (i32.add (get_global $objectArrayTest.ps) (i32.const 4))) (i32.const 1))))
        (set_local $item (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))))
        (call $objectArrayTest.println (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 952) (call $i32.toString (get_local $i))) (i32.const 960)) (call $i32.toString (get_local $item))) (i32.const 976)) (call $i32.toString (i32.load (i32.add (get_local $item) (i32.const 16))))))
        (call $objectArrayTest.dump (get_local $item))
        ;; For loop control step: (i32.const 1)
        (set_local $i (i32.add (get_local $i) (i32.const 1)))
        (br $loop_C0000oJmi7c)
        ;; For Loop Next On loop_C0000oJmi7c

    )
)

;; Save (i32.const 2) array element data to memory:
;; Array memory block begin at location: (get_local $arrayOffset_A0000r68HQO)
(set_local $arrayOffset_A0000r68HQO (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 2) (i32.const 4))) (i32.const 7)))
;; class_id/typealias_enum i32 data: (i32.const 352)/array(Of intptr)
(i32.store (get_local $arrayOffset_A0000r68HQO) (i32.const 352))
(i32.store (i32.add (get_local $arrayOffset_A0000r68HQO) (i32.const 4)) (i32.const 2))
;; End of byte marks meta data, start write data blocks
(set_local $itemOffset_Z0000sUW55t (i32.add (get_local $arrayOffset_A0000r68HQO) (i32.const 8)))
(set_local $newObject_p0000ptGUEB (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 352)))
;; set field [arrayObjects.line::a]
(i32.store (i32.add (get_local $newObject_p0000ptGUEB) (i32.const 4)) (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (i32.const 0) (i32.const 4)))))
;; set field [arrayObjects.line::b]
(i32.store (i32.add (get_local $newObject_p0000ptGUEB) (i32.const 0)) (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (i32.const 1) (i32.const 4)))))
(i32.store (i32.add (get_local $itemOffset_Z0000sUW55t) (i32.const 0)) (get_local $newObject_p0000ptGUEB))
(set_local $newObject_R0000qnd3bG (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 352)))
;; set field [arrayObjects.line::a]
(i32.store (i32.add (get_local $newObject_R0000qnd3bG) (i32.const 4)) (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (i32.const 2) (i32.const 4)))))
;; set field [arrayObjects.line::b]
(i32.store (i32.add (get_local $newObject_R0000qnd3bG) (i32.const 0)) (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (i32.const 3) (i32.const 4)))))
(i32.store (i32.add (get_local $itemOffset_Z0000sUW55t) (i32.const 4)) (get_local $newObject_R0000qnd3bG))
;; Assign array memory data to another expression
(set_global $objectArrayTest.lines (get_local $arrayOffset_A0000r68HQO))
)

    (start $Application_SubNew)
)