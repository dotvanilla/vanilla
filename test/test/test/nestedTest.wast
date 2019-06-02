(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 6/2/2019 9:53:52 AM
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
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 615))

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
    
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 13 with 312 bytes in memory
    ;;
    ;; class nestedTypes.[13] line
    ;;
    (data (i32.const 13) "eyJjb21tZW50IjpudWxsLCJtZW1vcnlQdHIiOnsiVmFsdWUiOjEzfSwiY2xhc3MiOiJsaW5lIiwiY2xhc3NfaWQiOjEzLCJmaWVsZHMiOnsiYSI6eyJnZW5lcmljIjpbXSwicmF3IjoiWzMyMl1wb2ludCIsInR5cGUiOjEwfSwiYiI6eyJnZW5lcmljIjpbXSwicmF3IjoiWzMyMl1wb2ludCIsInR5cGUiOjEwfX0sImlzU3RydWN0IjpmYWxzZSwibWV0aG9kcyI6e30sIm5hbWVzcGFjZSI6Im5lc3RlZFR5cGVzIn0=\00")
    
    ;; String from 322 with 292 bytes in memory
    ;;
    ;; structure nestedTypes.[322] point
    ;;
    (data (i32.const 322) "eyJjb21tZW50IjpudWxsLCJtZW1vcnlQdHIiOnsiVmFsdWUiOjMyMn0sImNsYXNzIjoicG9pbnQiLCJjbGFzc19pZCI6MzIyLCJmaWVsZHMiOnsieSI6eyJnZW5lcmljIjpbXSwicmF3IjoiZjMyIiwidHlwZSI6M30sIngiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImYzMiIsInR5cGUiOjN9fSwiaXNTdHJ1Y3QiOnRydWUsIm1ldGhvZHMiOnt9LCJuYW1lc3BhY2UiOiJuZXN0ZWRUeXBlcyJ9\00")

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
    



)

    (func $nestedTest.constructor  
    ;; Public Function constructor() As void
    
(local $newObject_9a020000 i32)
(local $newObject_9b020000 i32)
(local $memoryCopy_9c020000 i32)
(local $newObject_9d020000 i32)


;; Initialize a object instance of [[13]line]
;; Object memory block begin at location: (get_local $newObject_9a020000)
(set_local $newObject_9a020000 (call $global.ObjectManager.Allocate (i32.const 16) (i32.const 13)))
(set_local $newObject_9b020000 (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 322)))
;; set field [nestedTypes.point::x]
(f32.store (i32.add (get_local $newObject_9b020000) (i32.const 4)) (f32.convert_s/i32 (i32.const 99)))
;; set field [nestedTypes.point::y]
(f32.store (i32.add (get_local $newObject_9b020000) (i32.const 0)) (f32.convert_s/i32 (i32.const 88)))
;; Copy memory of structure value:
(set_local $memoryCopy_9c020000 (i32.add (get_local $newObject_9a020000) (i32.const 0)))
;; set field [nestedTypes.point::y]
(f32.store (i32.add (get_local $memoryCopy_9c020000) (i32.const 0)) (f32.load (i32.add (get_local $newObject_9b020000) (i32.const 0))))
;; set field [nestedTypes.point::x]
(f32.store (i32.add (get_local $memoryCopy_9c020000) (i32.const 4)) (f32.load (i32.add (get_local $newObject_9b020000) (i32.const 4))))
;; set field [nestedTypes.line::b]
(i32.store (i32.add (get_local $newObject_9a020000) (i32.const 8)) (i32.const 0))
;; Initialize an object memory block with 16 bytes data

(set_global $nestedTest.line (get_local $newObject_9a020000))
(set_local $newObject_9d020000 (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 322)))
;; set field [nestedTypes.point::x]
(f32.store (i32.add (get_local $newObject_9d020000) (i32.const 4)) (f32.convert_s/i32 (i32.const 100)))
;; set field [nestedTypes.point::y]
(f32.store (i32.add (get_local $newObject_9d020000) (i32.const 0)) (f32.convert_s/i32 (i32.const 50000)))
(i32.store (i32.add (get_global $nestedTest.line) (i32.const 8)) (get_local $newObject_9d020000))
)

    (start $Application_SubNew)
)