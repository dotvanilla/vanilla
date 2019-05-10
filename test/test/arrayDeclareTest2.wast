(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/11/2019 12:24:59 AM
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
    (global $arrayDeclareTest2.len (mut i32) (i32.const 100))

    ;; Export methods of this module
    ;; export from VB.NET module: [arrayDeclareTest2]
    
    (export "arrayDeclareTest2.Main" (func $arrayDeclareTest2.Main))
    
     

    ;; functions in [arrayDeclareTest2]
    
    (func $arrayDeclareTest2.Main  
        ;; Public Function Main() As void
        
    (local $arrayOffset_9a020000 i32)
    (local $a i32)
    (local $arrayOffset_9b020000 i32)
    (local $b i32)
    (local $arrayOffset_9c020000 i32)
    (local $arrayOffset_9d020000 i32)
    (local $c i32)
    
    
    ;; Save (i32.sub (get_global $arrayDeclareTest2.len) (i32.const 1)) array element data to memory:
    ;; Array memory block begin at location: (get_global $global.ObjectManager)
    ;; class_id/typealias_enum i32 data: (i32.const 4)/array(Of f64)
    (i32.store (get_global $global.ObjectManager) (i32.const 4))
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.sub (get_global $arrayDeclareTest2.len) (i32.const 1)))
    ;; End of byte marks meta data, start write data blocks
    ;; Offset object manager with (i32.add (i32.const 8) (i32.mul (i32.sub (get_global $arrayDeclareTest2.len) (i32.const 1)) (i32.const 8))) bytes
    (set_local $arrayOffset_9a020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    (set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9a020000) (i32.const -8)) (i32.add (i32.const 8) (i32.mul (i32.sub (get_global $arrayDeclareTest2.len) (i32.const 1)) (i32.const 8)))))
    ;; Assign array memory data to another expression
    (set_local $a (i32.add (get_local $arrayOffset_9a020000) (i32.const -8)))
    
    ;; Save (i32.sub (get_global $arrayDeclareTest2.len) (i32.const 1)) array element data to memory:
    ;; Array memory block begin at location: (get_global $global.ObjectManager)
    ;; class_id/typealias_enum i32 data: (i32.const 4)/array(Of f64)
    (i32.store (get_global $global.ObjectManager) (i32.const 4))
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.sub (get_global $arrayDeclareTest2.len) (i32.const 1)))
    ;; End of byte marks meta data, start write data blocks
    ;; Offset object manager with (i32.add (i32.const 8) (i32.mul (i32.sub (get_global $arrayDeclareTest2.len) (i32.const 1)) (i32.const 8))) bytes
    (set_local $arrayOffset_9b020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    (set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9b020000) (i32.const -8)) (i32.add (i32.const 8) (i32.mul (i32.sub (get_global $arrayDeclareTest2.len) (i32.const 1)) (i32.const 8)))))
    ;; Assign array memory data to another expression
    (set_local $b (i32.add (get_local $arrayOffset_9b020000) (i32.const -8)))
    
    ;; Save (i32.const 99) array element data to memory:
    ;; Array memory block begin at location: (get_global $global.ObjectManager)
    ;; class_id/typealias_enum i32 data: (i32.const 4)/array(Of f64)
    (i32.store (get_global $global.ObjectManager) (i32.const 4))
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 99))
    ;; End of byte marks meta data, start write data blocks
    ;; Offset object manager with (i32.add (i32.const 8) (i32.mul (i32.const 99) (i32.const 8))) bytes
    (set_local $arrayOffset_9c020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    (set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9c020000) (i32.const -8)) (i32.add (i32.const 8) (i32.mul (i32.const 99) (i32.const 8)))))
    ;; Assign array memory data to another expression
    (set_local $a (i32.add (get_local $arrayOffset_9c020000) (i32.const -8)))
    
    ;; Save (i32.const 8) array element data to memory:
    ;; Array memory block begin at location: (get_global $global.ObjectManager)
    ;; class_id/typealias_enum i32 data: (i32.const 1)/array(Of i32)
    (i32.store (get_global $global.ObjectManager) (i32.const 1))
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 8))
    ;; End of byte marks meta data, start write data blocks
    ;; Offset object manager with (i32.add (i32.const 8) (i32.mul (i32.const 8) (i32.const 4))) bytes
    (set_local $arrayOffset_9d020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    (set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9d020000) (i32.const -8)) (i32.add (i32.const 8) (i32.mul (i32.const 8) (i32.const 4)))))
    (i32.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 0)) (i32.const 1))
    (i32.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 4)) (i32.const 11))
    (i32.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 8)) (i32.const 11))
    (i32.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 12)) (i32.const 11))
    (i32.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 16)) (i32.const 11))
    (i32.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 20)) (i32.const 11))
    (i32.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 24)) (i32.const 11))
    (i32.store (i32.add (get_local $arrayOffset_9d020000) (i32.const 28)) (i32.const 11))
    ;; Assign array memory data to another expression
    (set_local $c (i32.add (get_local $arrayOffset_9d020000) (i32.const -8)))
    )
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $arrayDeclareTest2.constructor )
)

(func $arrayDeclareTest2.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)