(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/18/2019 2:22:27 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 729))

    ;; Memory data for string constant
    
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 11 with 356 bytes in memory
    ;;
    ;; class testDifference.[11] circleClass
    ;;
    (data (i32.const 11) "eyJjb21tZW50IjpudWxsLCJtZW1vcnlQdHIiOnsiVmFsdWUiOjExfSwiY2xhc3MiOiJjaXJjbGVDbGFzcyIsImNsYXNzX2lkIjoxMSwiZmllbGRzIjp7IngiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImY2NCIsInR5cGUiOjR9LCJ5Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJmNjQiLCJ0eXBlIjo0fSwiciI6eyJnZW5lcmljIjpbXSwicmF3IjoiaTMyIiwidHlwZSI6MX19LCJpc1N0cnVjdCI6ZmFsc2UsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOiJ0ZXN0RGlmZmVyZW5jZSJ9\00")
    
    ;; String from 368 with 360 bytes in memory
    ;;
    ;; structure testDifference.[368] circleStruct
    ;;
    (data (i32.const 368) "eyJjb21tZW50IjpudWxsLCJtZW1vcnlQdHIiOnsiVmFsdWUiOjM2OH0sImNsYXNzIjoiY2lyY2xlU3RydWN0IiwiY2xhc3NfaWQiOjM2OCwiZmllbGRzIjp7InIiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImkzMiIsInR5cGUiOjF9LCJ5Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJmNjQiLCJ0eXBlIjo0fSwieCI6eyJnZW5lcmljIjpbXSwicmF3IjoiZjY0IiwidHlwZSI6NH19LCJpc1N0cnVjdCI6dHJ1ZSwibWV0aG9kcyI6e30sIm5hbWVzcGFjZSI6InRlc3REaWZmZXJlbmNlIn0=\00")

    ;; Global variables in this module
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

    ;; Export methods of this module
    ;; export from VB.NET module: [ClassStructureDifferenceTest]
    
    (export "ClassStructureDifferenceTest.Main" (func $ClassStructureDifferenceTest.Main))
    
     

    ;; functions in [ClassStructureDifferenceTest]
    
    (func $ClassStructureDifferenceTest.Main  
        ;; Public Function Main() As void
        
    (local $newObject_9a020000 i32)
    (local $c1 i32)
    (local $c2 i32)
    (local $newObject_9b020000 i32)
    (local $s1 i32)
    (local $newObject_9c020000 i32)
    (local $s2 i32)
    (local $newObject_9d020000 i32)
    
    
    ;; Initialize a object instance of [[11]circleClass]
    ;; Object memory block begin at location: (get_local $newObject_9a020000)
    (set_local $newObject_9a020000 (get_global $global.ObjectManager))
    ;; Offset object manager with 20 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $newObject_9a020000) (i32.const 20)))
    ;; set field [testDifference.circleClass::r]
    (i32.store (i32.add (get_local $newObject_9a020000) (i32.const 16)) (i32.const 100))
    ;; set field [testDifference.circleClass::x]
    (f64.store (i32.add (get_local $newObject_9a020000) (i32.const 0)) (f64.convert_s/i32 (i32.const 1)))
    ;; set field [testDifference.circleClass::y]
    (f64.store (i32.add (get_local $newObject_9a020000) (i32.const 8)) (f64.convert_s/i32 (i32.const 1)))
    ;; Initialize an object memory block with 20 bytes data
    
    (set_local $c1 (get_local $newObject_9a020000))
    (set_local $c2 (get_local $c1))
    (i32.store (i32.add (get_local $c1) (i32.const 16)) (i32.sub (i32.const 0) (i32.const 99999)))
    
    ;; Initialize a object instance of [[368]circleStruct]
    ;; Object memory block begin at location: (get_local $newObject_9b020000)
    (set_local $newObject_9b020000 (get_global $global.ObjectManager))
    ;; Offset object manager with 20 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $newObject_9b020000) (i32.const 20)))
    ;; set field [testDifference.circleStruct::y]
    (f64.store (i32.add (get_local $newObject_9b020000) (i32.const 4)) (f64.convert_s/i32 (i32.const 99)))
    ;; set field [testDifference.circleStruct::x]
    (f64.store (i32.add (get_local $newObject_9b020000) (i32.const 12)) (f64.load (i32.add (get_local $newObject_9b020000) (i32.const 4))))
    ;; set field [testDifference.circleStruct::r]
    (i32.store (i32.add (get_local $newObject_9b020000) (i32.const 0)) (i32.trunc_s/f64 (f64.mul (f64.load (i32.add (get_local $newObject_9b020000) (i32.const 12))) (f64.load (i32.add (get_local $newObject_9b020000) (i32.const 4))))))
    ;; Initialize an object memory block with 20 bytes data
    
    (set_local $s1 (get_local $newObject_9b020000))
    
    ;; Initialize a object instance of [[368]circleStruct]
    ;; Object memory block begin at location: (get_local $newObject_9c020000)
    (set_local $newObject_9c020000 (get_global $global.ObjectManager))
    ;; Offset object manager with 20 bytes.
    (set_global $global.ObjectManager (i32.add (get_local $newObject_9c020000) (i32.const 20)))
    ;; set field [testDifference.circleStruct::r]
    (i32.store (i32.add (get_local $newObject_9c020000) (i32.const 0)) (i32.load (i32.add (get_local $s1) (i32.const 0))))
    ;; set field [testDifference.circleStruct::y]
    (f64.store (i32.add (get_local $newObject_9c020000) (i32.const 4)) (f64.load (i32.add (get_local $s1) (i32.const 4))))
    ;; set field [testDifference.circleStruct::x]
    (f64.store (i32.add (get_local $newObject_9c020000) (i32.const 12)) (f64.load (i32.add (get_local $s1) (i32.const 12))))
    ;; Initialize an object memory block with 20 bytes data
    
    (set_local $s2 (get_local $newObject_9c020000))
    (i32.store (i32.add (get_local $s1) (i32.const 0)) (i32.sub (i32.const 0) (i32.const 88888)))
    (call $ClassStructureDifferenceTest.modifyTest (get_local $newObject_9d020000))
    )
    
    
    (func $ClassStructureDifferenceTest.modifyTest (param $s i32) 
        ;; Public Function modifyTest(s As intptr) As void
        
    
    
    (f64.store (i32.add (get_local $s) (i32.const 12)) (f64.convert_s/i64 (i64.const 2222229999)))
    )
    
    
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $ClassStructureDifferenceTest.constructor )
)

(func $ClassStructureDifferenceTest.constructor  
    ;; Public Function constructor() As void
    


(call $ClassStructureDifferenceTest.Main )
)

(start $Application_SubNew)
)