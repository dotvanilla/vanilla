(module ;; Module HelloWorld

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.78.7513.15987
    ;; build: 7/27/2020 8:52:55 AM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    ;; Declare Function Math.pow Lib "Math" Alias "pow" (a As f64, b As f64) As f64
    (func $Math.pow (import "Math" "pow") (param $a f64) (param $b f64) (result f64))
;; Declare Function Math.sqrt Lib "Math" Alias "sqrt" (a As f64) As f64
    (func $Math.sqrt (import "Math" "sqrt") (param $a f64) (result f64))
;; Declare Function Math.exp Lib "Math" Alias "exp" (x As f64) As f64
    (func $Math.exp (import "Math" "exp") (param $x f64) (result f64))
;; Declare Function Math.cos Lib "Math" Alias "cos" (x As f64) As f64
    (func $Math.cos (import "Math" "cos") (param $x f64) (result f64))
;; Declare Function Math.random Lib "Math" Alias "random" () As f64
    (func $Math.random (import "Math" "random")  (result f64))
;; Declare Function Math.ceil Lib "Math" Alias "ceil" (x As f64) As f64
    (func $Math.ceil (import "Math" "ceil") (param $x f64) (result f64))
;; Declare Function Math.floor Lib "Math" Alias "floor" (x As f64) As f64
    (func $Math.floor (import "Math" "floor") (param $x f64) (result f64))
;; Declare Function GC.addObject Lib "GC" Alias "addObject" (offset As i32, class_id As i32) As void
    (func $GC.addObject (import "GC" "addObject") (param $offset i32) (param $class_id i32) )
;; Declare Function string.replace Lib "string" Alias "replace" (input As string, find As intptr, replacement As string) As string
    (func $string.replace (import "string" "replace") (param $input i32) (param $find i32) (param $replacement i32) (result i32))
;; Declare Function string.add Lib "string" Alias "add" (a As string, b As string) As string
    (func $string.add (import "string" "add") (param $a i32) (param $b i32) (result i32))
;; Declare Function string.length Lib "string" Alias "length" (text As string) As i32
    (func $string.length (import "string" "length") (param $text i32) (result i32))
;; Declare Function string.indexOf Lib "string" Alias "indexOf" (input As string, find As string) As i32
    (func $string.indexOf (import "string" "indexOf") (param $input i32) (param $find i32) (result i32))
;; Declare Function log Lib "console" Alias "log" (message As string) As i32
    (func $console.log (import "console" "log") (param $message i32) (result i32))
;; Declare Function warn Lib "console" Alias "warn" (message As string) As i32
    (func $console.warn (import "console" "warn") (param $message i32) (result i32))
;; Declare Function info Lib "console" Alias "info" (message As string) As i32
    (func $console.info (import "console" "info") (param $message i32) (result i32))
;; Declare Function error Lib "console" Alias "error" (message As string) As i32
    (func $console.error (import "console" "error") (param $message i32) (result i32))
;; Declare Function DOMById Lib "document" Alias "getElementById" (id As string) As i32
    (func $document.DOMById (import "document" "getElementById") (param $id i32) (result i32))
;; Declare Function setText Lib "document" Alias "writeElementText" (node As i32, text As string) As i32
    (func $document.setText (import "document" "writeElementText") (param $node i32) (param $text i32) (result i32))
;; Declare Function createElement Lib "document" Alias "createElement" (tagName As string) As i32
    (func $document.createElement (import "document" "createElement") (param $tagName i32) (result i32))
;; Declare Function setAttribute Lib "document" Alias "setAttribute" (node As i32, attr As string, value As string) As i32
    (func $document.setAttribute (import "document" "setAttribute") (param $node i32) (param $attr i32) (param $value i32) (result i32))
;; Declare Function appendChild Lib "document" Alias "appendChild" (parent As i32, node As i32) As i32
    (func $document.appendChild (import "document" "appendChild") (param $parent i32) (param $node i32) (result i32))
;; Declare Function Exp Lib "Math" Alias "exp" (x As f64) As f64
    (func $Math.Exp (import "Math" "exp") (param $x f64) (result f64))
;; Declare Function array.new Lib "Array" Alias "create" (size As i32) As list
    (func $array.new (import "Array" "create") (param $size i32) (result i32))
;; Declare Function list_array.push Lib "Array" Alias "push" (array As list, element As list(Of f64)) As list
    (func $list_array.push (import "Array" "push") (param $array i32) (param $element i32) (result i32))
;; Declare Function list_array.get Lib "Array" Alias "get" (array As list, index As i32) As list(Of f64)
    (func $list_array.get (import "Array" "get") (param $array i32) (param $index i32) (result i32))
;; Declare Function list_array.set Lib "Array" Alias "set" (array As list, index As i32, value As list(Of f64)) As void
    (func $list_array.set (import "Array" "set") (param $array i32) (param $index i32) (param $value i32) )
;; Declare Function array.length Lib "Array" Alias "length" (array As list) As i32
    (func $array.length (import "Array" "length") (param $array i32) (result i32))
;; Declare Function i32.toString Lib "string" Alias "toString" (x As i32) As string
    (func $i32.toString (import "string" "toString") (param $x i32) (result i32))
;; Declare Function f64.toString Lib "string" Alias "toString" (x As f64) As string
    (func $f64.toString (import "string" "toString") (param $x f64) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 856))

    ;; memory allocate in javascript runtime
    (func $global.ObjectManager.Allocate (param $sizeof i32) (param $class_id i32) (result i32)
    ;; Public Function ObjectManager.Allocate(sizeof As i32, class_id As i32) As i32
    
(local $offset i32)
(local $padding i32)

(set_local $offset (get_global $global.ObjectManager))
(set_global $global.ObjectManager (i32.add (get_local $offset) (get_local $sizeof)))
(set_local $padding (i32.rem_s (get_global $global.ObjectManager) (i32.const 8)))

(if (get_local $padding) 
    (then
                (set_local $padding (i32.sub (i32.const 8) (get_local $padding)))
        (set_global $global.ObjectManager (i32.add (get_global $global.ObjectManager) (get_local $padding)))
    ) (else
                (set_global $global.ObjectManager (i32.add (get_global $global.ObjectManager) (i32.const 8)))
    )
)
(call $GC.addObject (get_local $offset) (get_local $class_id))
(return (get_local $offset))
)
    (func $global.GetMemorySize  (result i32)
    ;; Public Function GetMemorySize() As i32
    


(return (get_global $global.ObjectManager))
)

    ;; Memory data for string constant
        
    ;; String from 13 with 12 bytes in memory
    (data (i32.const 13) "Hello World!\00")
    
    ;; String from 32 with 54 bytes in memory
    (data (i32.const 32) "This message comes from a VisualBasic.NET application!\00")
    
    ;; String from 88 with 21 bytes in memory
    (data (i32.const 88) "WebAssembly it works!\00")
    
    ;; String from 112 with 4 bytes in memory
    (data (i32.const 112) "text\00")
    
    ;; String from 120 with 5 bytes in memory
    (data (i32.const 120) "notes\00")
    
    ;; String from 128 with 1 bytes in memory
    (data (i32.const 128) "p\00")
    
    ;; String from 136 with 1 bytes in memory
    (data (i32.const 136) "p\00")
    
    ;; String from 144 with 5 bytes in memory
    (data (i32.const 144) "style\00")
    
    ;; String from 152 with 28 bytes in memory
    (data (i32.const 152) "background-color: lightgrey;\00")
    
    ;; String from 184 with 5 bytes in memory
    (data (i32.const 184) "style\00")
    
    ;; String from 192 with 27 bytes in memory
    (data (i32.const 192) "font-size: 2em; color: red;\00")
    
    ;; String from 224 with 5 bytes in memory
    (data (i32.const 224) "style\00")
    
    ;; String from 232 with 29 bytes in memory
    (data (i32.const 232) "font-size: 5em; color: green;\00")
    
    ;; String from 264 with 33 bytes in memory
    (data (i32.const 264) "Debug text message display below:\00")
    
    ;; String from 304 with 56 bytes in memory
    (data (i32.const 304) "Try to display an error message on javascript console...\00")
    
    ;; String from 368 with 15 bytes in memory
    (data (i32.const 368) "sfghnsmfhsdjkfh\00")
    
    ;; String from 384 with 25 bytes in memory
    (data (i32.const 384) "sdjkfhsdjkfhsdjkfhsdjkfhs\00")
    
    ;; String from 416 with 18 bytes in memory
    (data (i32.const 416) "djkfhsdjkfsdfsdfsd\00")
    
    ;; String from 440 with 6 bytes in memory
    (data (i32.const 440) "result\00")
    
    ;; String from 448 with 6 bytes in memory
    (data (i32.const 448) "result\00")
    
    ;; String from 456 with 37 bytes in memory
    (data (i32.const 456) "The calculation result of PoissonPDF(\00")
    
    ;; String from 496 with 2 bytes in memory
    (data (i32.const 496) ", \00")
    
    ;; String from 504 with 5 bytes in memory
    (data (i32.const 504) ") is \00")
    
    ;; String from 512 with 1 bytes in memory
    (data (i32.const 512) "!\00")
    
    ;; String from 520 with 6 bytes in memory
    (data (i32.const 520) "result\00")
    
    ;; String from 528 with 5 bytes in memory
    (data (i32.const 528) "style\00")
    
    ;; String from 536 with 24 bytes in memory
    (data (i32.const 536) "color: blue; font-size: \00")
    
    ;; String from 568 with 20 bytes in memory
    (data (i32.const 568) "; background-color: \00")
    
    ;; String from 592 with 1 bytes in memory
    (data (i32.const 592) ";\00")
    
    ;; String from 600 with 11 bytes in memory
    (data (i32.const 600) "HelloWorld!\00")
    
    ;; String from 616 with 80 bytes in memory
    (data (i32.const 616) "A hello world demo project for VisualBasic.NET WebAssembly compiler and runtime.\00")
    
    ;; String from 704 with 10 bytes in memory
    (data (i32.const 704) "xieguigang\00")
    
    ;; String from 720 with 10 bytes in memory
    (data (i32.const 720) "HelloWorld\00")
    
    ;; String from 736 with 20 bytes in memory
    (data (i32.const 736) "Copyright © MIT 2019\00")
    
    ;; String from 760 with 11 bytes in memory
    (data (i32.const 760) "HelloWorld!\00")
    
    ;; String from 776 with 36 bytes in memory
    (data (i32.const 776) "8750377f-b6e7-4fb5-886b-4c3fa451ec4c\00")
    
    ;; String from 816 with 13 bytes in memory
    (data (i32.const 816) "123.34.0.5466\00")
    
    ;; String from 832 with 9 bytes in memory
    (data (i32.const 832) "1.0.99.78\00")
    
    ;; String from 848 with 0 bytes in memory
    (data (i32.const 848) "\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    

    ;; Pre-defined constant values
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

    ;; Global variables in this module
    (global $App.helloWorld (mut i32) (i32.const 13))
(global $App.note (mut i32) (i32.const 32))
(global $App.note2 (mut i32) (i32.const 88))
(global $array.stringArray (mut i32) (i32.const 0))

    ;; Export methods of this module
    (export "global.GetMemorySize" (func $global.GetMemorySize))

    ;; export from VB.NET module: [App]
    
    (export "App.RunApp" (func $App.RunApp))
    
    
    ;; export from VB.NET module: [array]
    
    (export "array.listArray" (func $array.listArray))
    
    
    ;; export from VB.NET module: [Math]
    
    (export "Math.PoissonPDF" (func $Math.PoissonPDF))
    (export "Math.DisplayResult" (func $Math.DisplayResult))
    
    
    ;; export from VB.NET module: [AssemblyInfo]
    
    (export "AssemblyInfo.AssemblyTitle" (func $AssemblyInfo.AssemblyTitle))
    (export "AssemblyInfo.AssemblyDescription" (func $AssemblyInfo.AssemblyDescription))
    (export "AssemblyInfo.AssemblyCompany" (func $AssemblyInfo.AssemblyCompany))
    (export "AssemblyInfo.AssemblyProduct" (func $AssemblyInfo.AssemblyProduct))
    (export "AssemblyInfo.AssemblyCopyright" (func $AssemblyInfo.AssemblyCopyright))
    (export "AssemblyInfo.AssemblyTrademark" (func $AssemblyInfo.AssemblyTrademark))
    (export "AssemblyInfo.Guid" (func $AssemblyInfo.Guid))
    (export "AssemblyInfo.AssemblyVersion" (func $AssemblyInfo.AssemblyVersion))
    (export "AssemblyInfo.AssemblyFileVersion" (func $AssemblyInfo.AssemblyFileVersion))
    (export "AssemblyInfo.AssemblyFullName" (func $AssemblyInfo.AssemblyFullName))
    
     

    ;; functions in [App]
    
    (func $App.RunApp  (result i32)
        ;; Public Function RunApp() As i32
        
    (local $textNode i32)
    (local $notes i32)
    (local $message1 i32)
    (local $message2 i32)
    
    (set_local $textNode (call $document.DOMById (i32.const 112)))
    (set_local $notes (call $document.DOMById (i32.const 120)))
    (set_local $message1 (call $document.createElement (i32.const 128)))
    (set_local $message2 (call $document.createElement (i32.const 136)))
    (drop (call $document.setText (get_local $textNode) (get_global $App.helloWorld)))
    (drop (call $document.setText (get_local $message1) (get_global $App.note)))
    (drop (call $document.setText (get_local $message2) (get_global $App.note2)))
    (drop (call $document.appendChild (get_local $notes) (get_local $message1)))
    (drop (call $document.appendChild (get_local $notes) (get_local $message2)))
    (drop (call $document.setAttribute (get_local $notes) (i32.const 144) (i32.const 152)))
    (drop (call $document.setAttribute (get_local $message1) (i32.const 184) (i32.const 192)))
    (drop (call $document.setAttribute (get_local $message2) (i32.const 224) (i32.const 232)))
    (drop (call $console.log (i32.const 264)))
    (drop (call $console.warn (get_global $App.note)))
    (drop (call $console.info (get_global $App.note2)))
    (drop (call $console.error (i32.const 304)))
    (return (i32.const 0))
    )
    
    
    
    
    ;; functions in [array]
    
    (func $array.arrayMemberTest  
        ;; Public Function arrayMemberTest() As void
        
    (local $len i32)
    
    (set_local $len (i32.load (i32.add (get_global $array.stringArray) (i32.const 4))))
    )
    
    
    (func $array.listArray  
        ;; Public Function listArray() As void
        
    (local $list i32)
    
    (set_local $list (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $f64_array.push (call $array.new (i32.const -1)) (i32.const 6)) (i32.const 54)) (i32.const 68)) (i32.const 988)) (i32.const 9654)) (i32.const 65)) (i32.const 464)))
    )
    
    
    
    
    ;; functions in [Math]
    
    (func $Math.PoissonPDF (param $k i32) (param $lambda f64) (result f64)
        ;; Public Function PoissonPDF(k As i32, lambda As f64) As f64
        
    (local $arrayOffset_70000b5w8Dl i32)
    (local $itemOffset_90000cj40iE i32)
    (local $result f64)
    
    (set_local $result (call $Math.Exp (f64.sub (f64.const 0) (get_local $lambda))))
    ;; Start Do While Block block_90000d7HnPD
    
    (block $block_90000d7HnPD 
        (loop $loop_80000eK2vxY
    
                    (br_if $block_90000d7HnPD (i32.eqz (i32.ge_s (get_local $k) (i32.const 1))))
            (set_local $result (f64.mul (get_local $result) (f64.div (get_local $lambda) (f64.convert_s/i32 (get_local $k)))))
            (set_local $k (i32.sub (get_local $k) (i32.const 1)))
            (br $loop_80000eK2vxY)
    
        )
    )
    ;; End Loop loop_80000eK2vxY
    (return (get_local $result))
    )
    
    
    (func $Math.DisplayResult (param $k i32) (param $lambda f64) (param $fontsize i32) (param $background i32) (result i32)
        ;; Public Function DisplayResult(k As i32, lambda As f64, fontsize As string, background As string) As i32
        
    (local $pdf f64)
    
    (set_local $pdf (call $Math.PoissonPDF (get_local $k) (get_local $lambda)))
    (drop (call $console.warn (get_local $fontsize)))
    (drop (call $console.log (call $i32.toString (call $document.DOMById (i32.const 440)))))
    (drop (call $document.setText (call $document.DOMById (i32.const 448)) (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 456) (call $i32.toString (get_local $k))) (i32.const 496)) (call $f64.toString (get_local $lambda))) (i32.const 504)) (call $f64.toString (get_local $pdf))) (i32.const 512))))
    (drop (call $document.setAttribute (call $document.DOMById (i32.const 520)) (i32.const 528) (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 536) (call $i32.toString (get_local $fontsize))) (i32.const 568)) (call $i32.toString (get_local $background))) (i32.const 592))))
    (return (i32.const 0))
    )
    
    
    
    
    ;; functions in [AssemblyInfo]
    
    (func $AssemblyInfo.AssemblyTitle  (result i32)
        ;; Public Function AssemblyTitle() As string
        
    
    
    (return (i32.const 600))
    )
    
    
    (func $AssemblyInfo.AssemblyDescription  (result i32)
        ;; Public Function AssemblyDescription() As string
        
    
    
    (return (i32.const 616))
    )
    
    
    (func $AssemblyInfo.AssemblyCompany  (result i32)
        ;; Public Function AssemblyCompany() As string
        
    
    
    (return (i32.const 704))
    )
    
    
    (func $AssemblyInfo.AssemblyProduct  (result i32)
        ;; Public Function AssemblyProduct() As string
        
    
    
    (return (i32.const 720))
    )
    
    
    (func $AssemblyInfo.AssemblyCopyright  (result i32)
        ;; Public Function AssemblyCopyright() As string
        
    
    
    (return (i32.const 736))
    )
    
    
    (func $AssemblyInfo.AssemblyTrademark  (result i32)
        ;; Public Function AssemblyTrademark() As string
        
    
    
    (return (i32.const 760))
    )
    
    
    (func $AssemblyInfo.Guid  (result i32)
        ;; Public Function Guid() As string
        
    
    
    (return (i32.const 776))
    )
    
    
    (func $AssemblyInfo.AssemblyVersion  (result i32)
        ;; Public Function AssemblyVersion() As string
        
    
    
    (return (i32.const 816))
    )
    
    
    (func $AssemblyInfo.AssemblyFileVersion  (result i32)
        ;; Public Function AssemblyFileVersion() As string
        
    
    
    (return (i32.const 832))
    )
    
    
    (func $AssemblyInfo.AssemblyFullName  (result i32)
        ;; Public Function AssemblyFullName() As string
        
    
    
    (return (i32.const 848))
    )
    
    
    


    ;; Application Initialize
    ;; 
    ;; Sub New
    (func $Application_SubNew
        ;; call of the global variable initialize
        (call $global.initializer )

        (call $App.constructor )

(call $array.constructor )

(call $console.constructor )

(call $document.constructor )

(call $Math.constructor )
    )

    ;; Initializer for global variables if it is not a primitive abstract type
    (func $global.initializer  
    ;; Public Function initializer() As void
    



)

    (func $App.constructor  
    ;; Public Function constructor() As void
    



)

(func $array.constructor  
    ;; Public Function constructor() As void
    
(local $arrayOffset_70000b5w8Dl i32)
(local $itemOffset_90000cj40iE i32)


;; Save (i32.const 3) array element data to memory:
;; Array memory block begin at location: (get_local $arrayOffset_70000b5w8Dl)
(set_local $arrayOffset_70000b5w8Dl (call $global.ObjectManager.Allocate (i32.add (i32.const 8) (i32.mul (i32.const 3) (i32.const 4))) (i32.const 7)))
;; class_id/typealias_enum i32 data: (i32.const 5)/array(Of string)
(i32.store (get_local $arrayOffset_70000b5w8Dl) (i32.const 5))
(i32.store (i32.add (get_local $arrayOffset_70000b5w8Dl) (i32.const 4)) (i32.const 3))
;; End of byte marks meta data, start write data blocks
(set_local $itemOffset_90000cj40iE (i32.add (get_local $arrayOffset_70000b5w8Dl) (i32.const 8)))
(i32.store (i32.add (get_local $itemOffset_90000cj40iE) (i32.const 0)) (i32.const 368))
(i32.store (i32.add (get_local $itemOffset_90000cj40iE) (i32.const 4)) (i32.const 384))
(i32.store (i32.add (get_local $itemOffset_90000cj40iE) (i32.const 8)) (i32.const 416))
;; Assign array memory data to another expression
(set_global $array.stringArray (get_local $arrayOffset_70000b5w8Dl))
)

(func $console.constructor  
    ;; Public Function constructor() As void
    



)

(func $document.constructor  
    ;; Public Function constructor() As void
    



)

(func $Math.constructor  
    ;; Public Function constructor() As void
    



)

    (start $Application_SubNew)
)