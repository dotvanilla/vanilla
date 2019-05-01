(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/1/2019 12:18:01 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function DOMbyId Lib "document" Alias "getElementById" (id As string) As i32
    (func $nullreferenceTest.DOMbyId (import "document" "getElementById") (param $id i32) (result i32))
    ;; Declare Function setAttr Lib "document" Alias "setAttribute" (node As i32, name As string, value As string) As i32
    (func $nullreferenceTest.setAttr (import "document" "setAttribute") (param $node i32) (param $name i32) (param $value i32) (result i32))
    ;; Declare Function print Lib "console" Alias "log" (message As string) As i32
    (func $nullreferenceTest.print (import "console" "log") (param $message i32) (result i32))
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

    ;; Memory data for string constant
    
    ;; String from 1 with 9 bytes in memory
    (data (i32.const 1) "Nothing 1\00")

    ;; String from 11 with 49 bytes in memory
    (data (i32.const 11) "This message should never display on the console!\00")

    ;; String from 61 with 9 bytes in memory
    (data (i32.const 61) "Nothing 2\00")

    ;; String from 71 with 57 bytes in memory
    (data (i32.const 71) "This message is also should never display on the console!\00")

    ;; String from 129 with 7 bytes in memory
    (data (i32.const 129) "Nothing\00")

    ;; String from 137 with 4 bytes in memory
    (data (i32.const 137) "test\00")

    ;; String from 142 with 1 bytes in memory
    (data (i32.const 142) "a\00")

    ;; String from 144 with 15 bytes in memory
    (data (i32.const 144) " is not nothing\00")

    ;; String from 160 with 1 bytes in memory
    (data (i32.const 160) "a\00")

    ;; String from 162 with 1 bytes in memory
    (data (i32.const 162) "b\00")
    
    (global $nullreferenceTest.nullString (mut i32) (i32.const 0))

    ;; export from [nullreferenceTest]
    
    (export "nullreferenceTest.isNothing" (func $nullreferenceTest.isNothing))
    (export "nullreferenceTest.noReturns" (func $nullreferenceTest.noReturns))
    (export "nullreferenceTest.test" (func $nullreferenceTest.test))
    
     

    ;; functions in [nullreferenceTest]
    
    (func $nullreferenceTest.isNothing  
        ;; Public Function isNothing() As void
        
    
    (if (i32.eq (get_global $nullreferenceTest.nullString) (i32.const 0)) 
        (then
                    (drop (call $nullreferenceTest.print (i32.const 1)))
        ) (else
                    (drop (call $nullreferenceTest.print (i32.const 11)))
            (drop (call $nullreferenceTest.print (get_global $nullreferenceTest.nullString)))
        )
    )
    
    (if (i32.eq (i32.const 0) (get_global $nullreferenceTest.nullString)) 
        (then
                    (drop (call $nullreferenceTest.print (i32.const 61)))
        ) (else
                    (drop (call $nullreferenceTest.print (i32.const 71)))
            (drop (call $nullreferenceTest.print (get_global $nullreferenceTest.nullString)))
        )
    )
    )
    (func $nullreferenceTest.noReturns  
        ;; Public Function noReturns() As void
        
    (drop (call $nullreferenceTest.print (i32.const 129)))
    (drop (call $nullreferenceTest.print (i32.const 0)))
    )
    (func $nullreferenceTest.test  (result i32)
        ;; Public Function test() As i32
        (local $node i32)
    (set_local $node (call $nullreferenceTest.DOMbyId (i32.const 137)))
    (drop (call $nullreferenceTest.setAttr (get_local $node) (i32.const 142) (call $string.add (call $i32.toString (get_local $node)) (i32.const 144))))
    (drop (call $nullreferenceTest.setAttr (i32.const 0) (i32.const 160) (i32.const 162)))
    (return (i32.const 0))
    )
    )