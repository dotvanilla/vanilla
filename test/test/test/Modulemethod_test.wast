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

    ;; Declare Function string.replace Lib "string" Alias "replace" (input As string, find As intptr, replacement As string) As string
    (func $string.replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
;; Declare Function string.add Lib "string" Alias "add" (a As string, b As string) As string
    (func $string.add (import "string" "add") (param $a i32) (param $b i32) (result i32))
;; Declare Function string.length Lib "string" Alias "length" (text As string) As i32
    (func $string.length (import "string" "length") (param $text i32) (result i32))
;; Declare Function string.indexOf Lib "string" Alias "indexOf" (input As string, find As string) As i32
    (func $string.indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
;; Declare Function i32.toString Lib "string" Alias "toString" (x As i32) As string
    (func $i32.toString (import "string" "toString") (param $x i32) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 106))

    ;; Memory data for string constant
        
    ;; String from 11 with 11 bytes in memory
    (data (i32.const 11) "34546734853\00")
    
    ;; String from 23 with 16 bytes in memory
    (data (i32.const 23) "8sdjkfsdhfsdfsdf\00")
    
    ;; String from 40 with 27 bytes in memory
    (data (i32.const 40) "This is a internal function\00")
    
    ;; String from 68 with 31 bytes in memory
    (data (i32.const 68) "This is a internal function too\00")
    
    ;; String from 100 with 5 bytes in memory
    (data (i32.const 100) "ddddd\00")
    
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
(global $Modulemethod_test.auniqueSymbol (mut i32) (i32.const 0))
(global $Modulemethod_test.ANonUniqueSymbol (mut i32) (i32.const 0))
(global $module2.ANonUniqueSymbol (mut i32) (i32.const 0))

    ;; Export methods of this module
    ;; export from VB.NET module: [Modulemethod_test]
    
    (export "Modulemethod_test.arraytypeInferTest" (func $Modulemethod_test.arraytypeInferTest))
    (export "Modulemethod_test.test" (func $Modulemethod_test.test))
    (export "Modulemethod_test.calls" (func $Modulemethod_test.calls))
    
    
    ;; export from VB.NET module: [unqiueTest]
    
    (export "unqiueTest.test" (func $unqiueTest.test))
    
    
    ;; export from VB.NET module: [module2]
    
    (export "module2.Runapp" (func $module2.Runapp))
    (export "module2.test" (func $module2.test))
    
     

    ;; functions in [Modulemethod_test]
    
    (func $Modulemethod_test.arraytypeInferTest  (result i32)
        ;; Public Function arraytypeInferTest() As array(Of i64)
        
    (local $arrayOffset_9a020000 i32)
    
    
    ;; Save (i32.const 4) array element data to memory:
    ;; Array memory block begin at location: (get_global $global.ObjectManager)
    ;; class_id/typealias_enum i32 data: (i32.const 2)/array(Of i64)
    (i32.store (get_global $global.ObjectManager) (i32.const 2))
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 4))
    ;; End of byte marks meta data, start write data blocks
    ;; Offset object manager with (i32.add (i32.const 8) (i32.mul (i32.const 4) (i32.const 8))) bytes
    (set_local $arrayOffset_9a020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    (set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9a020000) (i32.const -8)) (i32.add (i32.const 8) (i32.mul (i32.const 4) (i32.const 8)))))
    (i64.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 0)) (i64.const 2342))
    (i64.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 8)) (i64.const 34))
    (i64.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 16)) (i64.const 322))
    (i64.store (i32.add (get_local $arrayOffset_9a020000) (i32.const 24)) (i64.const 343))
    ;; Assign array memory data to another expression
    (return (i32.add (get_local $arrayOffset_9a020000) (i32.const -8)))
    )
    
    
    (func $Modulemethod_test.test  (result i32)
        ;; Public Function test() As i32
        
    
    
    (return (i32.sub (i32.const 0) (i32.const 9999)))
    )
    
    
    (func $Modulemethod_test.calls  
        ;; Public Function calls() As void
        
    
    
    (drop (call $Modulemethod_test.test ))
    (drop (call $module2.test (call $string.add (call $string.add (i32.const 11) (call $i32.toString (call $Modulemethod_test.test ))) (i32.const 23))))
    (drop (call $Modulemethod_test.ThisIsAInternalFunction ))
    )
    
    
    (func $Modulemethod_test.ThisIsAInternalFunction  (result i32)
        ;; Public Function ThisIsAInternalFunction() As any
        
    
    
    (return (i32.const 40))
    )
    
    
    
    
    ;; functions in [unqiueTest]
    
    (func $unqiueTest.test  
        ;; Public Function test() As void
        
    (local $a i32)
    (local $b i32)
    (local $c i32)
    
    (set_local $a (get_global $Modulemethod_test.auniqueSymbol))
    (set_local $b (get_global $module2.ANonUniqueSymbol))
    (set_local $c (get_global $Modulemethod_test.ANonUniqueSymbol))
    )
    
    
    
    
    ;; functions in [module2]
    
    (func $module2.ThisIsAInternalFunction  (result i32)
        ;; Public Function ThisIsAInternalFunction() As any
        
    
    
    (return (i32.const 68))
    )
    
    
    (func $module2.Runapp  
        ;; Public Function Runapp() As void
        
    
    
    (call $Modulemethod_test.calls )
    (drop (call $module2.ThisIsAInternalFunction ))
    )
    
    
    (func $module2.returnANonUniqueSymbol  (result i32)
        ;; Public Function returnANonUniqueSymbol() As array(Of i32)
        
    (local $a i32)
    
    (set_local $a (get_global $Modulemethod_test.ANonUniqueSymbol))
    (return (get_global $module2.ANonUniqueSymbol))
    )
    
    
    (func $module2.test (param $gg i32) (result i32)
        ;; Public Function test(gg As string) As array(Of string)
        
    (local $arrayOffset_9b020000 i32)
    
    
    ;; Save (i32.const 2) array element data to memory:
    ;; Array memory block begin at location: (get_global $global.ObjectManager)
    ;; class_id/typealias_enum i32 data: (i32.const 5)/array(Of string)
    (i32.store (get_global $global.ObjectManager) (i32.const 5))
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 2))
    ;; End of byte marks meta data, start write data blocks
    ;; Offset object manager with (i32.add (i32.const 8) (i32.mul (i32.const 2) (i32.const 4))) bytes
    (set_local $arrayOffset_9b020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    (set_global $global.ObjectManager (i32.add (i32.add (get_local $arrayOffset_9b020000) (i32.const -8)) (i32.add (i32.const 8) (i32.mul (i32.const 2) (i32.const 4)))))
    (i32.store (i32.add (get_local $arrayOffset_9b020000) (i32.const 0)) (get_local $gg))
    (i32.store (i32.add (get_local $arrayOffset_9b020000) (i32.const 4)) (call $string.add (get_local $gg) (i32.const 100)))
    ;; Assign array memory data to another expression
    (return (i32.add (get_local $arrayOffset_9b020000) (i32.const -8)))
    )
    
    
    


;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    (call $Modulemethod_test.constructor )

(call $unqiueTest.constructor )

(call $module2.constructor )
)

(func $Modulemethod_test.constructor  
    ;; Public Function constructor() As void
    



)

(func $unqiueTest.constructor  
    ;; Public Function constructor() As void
    



)

(func $module2.constructor  
    ;; Public Function constructor() As void
    



)

(start $Application_SubNew)
)