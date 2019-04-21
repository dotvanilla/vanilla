(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 4/21/2019 8:59:18 PM

    ;; imports must occur before all non-import definitions

    ;; Declare Function debug Lib "console" Alias "log" (any As System.String[]) As i32
    (func $debug (import "console" "log") (param $any i32) (result i32))
    ;; Declare Function print Lib "console" Alias "log" (any As char*) As i32
    (func $print (import "console" "log") (param $any i32) (result i32))
    ;; Declare Function new_array Lib "Array" Alias "create" (size As i32) As i32
    (func $new_array (import "Array" "create") (param $size i32) (result i32))
    ;; Declare Function array_push Lib "Array" Alias "push" (array As i32, element As object) As i32
    (func $array_push (import "Array" "push") (param $array i32) (param $element i32) (result i32))
    ;; Declare Function array_get Lib "Array" Alias "get" (array As i32, index As i32) As i32
    (func $array_get (import "Array" "get") (param $array i32) (param $index i32) (result i32))
    ;; Declare Function array_length Lib "Array" Alias "length" (array As i32) As i32
    (func $array_length (import "Array" "length") (param $array i32) (result i32))
    ;; Declare Function string_replace Lib "string" Alias "replace" (input As char*, find As i32, replacement As char*) As i32
    (func $string_replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
    ;; Declare Function string_add Lib "string" Alias "add" (a As char*, b As char*) As char*
    (func $string_add (import "string" "add") (param $a i32) (param $b i32) (result i32))
    ;; Declare Function string_length Lib "string" Alias "length" (text As char*) As i32
    (func $string_length (import "string" "length") (param $text i32) (result i32))
    ;; Declare Function string_indexOf Lib "string" Alias "indexOf" (input As char*, find As char*) As i32
    (func $string_indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
    ;; Declare Function array_set Lib "Array" Alias "set" (array As i32, index As i32, value As i32) As i32
    (func $array_set (import "Array" "set") (param $array i32) (param $index i32) (param $value i32) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    ;; String from 1 with 3 bytes in memory
    (data (i32.const 1) "yes\00")

    ;; String from 5 with 6 bytes in memory
    (data (i32.const 5) "333333\00")

    ;; String from 12 with 5 bytes in memory
    (data (i32.const 12) "AAAAA\00")

    ;; String from 18 with 5 bytes in memory
    (data (i32.const 18) "XXXXX\00")

    ;; String from 24 with 6 bytes in memory
    (data (i32.const 24) "534535\00")

    ;; String from 31 with 13 bytes in memory
    (data (i32.const 31) "asdajkfsdhjkf\00")

    ;; String from 45 with 11 bytes in memory
    (data (i32.const 45) "Hello world\00")
    
    (global $arrayLength (mut i32) (i32.const 9999))

    ;; export from [arrayTest]
    
    (export "arrayDeclares" (func $arrayDeclares))
    (export "testListAdd" (func $testListAdd))
    (export "arrayLoop" (func $arrayLoop))
    (export "createArray" (func $createArray))
    
     

    ;; functions in [arrayTest]
    
    (func $arrayDeclares  
        ;; Public Function arrayDeclares() As void
        (local $syntax3 i32)
    (local $len i32)
    (local $syntax1 i32)
    (local $syntax2 i32)
    (set_local $syntax3 (call $new_array (i32.sub (get_global $arrayLength) (i32.const 5))))
    (set_local $len (i32.const 999))
    (set_local $syntax1 (call $new_array (i32.sub (get_local $len) (i32.const 1))))
    (set_local $syntax2 (call $array_push (call $array_push (call $array_push (call $array_push (call $new_array (i32.const -1)) (i32.const 23)) (i32.const 42)) (i32.const 42)) (i32.const 4)))
    )
    (func $testListAdd  (result i32)
        ;; Public Function testListAdd() As i32
        (local $l i32)
    (set_local $l (call $new_array (i32.const -1)))
    (drop (call $array_push (get_local $l) (i32.const 1)))
    (drop (call $print (call $array_get (get_local $l) (i32.const 2))))
    (return (i32.const 0))
    )
    (func $arrayLoop  (result i32)
        ;; Public Function arrayLoop() As i32
        (local $ints i32)
    (local $i i32)
    (set_local $ints (call $array_push (call $array_push (call $array_push (call $array_push (call $array_push (call $array_push (call $array_push (call $array_push (call $new_array (i32.const -1)) (i32.const 1)) (i32.const 2)) (i32.const 3)) (i32.const 4)) (i32.const 5)) (i32.const 6)) (i32.const 7)) (i32.const 88)))
    (set_local $i (i32.const 0))
    ;; For i As Integer = 0 To ints.Length
    
    (block $block_9a020000 
        (loop $loop_9b020000
    
                    (br_if $block_9a020000 (i32.gt_s (get_local $i) (call $array_length (get_local $ints))))
            (drop (call $print (call $array_get (get_local $ints) (get_local $i))))
            (set_local $i (i32.add (get_local $i) (i32.const 1)))
            (br $loop_9b020000)
            ;; For Loop Next On loop_9b020000
    
        )
    )
    (return (i32.const 0))
    )
    (func $createArray  (result i32)
        ;; Public Function createArray() As i32
        (local $str i32)
    (local $strAtFirst i32)
    (set_local $str (call $array_push (call $array_push (call $array_push (call $array_push (call $array_push (call $new_array (i32.const -1)) (i32.const 5)) (i32.const 12)) (i32.const 18)) (i32.const 24)) (i32.const 31)))
    (set_local $strAtFirst (call $array_get (get_local $str) (i32.const 0)))
    (drop (call $debug (get_local $str)))
    (drop (call $print (call $array_get (get_local $str) (i32.const 3))))
    (drop (call $array_set (get_local $str) (i32.const 4) (i32.const 45)))
    (drop (call $debug (get_local $str)))
    (drop (call $print (call $array_get (get_local $str) (i32.const 4))))
    (return (i32.const 0))
    )
    )