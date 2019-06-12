(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 6/12/2019 7:11:03 PM
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
    (global $global.ObjectManager (mut i32) (i32.const 288))

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
    
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 13 with 272 bytes in memory
    ;;
    ;; class objectarrayInitModel.[13] circle
    ;;
    (data (i32.const 13) "eyJjb21tZW50IjpudWxsLCJjbGFzcyI6ImNpcmNsZSIsImNsYXNzX2lkIjoxMywiZmllbGRzIjp7IngiOnsiZ2VuZXJpYyI6W10sInJhdyI6ImYzMiIsInR5cGUiOjN9LCJ5Ijp7ImdlbmVyaWMiOltdLCJyYXciOiJmMzIiLCJ0eXBlIjozfX0sImlzU3RydWN0IjpmYWxzZSwibWV0aG9kcyI6e30sIm5hbWVzcGFjZSI6Im9iamVjdGFycmF5SW5pdE1vZGVsIn0=\00")

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
    (global $objectArrayInitlizeTest.list (mut i32) (i32.const 0))

    ;; Export methods of this module
    (export "global.GetMemorySize" (func $global.GetMemorySize))

     




    ;; Application Initialize
    ;; 
    ;; Sub New
    (func $Application_SubNew
        ;; call of the global variable initialize
        (call $global.initializer )

        (call $objectArrayInitlizeTest.constructor )
    )

    ;; Initializer for global variables if it is not a primitive abstract type
    (func $global.initializer  
    ;; Public Function initializer() As void
    
(local $arrayOffset_o0000blO9re i32)


;; Save (i32.const 100) array element data to memory:
;; Array memory block begin at location: (get_local $arrayOffset_o0000blO9re)
(set_local $arrayOffset_o0000blO9re (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 100) (i32.const 4))) (i32.const 7)))
;; class_id/typealias_enum i32 data: (i32.const 13)/array(Of intptr)
(i32.store (get_local $arrayOffset_o0000blO9re) (i32.const 13))
(i32.store (i32.add (get_local $arrayOffset_o0000blO9re) (i32.const 4)) (i32.const 100))
;; End of byte marks meta data, start write data blocks
;; Assign array memory data to another expression
(set_global $objectArrayInitlizeTest.list (get_local $arrayOffset_o0000blO9re))
)

    (func $objectArrayInitlizeTest.constructor  
    ;; Public Function constructor() As void
    
(local $arrayOffset_o0000blO9re i32)
(local $i i32)
(local $newObject_60000eaEMLD i32)

(set_local $i (i32.const 0))
;; For i As Integer = 0 To list.Length - 1

(block $block_B0000c1S0T0 
    (loop $loop_V0000d1f4sD

                (br_if $block_B0000c1S0T0 (i32.gt_s (get_local $i) (i32.sub (i32.load (i32.add (get_global $objectArrayInitlizeTest.list) (i32.const 4))) (i32.const 1))))
        (set_local $newObject_60000eaEMLD (call $global.ObjectManager.Allocate (i32.const 8) (i32.const 13)))
        ;; set field [objectarrayInitModel.circle::x]
        (f32.store (i32.add (get_local $newObject_60000eaEMLD) (i32.const 0)) (f32.demote/f64 (call $Math.random )))
        ;; set field [objectarrayInitModel.circle::y]
        (f32.store (i32.add (get_local $newObject_60000eaEMLD) (i32.const 4)) (f32.demote/f64 (call $Math.random )))
        (i32.store (i32.add (i32.add (get_global $objectArrayInitlizeTest.list) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4))) (get_local $newObject_60000eaEMLD))
        ;; For loop control step: (i32.const 1)
        (set_local $i (i32.add (get_local $i) (i32.const 1)))
        (br $loop_V0000d1f4sD)
        ;; For Loop Next On loop_V0000d1f4sD

    )
)
)

    (start $Application_SubNew)
)