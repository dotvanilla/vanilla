(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/11/2019 3:43:13 PM
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
    (global $global.ObjectManager (mut i32) (i32.const 1058))

    ;; Memory data for string constant
        
    ;; String from 11 with 5 bytes in memory
    (data (i32.const 11) "55555\00")
    
    ;; String from 438 with 10 bytes in memory
    (data (i32.const 438) "[55555555]\00")
    
    ;; String from 894 with 25 bytes in memory
    (data (i32.const 894) "y of the globalobject is \00")
    
    ;; String from 920 with 25 bytes in memory
    (data (i32.const 920) "y of the globalobject is \00")
    
    ;; String from 946 with 16 bytes in memory
    (data (i32.const 946) "{55, 55, 555, 5}\00")
    
    ;; String from 963 with 5 bytes in memory
    (data (i32.const 963) "y is \00")
    
    ;; String from 969 with 57 bytes in memory
    (data (i32.const 969) "min distance of two circle center is (a.radius+b.radius) \00")
    
    ;; String from 1027 with 18 bytes in memory
    (data (i32.const 1027) "y after update is \00")
    
    ;; String from 1046 with 11 bytes in memory
    (data (i32.const 1046) "XXXXXXXXXX!\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 17 with 420 bytes in memory
    ;;
    ;; class moduleContainer.name1.[17] CircleModel
    ;;
    (data (i32.const 17) "eyJtZW1vcnlQdHIiOnsiVmFsdWUiOjE3fSwiY2xhc3MiOiJDaXJjbGVNb2RlbCIsImNsYXNzX2lkIjoxNywiZmllbGRzIjp7Im5vZGVOYW1lIjp7ImdlbmVyaWMiOltdLCJyYXciOiJzdHJpbmciLCJ0eXBlIjo1fSwieCI6eyJnZW5lcmljIjpbXSwicmF3IjoiaTMyIiwidHlwZSI6MX0sInkiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImkzMiIsInR5cGUiOjF9LCJyYWRpdXMiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImY2NCIsInR5cGUiOjR9fSwiaXNTdHJ1Y3QiOmZhbHNlLCJtZXRob2RzIjp7fSwibmFtZXNwYWNlIjoibW9kdWxlQ29udGFpbmVyLm5hbWUxIn0=\00")
    
    ;; String from 449 with 444 bytes in memory
    ;;
    ;; class [449] RectangleModel
    ;;
    (data (i32.const 449) "eyJtZW1vcnlQdHIiOnsiVmFsdWUiOjQ0OX0sImNsYXNzIjoiUmVjdGFuZ2xlTW9kZWwiLCJjbGFzc19pZCI6NDQ5LCJmaWVsZHMiOnsidyI6eyJnZW5lcmljIjpbXSwicmF3IjoiaTMyIiwidHlwZSI6MX0sInkiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImkzMiIsInR5cGUiOjF9LCJ4Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJpMzIiLCJ0eXBlIjoxfSwibmFtZSI6eyJnZW5lcmljIjpbXSwicmF3Ijoic3RyaW5nIiwidHlwZSI6NX0sImgiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImkzMiIsInR5cGUiOjF9fSwiaXNTdHJ1Y3QiOmZhbHNlLCJtZXRob2RzIjp7fSwibmFtZXNwYWNlIjpudWxsfQ==\00")

    ;; Global variables in this module
    (global $Math.E (mut f64) (f64.const 2.71828182845905))
(global $Math.PI (mut f64) (f64.const 3.14159265358979))
(global $Integer.MaxValue (mut i32) (i32.const 2147483647))
(global $Long.MaxValue (mut i64) (i64.const 9223372036854775807))
(global $Single.MaxValue (mut f32) (f32.const 340282356779733623858607532500980858880))
(global $Double.MaxValue (mut f64) (f64.const 179769313486231570814527423731704356798070567525844996598917476803157260780028538760589558632766878171540458953514382464234321326889464182768467546703537516986049910576551282076245490090389328944075868508455133942304583236903222948165808559332123348274797826204144723168738177180919299881250404026184124858368))
(global $Integer.MinValue (mut i32) (i32.const -2147483648))
(global $Long.MinValue (mut i64) (i64.const -9223372036854775808))
(global $Single.MinValue (mut f32) (f32.const -340282356779733623858607532500980858880))
(global $Double.MinValue (mut f64) (f64.const -179769313486231570814527423731704356798070567525844996598917476803157260780028538760589558632766878171540458953514382464234321326889464182768467546703537516986049910576551282076245490090389328944075868508455133942304583236903222948165808559332123348274797826204144723168738177180919299881250404026184124858368))
(global $Runtest.globalObject2 (mut i32) (i32.const 0))
(global $Runtest.globalObject (mut i32) (i32.const 0))

    ;; Export methods of this module
    ;; export from VB.NET module: [Runtest]
    
    (export "Runtest.globalMemberTest" (func $Runtest.globalMemberTest))
    (export "Runtest.test" (func $Runtest.test))
    
     

    ;; functions in [Runtest]
    
    (func $Runtest.globalMemberTest  (result i32)
        ;; Public Function globalMemberTest() As i32
        
    
    
    (set_global $Runtest.globalObject (call $Runtest.returnObjecttest (i64.const 777)))
    (call $Runtest.print (call $string.add (i32.const 894) (call $i32.toString (i32.load (i32.add (get_global $Runtest.globalObject) (i32.const 8))))))
    (call $Runtest.print (call $string.add (i32.const 920) (call $i32.toString (i32.load (i32.add (get_global $Runtest.globalObject) (i32.const 8))))))
    (return (i32.trunc_s/f64 (f64.mul (f64.convert_s/i32 (i32.load (i32.add (get_global $Runtest.globalObject) (i32.const 4)))) (f64.load (i32.add (get_global $Runtest.globalObject) (i32.const 12))))))
    )
    
    
    (func $Runtest.test  
        ;; Public Function test() As void
        
    (local $newObject_9a020000 i32)
    (local $s i32)
    (local $c i32)
    
    
    ;; Initialize a object instance of [[17]CircleModel]
    ;; Object memory block begin at location: (get_local $newObject_9a020000)
    (set_local $newObject_9a020000 (get_global $global.ObjectManager))
    ;; Offset object manager with 20 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $newObject_9a020000) (i32.const 20)))
    ;; set field [moduleContainer.name1.CircleModel::radius]
    (f64.store (i32.add (get_local $newObject_9a020000) (i32.const 12)) (f64.convert_s/i32 (i32.const 100001)))
    ;; set field [moduleContainer.name1.CircleModel::x]
    (i32.store (i32.add (get_local $newObject_9a020000) (i32.const 4)) (i32.sub (i32.const 0) (i32.const 1)))
    ;; set field [moduleContainer.name1.CircleModel::y]
    (i32.store (i32.add (get_local $newObject_9a020000) (i32.const 8)) (i32.trunc_s/f64 (f64.const 1.0009)))
    ;; set field [moduleContainer.name1.CircleModel::nodeName]
    (i32.store (i32.add (get_local $newObject_9a020000) (i32.const 0)) (i32.const 946))
    ;; Initialize an object memory block with 20 bytes data
    
    (set_local $s (get_local $newObject_9a020000))
    (call $Runtest.print (call $f64.toString (f64.load (i32.add (get_local $s) (i32.const 12)))))
    (set_local $c (call $Runtest.returnObjecttest (i64.const 99999)))
    (call $Runtest.print (call $string.add (i32.const 963) (call $i32.toString (i32.load (i32.add (get_local $c) (i32.const 8))))))
    (call $Runtest.print (call $string.add (i32.const 969) (call $f64.toString (f64.add (f64.load (i32.add (get_local $s) (i32.const 12))) (f64.load (i32.add (get_local $c) (i32.const 12)))))))
    (i32.store (i32.add (get_local $c) (i32.const 8)) (i32.trunc_s/f64 (f64.sub (f64.const 0) (f64.const 99.999))))
    (call $Runtest.print (call $string.add (i32.const 1027) (call $i32.toString (i32.load (i32.add (get_local $c) (i32.const 8))))))
    )
    
    
    (func $Runtest.returnObjecttest (param $radius i64) (result i32)
        ;; Public Function returnObjecttest(radius As i64 [ = 99999]) As intptr
        
    (local $newObject_9b020000 i32)
    
    
    ;; Initialize a object instance of [[17]CircleModel]
    ;; Object memory block begin at location: (get_local $newObject_9b020000)
    (set_local $newObject_9b020000 (get_global $global.ObjectManager))
    ;; Offset object manager with 20 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $newObject_9b020000) (i32.const 20)))
    ;; set field [moduleContainer.name1.CircleModel::nodeName]
    (i32.store (i32.add (get_local $newObject_9b020000) (i32.const 0)) (i32.const 1046))
    ;; set field [moduleContainer.name1.CircleModel::radius]
    (f64.store (i32.add (get_local $newObject_9b020000) (i32.const 12)) (f64.convert_s/i64 (get_local $radius)))
    ;; set field [moduleContainer.name1.CircleModel::x]
    (i32.store (i32.add (get_local $newObject_9b020000) (i32.const 4)) (i32.wrap/i64 (i64.add (get_local $radius) (i64.const 1))))
    ;; set field [moduleContainer.name1.CircleModel::y]
    (i32.store (i32.add (get_local $newObject_9b020000) (i32.const 8)) (i32.const 0))
    ;; Initialize an object memory block with 20 bytes data
    
    (return (get_local $newObject_9b020000))
    )
    
    
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $Runtest.constructor )
)

(func $Runtest.constructor  
    ;; Public Function constructor() As void
    
(local $newObject_9c020000 i32)


;; Initialize a object instance of [[449]RectangleModel]
;; Object memory block begin at location: (get_local $newObject_9c020000)
(set_local $newObject_9c020000 (get_global $global.ObjectManager))
;; Offset object manager with 20 bytes.
(set_global $global.ObjectManager (i32.add (get_local $newObject_9c020000) (i32.const 20)))
;; set field [RectangleModel::h]
(i32.store (i32.add (get_local $newObject_9c020000) (i32.const 16)) (i32.const 500))
;; set field [RectangleModel::w]
(i32.store (i32.add (get_local $newObject_9c020000) (i32.const 0)) (i32.const 900))
;; set field [RectangleModel::y]
(i32.store (i32.add (get_local $newObject_9c020000) (i32.const 4)) (i32.const 0))
;; set field [RectangleModel::x]
(i32.store (i32.add (get_local $newObject_9c020000) (i32.const 8)) (i32.const 0))
;; set field [RectangleModel::name]
(i32.store (i32.add (get_local $newObject_9c020000) (i32.const 12)) (i32.const 438))
;; Initialize an object memory block with 20 bytes data

(set_global $Runtest.globalObject2 (get_local $newObject_9c020000))
(i32.store (i32.add (get_global $Runtest.globalObject2) (i32.const 12)) (i32.load (i32.add (get_global $Runtest.globalObject) (i32.const 0))))
)

(start $Application_SubNew)
)