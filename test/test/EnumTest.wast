(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/11/2019 10:25:54 AM
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