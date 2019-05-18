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
    (global $global.ObjectManager (mut i32) (i32.const 11))

    ;; Memory data for string constant
    
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    

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
    ;; export from VB.NET module: [EnumTest]
    
    (export "EnumTest.Add1" (func $EnumTest.Add1))
    (export "EnumTest.DoAdd" (func $EnumTest.DoAdd))
    
     

    ;; functions in [EnumTest]
    
    (func $EnumTest.Add1 (param $i i32) (result i64)
        ;; Public Function Add1(i As i32) As i64
        
    (local $x i32)
    (local $a i64)
    
    (set_local $x (i32.add (get_local $i) (i32.const 1)))
    (set_local $a (i64.extend_s/i32 (get_local $x)))
    (return (get_local $a))
    )
    
    
    (func $EnumTest.DoAdd  (result i32)
        ;; Public Function DoAdd() As i32
        
    
    
    (return (i32.wrap/i64 (call $EnumTest.Add1 (i32.wrap/i64 (i64.add (i64.add (i64.const 3) (i64.const 4)) (i64.const 999))))))
    )
    
    
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $EnumTest.constructor )
)

(func $EnumTest.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)