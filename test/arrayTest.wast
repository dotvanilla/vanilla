(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 4/27/2019 8:36:00 PM

    ;; imports must occur before all non-import definitions

    ;; Declare Function debug Lib "console" Alias "log" (any As array(Of string)) As i32
    (func $debug (import "console" "log") (param $any i32) (result i32))
    ;; Declare Function print Lib "console" Alias "log" (any As string) As i32
    (func $print (import "console" "log") (param $any i32) (result i32))
    ;; Declare Function log Lib "console" Alias "log" (any As any) As void
    (func $log (import "console" "log") (param $any i32) )
    ;; Declare Function new_array Lib "Array" Alias "create" (size As i32) As array
    (func $new_array (import "Array" "create") (param $size i32) (result i32))
    ;; Declare Function array_push Lib "Array" Alias "push" (array As array, element As any) As array
    (func $array_push (import "Array" "push") (param $array i32) (param $element i32) (result i32))
    ;; Declare Function array_get Lib "Array" Alias "get" (array As array, index As i32) As any
    (func $array_get (import "Array" "get") (param $array i32) (param $index i32) (result i32))
    ;; Declare Function array.length Lib "Array" Alias "length" (array As array) As i32
    (func $array.length (import "Array" "length") (param $array i32) (result i32))
    ;; Declare Function i32.toString Lib "string" Alias "toString" (x As i32) As string
    (func $i32.toString (import "string" "toString") (param $x i32) (result i32))
    ;; Declare Function string_replace Lib "string" Alias "replace" (input As string, find As intptr, replacement As string) As string
    (func $string_replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
    ;; Declare Function string_add Lib "string" Alias "add" (a As string, b As string) As string
    (func $string_add (import "string" "add") (param $a i32) (param $b i32) (result i32))
    ;; Declare Function string_length Lib "string" Alias "length" (text As string) As i32
    (func $string_length (import "string" "length") (param $text i32) (result i32))
    ;; Declare Function string_indexOf Lib "string" Alias "indexOf" (input As string, find As string) As i32
    (func $string_indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
    ;; Declare Function array_set Lib "Array" Alias "set" (array As array, index As i32, value As any) As void
    (func $array_set (import "Array" "set") (param $array i32) (param $index i32) (param $value i32) )
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    ;; String from 1 with 5 bytes in memory
    (data (i32.const 1) "Hello\00")

    ;; String from 7 with 5 bytes in memory
    (data (i32.const 7) "World\00")

    ;; String from 13 with 3 bytes in memory
    (data (i32.const 13) "yes\00")

    ;; String from 17 with 6 bytes in memory
    (data (i32.const 17) "333333\00")

    ;; String from 24 with 5 bytes in memory
    (data (i32.const 24) "AAAAA\00")

    ;; String from 30 with 5 bytes in memory
    (data (i32.const 30) "XXXXX\00")

    ;; String from 36 with 6 bytes in memory
    (data (i32.const 36) "534535\00")

    ;; String from 43 with 13 bytes in memory
    (data (i32.const 43) "asdajkfsdhjkf\00")

    ;; String from 57 with 11 bytes in memory
    (data (i32.const 57) "Hello world\00")
    
    (global $arrayLength (mut i32) (i32.const 9999))

(global $xxl (mut i32) (i32.const 0))

(global $ints2 (mut i32) (i32.const 0))

    ;; export from [arrayTest]
    
    (export "arrayLoop" (func $arrayLoop))
    (export "testListAdd" (func $testListAdd))
    (export "arrayDeclares" (func $arrayDeclares))
    (export "createArray" (func $createArray))
    
     

    ;; functions in [arrayTest]
    
    (func $arrayLoop  (result i32)
        ;; Public Function arrayLoop() As i32
        (local $ints i32)
    (local $i i32)
    (set_local $ints (call $array_push (call $array_push (call $array_push (call $array_push (call $array_push (call $array_push (call $array_push (call $array_push (call $new_array (i32.const -1)) (i32.const 1)) (i32.const 2)) (i32.const 3)) (i32.const 4)) (i32.const 5)) (i32.const 6)) (i32.const 7)) (i32.const 88)))
    (set_global $ints2 (get_local $ints))
    (drop (call $print (call $i32.toString (call $array.length (get_global $ints2)))))
    (set_local $i (i32.const 0))
    ;; For i As Integer = 0 To ints.Length
    
    (block $block_9a020000 
        (loop $loop_9b020000
    
                    (br_if $block_9a020000 (i32.lt_s (get_local $i) (call $array.length (get_local $ints))))
            (drop (call $print (call $i32.toString (call $array_get (get_local $ints) (get_local $i)))))
            (set_local $i (i32.add (get_local $i) (i32.const 1)))
            (br $loop_9b020000)
            ;; For Loop Next On loop_9b020000
    
        )
    )
    (return (i32.const 0))
    )
    (func $testListAdd  (result i32)
        ;; Public Function testListAdd() As i32
        (local $l i32)
    (set_local $l (call $array_push (call $array_push (call $new_array (i32.const -1)) (i32.const 1)) (i32.const 7)))
    (set_global $xxl (get_local $l))
    (drop (call $array_push (get_local $l) (i32.const 13)))
    (drop (call $print (call $i32.toString (call $array_get (get_local $l) (i32.const 2)))))
    (call $log (get_local $l))
    (return (i32.const 0))
    )
    (func $arrayDeclares  
        ;; Public Function arrayDeclares() As void
        (local $syntax2 i32)
    (local $syntax3 i32)
    (local $len i32)
    (local $syntax1 i32)
    (set_local $syntax2 (call $array_push (call $array_push (call $array_push (call $array_push (call $new_array (i32.const -1)) (i32.const 23)) (i32.const 42)) (i32.const 42)) (i32.const 4)))
    (set_local $syntax3 (call $new_array (i32.sub (get_global $arrayLength) (i32.const 5))))
    (set_local $len (i32.const 999))
    (set_local $syntax1 (call $new_array (i32.sub (get_local $len) (i32.const 1))))
    (call $log (get_local $syntax2))
    (call $log (get_local $syntax3))
    (call $log (get_local $syntax1))
    )
    (func $createArray  (result i32)
        ;; Public Function createArray() As i32
        (local $str i32)
    (local $strAtFirst i32)
    (set_local $str (call $array_push (call $array_push (call $array_push (call $array_push (call $array_push (call $new_array (i32.const -1)) (i32.const 17)) (i32.const 24)) (i32.const 30)) (i32.const 36)) (i32.const 43)))
    (set_local $strAtFirst (call $array_get (get_local $str) (i32.const 0)))
    (drop (call $debug (get_local $str)))
    (drop (call $print (call $i32.toString (call $array_get (get_local $str) (i32.const 3)))))
    (call $array_set (get_local $str) (i32.const 4) (i32.const 57))
    (drop (call $debug (get_local $str)))
    (drop (call $print (call $i32.toString (call $array_get (get_local $str) (i32.const 4)))))
    (call $log (get_local $str))
    (return (i32.const 0))
    )
    )