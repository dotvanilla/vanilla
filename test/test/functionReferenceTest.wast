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

    ;; Declare Function Math.exp Lib "Math" Alias "exp" (x As f64) As f64
    (func $Math.exp (import "Math" "exp") (param $x f64) (result f64))
;; Declare Function Math.pow Lib "Math" Alias "pow" (a As f64, b As f64) As f64
    (func $Math.pow (import "Math" "pow") (param $a f64) (param $b f64) (result f64))
;; Declare Function Math.cos Lib "Math" Alias "cos" (x As f64) As f64
    (func $Math.cos (import "Math" "cos") (param $x f64) (result f64))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 11))

    ;; Memory data for string constant
    
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    

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

    ;; Export methods of this module
    ;; export from VB.NET module: [functionReferenceTest]
    
    (export "functionReferenceTest.Main" (func $functionReferenceTest.Main))
    
     

    ;; functions in [functionReferenceTest]
    
    (func $functionReferenceTest.Main  
        ;; Public Function Main() As void
        
    (local $x i32)
    (local $y i32)
    (local $y2 f64)
    (local $g f64)
    (local $z f64)
    
    (call $functionReferenceTest.Main )
    (set_local $x (i32.trunc_s/f64 (call $Math.exp (f64.convert_s/i32 (i32.const 9)))))
    (set_local $y (i32.trunc_s/f64 (call $Math.pow (get_global $Math.E) (f64.convert_s/i32 (i32.const 9)))))
    (set_local $y2 (call $Math.pow (get_global $Math.E) (f64.convert_s/i32 (i32.const 9))))
    (set_local $g (call $Math.cos (f64.const 0.5)))
    (set_local $z (call $Math.pow (f64.convert_s/i32 (get_local $x)) (f64.convert_s/i32 (i32.const 2))))
    )
    
    
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $functionReferenceTest.constructor )
)

(func $functionReferenceTest.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)