(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/4/2019 10:18:51 AM
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
    ;; Declare Function print Lib "console" Alias "log" (data As string) As void
    (func $Runtest.print (import "console" "log") (param $data i32) )
    ;; Declare Function i32.toString Lib "string" Alias "toString" (x As i32) As string
    (func $i32.toString (import "string" "toString") (param $x i32) (result i32))
    ;; Declare Function f64.toString Lib "string" Alias "toString" (x As f64) As string
    (func $f64.toString (import "string" "toString") (param $x f64) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 704))

    ;; Memory data for string constant
    
    ;; String from 1 with 10 bytes in memory
    (data (i32.const 1) "[55555555]\00")

    ;; String from 269 with 5 bytes in memory
    (data (i32.const 269) "55555\00")

    ;; String from 540 with 25 bytes in memory
    (data (i32.const 540) "y of the globalobject is \00")

    ;; String from 566 with 25 bytes in memory
    (data (i32.const 566) "y of the globalobject is \00")

    ;; String from 592 with 16 bytes in memory
    (data (i32.const 592) "{55, 55, 555, 5}\00")

    ;; String from 609 with 5 bytes in memory
    (data (i32.const 609) "y is \00")

    ;; String from 615 with 57 bytes in memory
    (data (i32.const 615) "min distance of two circle center is (a.radius+b.radius) \00")

    ;; String from 673 with 18 bytes in memory
    (data (i32.const 673) "y after update is \00")

    ;; String from 692 with 11 bytes in memory
    (data (i32.const 692) "XXXXXXXXXX!\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    
    ;; String from 12 with 256 bytes in memory
    (data (i32.const 12) "eyJtZW1vcnlQdHIiOnsiVmFsdWUiOjEyfSwiQ2xhc3MiOiJSZWN0YW5nbGVNb2RlbCIsIkZpZWxkcyI6eyJuYW1lIjp7ImdlbmVyaWMiOltdfSwieCI6eyJnZW5lcmljIjpbXX0sInkiOnsiZ2VuZXJpYyI6W119LCJ3Ijp7ImdlbmVyaWMiOltdfSwiaCI6eyJnZW5lcmljIjpbXX19LCJNZXRob2RzIjp7fSwiTmFtZXNwYWNlIjpudWxsfQ==\00")

    ;; String from 275 with 264 bytes in memory
    (data (i32.const 275) "eyJtZW1vcnlQdHIiOnsiVmFsdWUiOjI3NX0sIkNsYXNzIjoiQ2lyY2xlTW9kZWwiLCJGaWVsZHMiOnsibm9kZU5hbWUiOnsiZ2VuZXJpYyI6W119LCJ4Ijp7ImdlbmVyaWMiOltdfSwieSI6eyJnZW5lcmljIjpbXX0sInJhZGl1cyI6eyJnZW5lcmljIjpbXX19LCJNZXRob2RzIjp7fSwiTmFtZXNwYWNlIjoibW9kdWxlQ29udGFpbmVyLm5hbWUxIn0=\00")

    ;; Global variables in this module
    (global $Runtest.globalObject (mut i32) (i32.const 0))

(global $Runtest.globalObject2 (mut i32) (i32.const 0))

    ;; Export methods of this module
    ;; export from VB.NET module: [Runtest]
    
    (export "Runtest.globalMemberTest" (func $Runtest.globalMemberTest))
    (export "Runtest.test" (func $Runtest.test))
    
     

    ;; functions in [Runtest]
    
    (func $Runtest.globalMemberTest  (result i32)
        ;; Public Function globalMemberTest() As i32
        
    (set_global $Runtest.globalObject (call $Runtest.returnObjecttest (i64.const 777)))
    (call $Runtest.print (call $string.add (i32.const 540) (call $i32.toString (i32.load (i32.add (get_global $Runtest.globalObject) (i32.const 8))))))
    (call $Runtest.print (call $string.add (i32.const 566) (call $i32.toString (i32.load (i32.add (get_global $Runtest.globalObject) (i32.const 8))))))
    (return (i32.trunc_s/f64 (f64.mul (f64.convert_s/i32 (i32.load (i32.add (get_global $Runtest.globalObject) (i32.const 4)))) (f64.load (i32.add (get_global $Runtest.globalObject) (i32.const 12))))))
    )
    (func $Runtest.test  
        ;; Public Function test() As void
        (local $s i32)
    (local $c i32)
    
    ;; Initialize a object instance of [CircleModel]
    ;; Object memory block begin at location: (get_global $global.ObjectManager)
    ;; set field [moduleContainer.name1.CircleModel::radius]
    (f64.store (i32.add (get_global $global.ObjectManager) (i32.const 12)) (f64.convert_s/i32 (i32.const 100001)))
    ;; set field [moduleContainer.name1.CircleModel::x]
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.sub (i32.const 0) (i32.const 1)))
    ;; set field [moduleContainer.name1.CircleModel::y]
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 8)) (i32.trunc_s/f64 (f64.const 1.0009)))
    ;; set field [moduleContainer.name1.CircleModel::nodeName]
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 0)) (i32.const 592))
    ;; Offset object manager with 20 bytes.
    (set_global $global.ObjectManager (i32.add (get_global $global.ObjectManager) (i32.const 20)))
    ;; Initialize an object memory block with 20 bytes data
    
    (set_local $s (get_global $global.ObjectManager))
    (call $Runtest.print (call $f64.toString (f64.load (i32.add (get_local $s) (i32.const 12)))))
    (set_local $c (call $Runtest.returnObjecttest (i64.const 99999)))
    (call $Runtest.print (call $string.add (i32.const 609) (call $i32.toString (i32.load (i32.add (get_local $c) (i32.const 8))))))
    (call $Runtest.print (call $string.add (i32.const 615) (call $f64.toString (f64.add (f64.load (i32.add (get_local $s) (i32.const 12))) (f64.load (i32.add (get_local $c) (i32.const 12)))))))
    (i32.store (i32.add (get_local $c) (i32.const 8)) (i32.trunc_s/f64 (f64.sub (f64.const 0) (f64.const 99.999))))
    (call $Runtest.print (call $string.add (i32.const 673) (call $i32.toString (i32.load (i32.add (get_local $c) (i32.const 8))))))
    )
    (func $Runtest.returnObjecttest (param $radius i64) (result i32)
        ;; Public Function returnObjecttest(radius As i64) As intptr
        
    
    ;; Initialize a object instance of [CircleModel]
    ;; Object memory block begin at location: (get_global $global.ObjectManager)
    ;; set field [moduleContainer.name1.CircleModel::nodeName]
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 0)) (i32.const 692))
    ;; set field [moduleContainer.name1.CircleModel::radius]
    (f64.store (i32.add (get_global $global.ObjectManager) (i32.const 12)) (f64.convert_s/i64 (get_local $radius)))
    ;; set field [moduleContainer.name1.CircleModel::x]
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.wrap/i64 (i64.add (get_local $radius) (i64.const 1))))
    ;; Offset object manager with 20 bytes.
    (set_global $global.ObjectManager (i32.add (get_global $global.ObjectManager) (i32.const 20)))
    ;; Initialize an object memory block with 20 bytes data
    
    (return (get_global $global.ObjectManager))
    )
    

;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew


;; Initialize a object instance of [RectangleModel]
;; Object memory block begin at location: (get_global $global.ObjectManager)
;; set field [RectangleModel::h]
(i32.store (i32.add (get_global $global.ObjectManager) (i32.const 16)) (i32.const 500))
;; set field [RectangleModel::w]
(i32.store (i32.add (get_global $global.ObjectManager) (i32.const 12)) (i32.const 900))
;; Offset object manager with 20 bytes.
(set_global $global.ObjectManager (i32.add (get_global $global.ObjectManager) (i32.const 20)))
;; Initialize an object memory block with 20 bytes data

(set_global $Runtest.globalObject2 (get_global $global.ObjectManager))
(i32.store (i32.add (get_global $Runtest.globalObject2) (i32.const 0)) (call $i32.toString (i32.load (i32.add (get_global $Runtest.globalObject) (i32.const 0)))))
)

(start $Application_SubNew)

)