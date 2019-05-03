(module ;; Module HelloWorld

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/4/2019 2:42:19 AM
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
    ;; Declare Function i32.toString Lib "string" Alias "toString" (x As i32) As string
    (func $i32.toString (import "string" "toString") (param $x i32) (result i32))
    ;; Declare Function f64.toString Lib "string" Alias "toString" (x As f64) As string
    (func $f64.toString (import "string" "toString") (param $x f64) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $global.ObjectManager (mut i32) (i32.const 635))

    ;; Memory data for string constant
    
    ;; String from 1 with 12 bytes in memory
    (data (i32.const 1) "Hello World!\00")

    ;; String from 14 with 54 bytes in memory
    (data (i32.const 14) "This message comes from a VisualBasic.NET application!\00")

    ;; String from 69 with 21 bytes in memory
    (data (i32.const 69) "WebAssembly it works!\00")

    ;; String from 91 with 4 bytes in memory
    (data (i32.const 91) "text\00")

    ;; String from 96 with 5 bytes in memory
    (data (i32.const 96) "notes\00")

    ;; String from 102 with 1 bytes in memory
    (data (i32.const 102) "p\00")

    ;; String from 104 with 1 bytes in memory
    (data (i32.const 104) "p\00")

    ;; String from 106 with 5 bytes in memory
    (data (i32.const 106) "style\00")

    ;; String from 112 with 28 bytes in memory
    (data (i32.const 112) "background-color: lightgrey;\00")

    ;; String from 141 with 5 bytes in memory
    (data (i32.const 141) "style\00")

    ;; String from 147 with 27 bytes in memory
    (data (i32.const 147) "font-size: 2em; color: red;\00")

    ;; String from 175 with 5 bytes in memory
    (data (i32.const 175) "style\00")

    ;; String from 181 with 29 bytes in memory
    (data (i32.const 181) "font-size: 5em; color: green;\00")

    ;; String from 211 with 33 bytes in memory
    (data (i32.const 211) "Debug text message display below:\00")

    ;; String from 245 with 56 bytes in memory
    (data (i32.const 245) "Try to display an error message on javascript console...\00")

    ;; String from 302 with 6 bytes in memory
    (data (i32.const 302) "result\00")

    ;; String from 309 with 6 bytes in memory
    (data (i32.const 309) "result\00")

    ;; String from 316 with 37 bytes in memory
    (data (i32.const 316) "The calculation result of PoissonPDF(\00")

    ;; String from 354 with 2 bytes in memory
    (data (i32.const 354) ", \00")

    ;; String from 357 with 5 bytes in memory
    (data (i32.const 357) ") is \00")

    ;; String from 363 with 1 bytes in memory
    (data (i32.const 363) "!\00")

    ;; String from 365 with 6 bytes in memory
    (data (i32.const 365) "result\00")

    ;; String from 372 with 5 bytes in memory
    (data (i32.const 372) "style\00")

    ;; String from 378 with 24 bytes in memory
    (data (i32.const 378) "color: blue; font-size: \00")

    ;; String from 403 with 20 bytes in memory
    (data (i32.const 403) "; background-color: \00")

    ;; String from 424 with 1 bytes in memory
    (data (i32.const 424) ";\00")

    ;; String from 426 with 11 bytes in memory
    (data (i32.const 426) "HelloWorld!\00")

    ;; String from 438 with 80 bytes in memory
    (data (i32.const 438) "A hello world demo project for VisualBasic.NET WebAssembly compiler and runtime.\00")

    ;; String from 519 with 10 bytes in memory
    (data (i32.const 519) "xieguigang\00")

    ;; String from 530 with 10 bytes in memory
    (data (i32.const 530) "HelloWorld\00")

    ;; String from 541 with 20 bytes in memory
    (data (i32.const 541) "Copyright © MIT 2019\00")

    ;; String from 562 with 11 bytes in memory
    (data (i32.const 562) "HelloWorld!\00")

    ;; String from 574 with 36 bytes in memory
    (data (i32.const 574) "8750377f-b6e7-4fb5-886b-4c3fa451ec4c\00")

    ;; String from 611 with 13 bytes in memory
    (data (i32.const 611) "123.34.0.5466\00")

    ;; String from 625 with 9 bytes in memory
    (data (i32.const 625) "1.0.99.78\00")
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    

    ;; Global variables in this module
    (global $App.helloWorld (mut i32) (i32.const 1))

(global $App.note (mut i32) (i32.const 14))

(global $App.note2 (mut i32) (i32.const 69))

    ;; Export methods of this module
    ;; export from VB.NET module: [App]
    
    (export "App.RunApp" (func $App.RunApp))
    
    
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
    
     

    ;; functions in [App]
    
    (func $App.RunApp  (result i32)
        ;; Public Function RunApp() As i32
        (local $textNode i32)
    (local $notes i32)
    (local $message1 i32)
    (local $message2 i32)
    (set_local $textNode (call $document.DOMById (i32.const 91)))
    (set_local $notes (call $document.DOMById (i32.const 96)))
    (set_local $message1 (call $document.createElement (i32.const 102)))
    (set_local $message2 (call $document.createElement (i32.const 104)))
    (drop (call $document.setText (get_local $textNode) (get_global $App.helloWorld)))
    (drop (call $document.setText (get_local $message1) (get_global $App.note)))
    (drop (call $document.setText (get_local $message2) (get_global $App.note2)))
    (drop (call $document.appendChild (get_local $notes) (get_local $message1)))
    (drop (call $document.appendChild (get_local $notes) (get_local $message2)))
    (drop (call $document.setAttribute (get_local $notes) (i32.const 106) (i32.const 112)))
    (drop (call $document.setAttribute (get_local $message1) (i32.const 141) (i32.const 147)))
    (drop (call $document.setAttribute (get_local $message2) (i32.const 175) (i32.const 181)))
    (drop (call $console.log (i32.const 211)))
    (drop (call $console.warn (get_global $App.note)))
    (drop (call $console.info (get_global $App.note2)))
    (drop (call $console.error (i32.const 245)))
    (return (i32.const 0))
    )
    
    
    ;; functions in [Math]
    
    (func $Math.PoissonPDF (param $k i32) (param $lambda f64) (result f64)
        ;; Public Function PoissonPDF(k As i32, lambda As f64) As f64
        (local $result f64)
    (set_local $result (call $Math.Exp (f64.sub (f64.const 0) (get_local $lambda))))
    ;; Start Do While Block block_9a020000
    
    (block $block_9a020000 
        (loop $loop_9b020000
    
                    (br_if $block_9a020000 (i32.eqz (i32.ge_s (get_local $k) (i32.const 1))))
            (set_local $result (f64.mul (get_local $result) (f64.div (get_local $lambda) (f64.convert_s/i32 (get_local $k)))))
            (set_local $k (i32.sub (get_local $k) (i32.const 1)))
            (br $loop_9b020000)
    
        )
    )
    ;; End Loop loop_9b020000
    (return (get_local $result))
    )
    (func $Math.DisplayResult (param $k i32) (param $lambda f64) (param $fontsize i32) (param $background i32) (result i32)
        ;; Public Function DisplayResult(k As i32, lambda As f64, fontsize As string, background As string) As i32
        (local $pdf f64)
    (set_local $pdf (call $Math.PoissonPDF (get_local $k) (get_local $lambda)))
    (drop (call $console.warn (get_local $fontsize)))
    (drop (call $console.log (call $i32.toString (call $document.DOMById (i32.const 302)))))
    (drop (call $document.setText (call $document.DOMById (i32.const 309)) (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 316) (call $i32.toString (get_local $k))) (i32.const 354)) (call $f64.toString (get_local $lambda))) (i32.const 357)) (call $f64.toString (get_local $pdf))) (i32.const 363))))
    (drop (call $document.setAttribute (call $document.DOMById (i32.const 365)) (i32.const 372) (call $string.add (call $string.add (call $string.add (call $string.add (i32.const 378) (call $i32.toString (get_local $fontsize))) (i32.const 403)) (call $i32.toString (get_local $background))) (i32.const 424))))
    (return (i32.const 0))
    )
    
    
    ;; functions in [AssemblyInfo]
    
    (func $AssemblyInfo.AssemblyTitle  (result i32)
        ;; Public Function AssemblyTitle() As string
        
    (return (i32.const 426))
    )
    (func $AssemblyInfo.AssemblyDescription  (result i32)
        ;; Public Function AssemblyDescription() As string
        
    (return (i32.const 438))
    )
    (func $AssemblyInfo.AssemblyCompany  (result i32)
        ;; Public Function AssemblyCompany() As string
        
    (return (i32.const 519))
    )
    (func $AssemblyInfo.AssemblyProduct  (result i32)
        ;; Public Function AssemblyProduct() As string
        
    (return (i32.const 530))
    )
    (func $AssemblyInfo.AssemblyCopyright  (result i32)
        ;; Public Function AssemblyCopyright() As string
        
    (return (i32.const 541))
    )
    (func $AssemblyInfo.AssemblyTrademark  (result i32)
        ;; Public Function AssemblyTrademark() As string
        
    (return (i32.const 562))
    )
    (func $AssemblyInfo.Guid  (result i32)
        ;; Public Function Guid() As string
        
    (return (i32.const 574))
    )
    (func $AssemblyInfo.AssemblyVersion  (result i32)
        ;; Public Function AssemblyVersion() As string
        
    (return (i32.const 611))
    )
    (func $AssemblyInfo.AssemblyFileVersion  (result i32)
        ;; Public Function AssemblyFileVersion() As string
        
    (return (i32.const 625))
    )
    

;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew

)

(start $Application_SubNew)

)