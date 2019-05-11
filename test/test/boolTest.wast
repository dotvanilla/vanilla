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

    ;; Declare Function Random Lib "Math" Alias "random" () As f64
    (func $boolTest.Random (import "Math" "random")  (result f64))
    
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
    (global $boolTest.b2 (mut i32) (i32.const 1))
(global $boolTest.threshold (mut f32) (f32.const 0.5))

    ;; Export methods of this module
    ;; export from VB.NET module: [boolTest]
    
    (export "boolTest.logical" (func $boolTest.logical))
    
     

    ;; functions in [boolTest]
    
    (func $boolTest.logical  (result i32)
        ;; Public Function logical() As i32
        
    (local $b i32)
    
    (set_local $b (f64.ge (f64.promote/f32 (f32.demote/f64 (call $boolTest.Random ))) (f64.add (f64.promote/f32 (get_global $boolTest.threshold)) (f64.const 0.1))))
    
    (if (i32.mul (get_local $b) (get_global $boolTest.b2)) 
        (then
                    (return (i32.trunc_s/f64 (f64.const 1.5)))
        ) (else
                    (return (i32.sub (i32.const 0) (i32.const 100)))
        )
    )
    (return (i32.const 0))
    )
    
    
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $boolTest.constructor )
)

(func $boolTest.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)