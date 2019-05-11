(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/11/2019 3:31:26 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    
    
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
    ;; export from VB.NET module: [logicalTest]
    
    (export "logicalTest.Main" (func $Main))
    
     

    ;; functions in [logicalTest]
    
    (func $logicalTest.Main  
        ;; Public Function Main() As void
        
    (local $displayWidth f32)
    (local $x f64)
    (local $y f64)
    (local $r f64)
    (local $displayHeight f32)
    
    
    (if (i32.add (i32.add (i32.add (f64.lt (f64.sub (f64.promote/f32 (get_local $displayWidth)) (f64.add (get_local $x) (get_local $r))) (f64.convert_s/i32 (i32.const 0))) (f64.lt (f64.sub (get_local $x) (get_local $r)) (f64.convert_s/i32 (i32.const 0)))) (f64.lt (f64.sub (f64.promote/f32 (get_local $displayHeight)) (f64.add (get_local $y) (get_local $r))) (f64.convert_s/i32 (i32.const 0)))) (f64.lt (f64.sub (get_local $y) (get_local $r)) (f64.convert_s/i32 (i32.const 0)))) 
        (then
            
        ) 
    )
    )
    
    
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $constructor )
)

(func $logicalTest.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)