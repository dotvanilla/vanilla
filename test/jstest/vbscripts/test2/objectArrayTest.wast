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
    (global $global.ObjectManager (mut i32) (i32.const 888))

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
    
    ;; String from 800 with 24 bytes in memory
    (data (i32.const 800) "Assign a random [x,y]:=[\00")
    
    ;; String from 832 with 1 bytes in memory
    (data (i32.const 832) ",\00")
    
    ;; String from 840 with 1 bytes in memory
    (data (i32.const 840) "]\00")
    
    ;; String from 848 with 1 bytes in memory
    (data (i32.const 848) "#\00")
    
    ;; String from 856 with 13 bytes in memory
    (data (i32.const 856) " addressOf:=&\00")
    
    ;; String from 872 with 8 bytes in memory
    (data (i32.const 872) ", name:=\00")
    
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
        
    (local $newObject_B0000bZrK4X i32)
    (local $newObject_z0000cCVA0Q i32)
    (local $newObject_e0000dO1ZSs i32)
    (local $newObject_f0000e6h4I1 i32)
    (local $arrayOffset_50000f2C82H i32)
    (local $itemOffset_J0000gzZPaL i32)
    (local $i i32)
    (local $j i32)
    
    (call $objectArrayTest.modifyArray )
    (set_local $i (i32.const 0))
    ;; For i As Integer = 0 To ps.Length - 1
    
    (block $block_Z0000hYM0R4 
        (loop $loop_N0000iwodTZ
    
                    (br_if $block_Z0000hYM0R4 (i32.gt_s (get_local $i) (i32.sub (i32.load (i32.add (get_global $objectArrayTest.ps) (i32.const 4))) (i32.const 1))))
            (call $objectArrayTest.println (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 728) (call $i32.toString (get_local $i))) (i32.const 736)) (call $i32.toString (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))))) (i32.const 752)) (call $i32.toString (i32.load (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 16))))))
            (call $objectArrayTest.println (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 768) (call $f64.toString (f64.load (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 0))))) (i32.const 784)) (call $f64.toString (f64.load (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 8))))) (i32.const 792)))
            (call $objectArrayTest.dump (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))))
            ;; For loop control step: (i32.const 1)
            (set_local $i (i32.add (get_local $i) (i32.const 1)))
            (br $loop_N0000iwodTZ)
            ;; For Loop Next On loop_N0000iwodTZ
    
        )
    )
    (set_local $j (i32.const 0))
    ;; For j As Integer = 0 To lines.Length - 1
    
    (block $block_D0000jQrGUq 
        (loop $loop_h0000kQ5e92
    
                    (br_if $block_D0000jQrGUq (i32.gt_s (get_local $j) (i32.sub (i32.load (i32.add (get_global $objectArrayTest.lines) (i32.const 4))) (i32.const 1))))
            (call $objectArrayTest.viewLine (i32.load (i32.add (i32.add (get_global $objectArrayTest.lines) (i32.const 8)) (i32.mul (get_local $j) (i32.const 4)))))
            ;; For loop control step: (i32.const 1)
            (set_local $j (i32.add (get_local $j) (i32.const 1)))
            (br $loop_h0000kQ5e92)
            ;; For Loop Next On loop_h0000kQ5e92
    
        )
    )
    )
    
    
    (func $objectArrayTest.modifyArray  
        ;; Public Function modifyArray() As void
        
    (local $i i32)
    
    (set_local $i (i32.const 0))
    ;; For i As Integer = 0 To ps.Length - 1
    
    (block $block_B0000l0oA77 
        (loop $loop_J0000mG0C6e
    
                    (br_if $block_B0000l0oA77 (i32.gt_s (get_local $i) (i32.sub (i32.load (i32.add (get_global $objectArrayTest.ps) (i32.const 4))) (i32.const 1))))
            (f64.store (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 0)) (call $Math.random ))
            (f64.store (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 8)) (f64.mul (call $Math.random ) (f64.convert_s/i32 (i32.const 100))))
            (call $objectArrayTest.warning (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 800) (call $f64.toString (f64.load (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 0))))) (i32.const 832)) (call $f64.toString (f64.load (i32.add (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))) (i32.const 8))))) (i32.const 840)))
            ;; For loop control step: (i32.const 1)
            (set_local $i (i32.add (get_local $i) (i32.const 1)))
            (br $loop_J0000mG0C6e)
            ;; For Loop Next On loop_J0000mG0C6e
    
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
    
(local $arrayOffset_50000f2C82H i32)
(local $itemOffset_J0000gzZPaL i32)
(local $newObject_B0000bZrK4X i32)
(local $newObject_z0000cCVA0Q i32)
(local $newObject_e0000dO1ZSs i32)
(local $newObject_f0000e6h4I1 i32)


;; Save (i32.const 4) array element data to memory:
;; Array memory block begin at location: (get_local $arrayOffset_50000f2C82H)
(set_local $arrayOffset_50000f2C82H (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 4) (i32.const 4))) (i32.const 7)))
;; class_id/typealias_enum i32 data: (i32.const 24)/array(Of intptr)
(i32.store (get_local $arrayOffset_50000f2C82H) (i32.const 24))
(i32.store (i32.add (get_local $arrayOffset_50000f2C82H) (i32.const 4)) (i32.const 4))
;; End of byte marks meta data, start write data blocks
(set_local $itemOffset_J0000gzZPaL (i32.add (get_local $arrayOffset_50000f2C82H) (i32.const 8)))
(set_local $newObject_B0000bZrK4X (call $global.ObjectManager.Allocate (i32.const 20) (i32.const 24)))
;; set field [arrayObjects.Point::x]
(f64.store (i32.add (get_local $newObject_B0000bZrK4X) (i32.const 0)) (f64.convert_s/i32 (i32.const 100)))
;; set field [arrayObjects.Point::y]
(f64.store (i32.add (get_local $newObject_B0000bZrK4X) (i32.const 8)) (f64.convert_s/i32 (i32.const 500)))
;; set field [arrayObjects.Point::name]
(i32.store (i32.add (get_local $newObject_B0000bZrK4X) (i32.const 16)) (i32.const 13))
(i32.store (i32.add (get_local $itemOffset_J0000gzZPaL) (i32.const 0)) (get_local $newObject_B0000bZrK4X))
(set_local $newObject_z0000cCVA0Q (call $global.ObjectManager.Allocate (i32.const 20) (i32.const 24)))
;; set field [arrayObjects.Point::x]
(f64.store (i32.add (get_local $newObject_z0000cCVA0Q) (i32.const 0)) (f64.convert_s/i32 (i32.const 1)))
;; set field [arrayObjects.Point::y]
(f64.store (i32.add (get_local $newObject_z0000cCVA0Q) (i32.const 8)) (f64.mul (f64.load (i32.add (get_local $newObject_z0000cCVA0Q) (i32.const 0))) (f64.convert_s/i32 (i32.const 999))))
;; set field [arrayObjects.Point::name]
(i32.store (i32.add (get_local $newObject_z0000cCVA0Q) (i32.const 16)) (i32.const 696))
(i32.store (i32.add (get_local $itemOffset_J0000gzZPaL) (i32.const 4)) (get_local $newObject_z0000cCVA0Q))
(set_local $newObject_e0000dO1ZSs (call $global.ObjectManager.Allocate (i32.const 20) (i32.const 24)))
;; set field [arrayObjects.Point::name]
(i32.store (i32.add (get_local $newObject_e0000dO1ZSs) (i32.const 16)) (i32.const 712))
;; set field [arrayObjects.Point::x]
(f64.store (i32.add (get_local $newObject_e0000dO1ZSs) (i32.const 0)) (f64.const 0))
;; set field [arrayObjects.Point::y]
(f64.store (i32.add (get_local $newObject_e0000dO1ZSs) (i32.const 8)) (f64.const 0))
(i32.store (i32.add (get_local $itemOffset_J0000gzZPaL) (i32.const 8)) (get_local $newObject_e0000dO1ZSs))
(set_local $newObject_f0000e6h4I1 (call $global.ObjectManager.Allocate (i32.const 20) (i32.const 24)))
;; set field [arrayObjects.Point::name]
(i32.store (i32.add (get_local $newObject_f0000e6h4I1) (i32.const 16)) (i32.const 720))
;; set field [arrayObjects.Point::x]
(f64.store (i32.add (get_local $newObject_f0000e6h4I1) (i32.const 0)) (f64.const 0))
;; set field [arrayObjects.Point::y]
(f64.store (i32.add (get_local $newObject_f0000e6h4I1) (i32.const 8)) (f64.const 0))
(i32.store (i32.add (get_local $itemOffset_J0000gzZPaL) (i32.const 12)) (get_local $newObject_f0000e6h4I1))
;; Assign array memory data to another expression
(set_global $objectArrayTest.ps (get_local $arrayOffset_50000f2C82H))
)

    (func $objectArrayTest.constructor  
    ;; Public Function constructor() As void
    
(local $item i32)
(local $i i32)
(local $newObject_C0000p099Xm i32)
(local $newObject_U0000qPBIhL i32)
(local $arrayOffset_30000r67J02 i32)
(local $itemOffset_w0000sCaQ3E i32)

(set_local $item (i32.const 0))
(set_local $i (i32.const 0))
;; For i As Integer = 0 To ps.Length - 1

(block $block_y0000nBpZ4I 
    (loop $loop_p0000oHLs2W

                (br_if $block_y0000nBpZ4I (i32.gt_s (get_local $i) (i32.sub (i32.load (i32.add (get_global $objectArrayTest.ps) (i32.const 4))) (i32.const 1))))
        (set_local $item (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))))
        (call $objectArrayTest.println (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 848) (call $i32.toString (get_local $i))) (i32.const 856)) (call $i32.toString (get_local $item))) (i32.const 872)) (call $i32.toString (i32.load (i32.add (get_local $item) (i32.const 16))))))
        (call $objectArrayTest.dump (get_local $item))
        ;; For loop control step: (i32.const 1)
        (set_local $i (i32.add (get_local $i) (i32.const 1)))
        (br $loop_p0000oHLs2W)
        ;; For Loop Next On loop_p0000oHLs2W

    )
)

;; Save (i32.const 2) array element data to memory:
;; Array memory block begin at location: (get_local $arrayOffset_30000r67J02)
(set_local $arrayOffset_30000r67J02 (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 2) (i32.const 4))) (i32.const 7)))
;; class_id/typealias_enum i32 data: (i32.const 352)/array(Of intptr)
(i32.store (get_local $arrayOffset_30000r67J02) (i32.const 352))
(i32.store (i32.add (get_local $arrayOffset_30000r67J02) (i32.const 4)) (i32.const 2))
;; End of byte marks meta data, start write data blocks
(set_local $itemOffset_w0000sCaQ3E (i32.add (get_local $arrayOffset_30000r67J02) (i32.const 8)))
(set_local $newObject_C0000p099Xm (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 352)))
;; set field [arrayObjects.line::a]
(i32.store (i32.add (get_local $newObject_C0000p099Xm) (i32.const 4)) (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (i32.const 0) (i32.const 4)))))
;; set field [arrayObjects.line::b]
(i32.store (i32.add (get_local $newObject_C0000p099Xm) (i32.const 0)) (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (i32.const 1) (i32.const 4)))))
(i32.store (i32.add (get_local $itemOffset_w0000sCaQ3E) (i32.const 0)) (get_local $newObject_C0000p099Xm))
(set_local $newObject_U0000qPBIhL (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 352)))
;; set field [arrayObjects.line::a]
(i32.store (i32.add (get_local $newObject_U0000qPBIhL) (i32.const 4)) (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (i32.const 2) (i32.const 4)))))
;; set field [arrayObjects.line::b]
(i32.store (i32.add (get_local $newObject_U0000qPBIhL) (i32.const 0)) (i32.load (i32.add (i32.add (get_global $objectArrayTest.ps) (i32.const 8)) (i32.mul (i32.const 3) (i32.const 4)))))
(i32.store (i32.add (get_local $itemOffset_w0000sCaQ3E) (i32.const 4)) (get_local $newObject_U0000qPBIhL))
;; Assign array memory data to another expression
(set_global $objectArrayTest.lines (get_local $arrayOffset_30000r67J02))
)

    (start $Application_SubNew)
)