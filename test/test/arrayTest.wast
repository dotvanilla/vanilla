(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/4/2019 2:17:00 AM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function debug Lib "console" Alias "log" (any As array(Of string)) As i32
    (func $arrayTest.debug (import "console" "log") (param $any i32) (result i32))
    ;; Declare Function print Lib "console" Alias "log" (any As string) As i32
    (func $arrayTest.print (import "console" "log") (param $any i32) (result i32))
    ;; Declare Function log Lib "console" Alias "log" (any As any) As void
    (func $arrayTest.log (import "console" "log") (param $any i32) )
    ;; Declare Function array.new Lib "Array" Alias "create" (size As i32) As list
    (func $array.new (import "Array" "create") (param $size i32) (result i32))
    ;; Declare Function i32_array.push Lib "Array" Alias "push" (array As list, element As i32) As list
    (func $i32_array.push (import "Array" "push") (param $array i32) (param $element i32) (result i32))
    ;; Declare Function i32_array.get Lib "Array" Alias "get" (array As list, index As i32) As i32
    (func $i32_array.get (import "Array" "get") (param $array i32) (param $index i32) (result i32))
    ;; Declare Function i32_array.set Lib "Array" Alias "set" (array As list, index As i32, value As i32) As void
    (func $i32_array.set (import "Array" "set") (param $array i32) (param $index i32) (param $value i32) )
    ;; Declare Function array.length Lib "Array" Alias "length" (array As list) As i32
    (func $array.length (import "Array" "length") (param $array i32) (result i32))
    ;; Declare Function array_array.push Lib "Array" Alias "push" (array As list, element As array(Of i32)) As list
    (func $array_array.push (import "Array" "push") (param $array i32) (param $element i32) (result i32))
    ;; Declare Function array_array.get Lib "Array" Alias "get" (array As list, index As i32) As array(Of i32)
    (func $array_array.get (import "Array" "get") (param $array i32) (param $index i32) (result i32))
    ;; Declare Function array_array.set Lib "Array" Alias "set" (array As list, index As i32, value As array(Of i32)) As void
    (func $array_array.set (import "Array" "set") (param $array i32) (param $index i32) (param $value i32) )
    ;; Declare Function i32.toString Lib "string" Alias "toString" (x As i32) As string
    (func $i32.toString (import "string" "toString") (param $x i32) (result i32))
    ;; Declare Function string.replace Lib "string" Alias "replace" (input As string, find As intptr, replacement As string) As string
    (func $string.replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
    ;; Declare Function string.add Lib "string" Alias "add" (a As string, b As string) As string
    (func $string.add (import "string" "add") (param $a i32) (param $b i32) (result i32))
    ;; Declare Function string.length Lib "string" Alias "length" (text As string) As i32
    (func $string.length (import "string" "length") (param $text i32) (result i32))
    ;; Declare Function string.indexOf Lib "string" Alias "indexOf" (input As string, find As string) As i32
    (func $string.indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
    ;; Declare Function string_array.push Lib "Array" Alias "push" (array As list, element As string) As list
    (func $string_array.push (import "Array" "push") (param $array i32) (param $element i32) (result i32))
    ;; Declare Function string_array.get Lib "Array" Alias "get" (array As list, index As i32) As string
    (func $string_array.get (import "Array" "get") (param $array i32) (param $index i32) (result i32))
    ;; Declare Function string_array.set Lib "Array" Alias "set" (array As list, index As i32, value As string) As void
    (func $string_array.set (import "Array" "set") (param $array i32) (param $index i32) (param $value i32) )
    ;; Declare Function list_array.push Lib "Array" Alias "push" (array As list, element As list(Of string)) As list
    (func $list_array.push (import "Array" "push") (param $array i32) (param $element i32) (result i32))
    ;; Declare Function list_array.get Lib "Array" Alias "get" (array As list, index As i32) As list(Of string)
    (func $list_array.get (import "Array" "get") (param $array i32) (param $index i32) (result i32))
    ;; Declare Function list_array.set Lib "Array" Alias "set" (array As list, index As i32, value As list(Of string)) As void
    (func $list_array.set (import "Array" "set") (param $array i32) (param $index i32) (param $value i32) )
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 69))

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
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    

    ;; Global variables in this module
    (global $arrayTest.arrayLength (mut i32) (i32.const 9999))

(global $arrayTest.xxl (mut i32) (i32.const 0))

(global $arrayTest.ints2 (mut i32) (i32.const 0))

    ;; Export methods of this module
    ;; export from VB.NET module: [arrayTest]
    
    (export "arrayTest.arrayLoop" (func $arrayTest.arrayLoop))
    (export "arrayTest.testListAdd" (func $arrayTest.testListAdd))
    (export "arrayTest.arrayDeclares" (func $arrayTest.arrayDeclares))
    (export "arrayTest.createArray" (func $arrayTest.createArray))
    
     

    ;; functions in [arrayTest]
    
    (func $arrayTest.arrayLoop  (result i32)
        ;; Public Function arrayLoop() As i32
        (local $ints i32)
    (local $i i32)
    (set_local $ints (call $i32_array.push (call $i32_array.push (call $i32_array.push (call $i32_array.push (call $i32_array.push (call $i32_array.push (call $i32_array.push (call $i32_array.push (call $array.new (i32.const -1)) (i32.const 1)) (i32.const 2)) (i32.const 3)) (i32.const 4)) (i32.const 5)) (i32.const 6)) (i32.const 7)) (i32.const 88)))
    (set_global $arrayTest.ints2 (get_local $ints))
    (drop (call $arrayTest.print (call $i32.toString (i32.load (i32.add (get_global $arrayTest.ints2) (i32.const 4))))))
    (set_local $i (i32.const 0))
    ;; For i As Integer = 0 To ints.Length
    
    (block $block_9a020000 
        (loop $loop_9b020000
    
                    (br_if $block_9a020000 (i32.gt_s (get_local $i) (i32.load (i32.add (get_local $ints) (i32.const 4)))))
            (drop (call $arrayTest.print (call $i32.toString (i32.load (i32.add (i32.add (get_local $ints) (i32.const 8)) (i32.mul (get_local $i) (i32.const 4)))))))
            ;; For loop control step: (i32.const 1)
            (set_local $i (i32.add (get_local $i) (i32.const 1)))
            (br $loop_9b020000)
            ;; For Loop Next On loop_9b020000
    
        )
    )
    (return (i32.const 0))
    )
    (func $arrayTest.testListAdd  (result i32)
        ;; Public Function testListAdd() As i32
        (local $l i32)
    (set_local $l (call $string_array.push (call $string_array.push (call $array.new (i32.const -1)) (i32.const 1)) (i32.const 7)))
    (set_global $arrayTest.xxl (get_local $l))
    (drop (call $string_array.push (get_local $l) (i32.const 13)))
    (drop (call $arrayTest.print (call $string_array.get (get_local $l) (i32.const 2))))
    (call $arrayTest.log (get_local $l))
    (return (i32.const 0))
    )
    (func $arrayTest.arrayDeclares  
        ;; Public Function arrayDeclares() As void
        (local $arrayOffset_9c020000 i32)
    (local $syntax2 i32)
    (local $syntax3 i32)
    (local $len i32)
    (local $syntax1 i32)
    
    ;; Save 4 array element data to memory:
    ;; Array memory block begin at location: (get_global $global.ObjectManager)
    ;; class_id/typealias_enum i32 data: (i32.const 4)/array(Of f64)
    (i32.store (get_global $global.ObjectManager) (i32.const 4))
    (i32.store (i32.add (get_global $global.ObjectManager) (i32.const 4)) (i32.const 4))
    ;; End of byte marks meta data, start write data blocks
    (set_local $arrayOffset_9c020000 (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    (f64.store (i32.add (get_local $arrayOffset_9c020000) (i32.const 0)) (f64.convert_s/i32 (i32.const 23)))
    (f64.store (i32.add (get_local $arrayOffset_9c020000) (i32.const 8)) (f64.convert_s/i32 (i32.const 42)))
    (f64.store (i32.add (get_local $arrayOffset_9c020000) (i32.const 16)) (f64.convert_s/i32 (i32.const 42)))
    (f64.store (i32.add (get_local $arrayOffset_9c020000) (i32.const 24)) (f64.convert_s/i32 (i32.const 4)))
    ;; Offset object manager with 24 bytes
    (set_global $global.ObjectManager (i32.add (get_global $global.ObjectManager) (i32.const 24)))
    ;; Assign array memory data to another expression
    (set_local $syntax2 (get_global $global.ObjectManager))
    (set_local $syntax3 (call $array.new (i32.sub (get_global $arrayTest.arrayLength) (i32.const 5))))
    (set_local $len (i32.const 999))
    (set_local $syntax1 (call $array.new (i32.sub (get_local $len) (i32.const 1))))
    (call $arrayTest.log (get_local $syntax2))
    (call $arrayTest.log (get_local $syntax3))
    (call $arrayTest.log (get_local $syntax1))
    )
    (func $arrayTest.createArray  (result i32)
        ;; Public Function createArray() As i32
        (local $str i32)
    (local $strAtFirst i32)
    (set_local $str (call $string_array.push (call $string_array.push (call $string_array.push (call $string_array.push (call $string_array.push (call $array.new (i32.const -1)) (i32.const 17)) (i32.const 24)) (i32.const 30)) (i32.const 36)) (i32.const 43)))
    (set_local $strAtFirst (i32.load (i32.add (i32.add (get_local $str) (i32.const 8)) (i32.mul (i32.const 0) (i32.const 4)))))
    (drop (call $arrayTest.debug (get_local $str)))
    (drop (call $arrayTest.print (call $i32.toString (i32.load (i32.add (i32.add (get_local $str) (i32.const 8)) (i32.mul (i32.const 3) (i32.const 4)))))))
    (i32.store (i32.add (i32.add (get_local $str) (i32.const 8)) (i32.mul (i32.const 4) (i32.const 4))) (i32.const 57))
    (drop (call $arrayTest.debug (get_local $str)))
    (drop (call $arrayTest.print (call $i32.toString (i32.load (i32.add (i32.add (get_local $str) (i32.const 8)) (i32.mul (i32.const 4) (i32.const 4)))))))
    (call $arrayTest.log (get_local $str))
    (return (i32.const 0))
    )
    

;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew


)

(start $Application_SubNew)

)