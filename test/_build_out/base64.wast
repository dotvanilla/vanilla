(module ;; Microsoft VisualBasic Project base64

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 
    ;; build: 2026-10-13 16:54:43
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    
    ;; Javascript function imports into current WebAssembly Module
    ;;
    ;; ----- NOTE: imports must occur before all non-import definitions ------

    ;; JAVASCRIPT [Math]
    (func $Math.Pow (import "Math" "pow") (param $base f64) (param $exponent f64) (result f64))
    (func $Math.Sqrt (import "Math" "sqrt") (param $x f64) (result f64))
    (func $Math.Exp (import "Math" "exp") (param $x f64) (result f64))
    (func $Math.Log (import "Math" "log") (param $x f64) (result f64))
    (func $Math.Log10 (import "Math" "log10") (param $x f64) (result f64))
    (func $Math.Cos (import "Math" "cos") (param $x f64) (result f64))
    (func $Math.Sin (import "Math" "sin") (param $x f64) (result f64))
    (func $Math.Tan (import "Math" "tan") (param $x f64) (result f64))
    (func $Math.Floor (import "Math" "floor") (param $x f64) (result f64))
    (func $Math.Ceiling (import "Math" "ceil") (param $x f64) (result f64))
    (func $Math.Round (import "Math" "round") (param $x f64) (result f64))
    (func $Math.Abs (import "Math" "abs") (param $x f64) (result f64))
    (func $Math.Min (import "Math" "min") (param $a f64) (param $b f64) (result f64))
    (func $Math.Max (import "Math" "max") (param $a f64) (param $b f64) (result f64))
    (func $Math.Random (import "Math" "random")  (result f64))
    ;; JAVASCRIPT [console]
    (func $Console.WriteLine (import "console" "log") (param $msg i32) )
    ;; JAVASCRIPT [Encoding]
    (func $Encoding.UTF8.GetBytes (import "Encoding" "GetBytes") (param $text i32) (result i32))
    (func $Encoding.UTF8.GetString (import "Encoding" "GetString") (param $bytes i32) (result i32))
    ;; JAVASCRIPT [StringBuilder]
    (func $StringBuilder.New (import "StringBuilder" "NewStringBuilder")  (result i32))
    (func $StringBuilder.Append (import "StringBuilder" "Append") (param $sb i32) (param $text i32) (result i32))
    (func $StringBuilder.AppendChar (import "StringBuilder" "AppendChar") (param $sb i32) (param $c i32) (result i32))
    (func $StringBuilder.AppendLine (import "StringBuilder" "AppendLine") (param $sb i32) (param $text i32) (result i32))
    (func $StringBuilder.ToString (import "StringBuilder" "ToString") (param $sb i32) (result i32))
    (func $StringBuilder.Clear (import "StringBuilder" "Clear") (param $sb i32) (result i32))
    ;; JAVASCRIPT [Strings]
    (func $String.Empty (import "Strings" "Empty")  (result i32))
    (func $Strings.ChrW (import "Strings" "ChrW") (param $code i32) (result i32))
    (func $Strings.NewString (import "Strings" "NewString") (param $c i32) (param $n i32) (result i32))
    (func $Strings.Concat (import "Strings" "Concat") (param $a i32) (param $b i32) (result i32))
    (func $Strings.Join (import "Strings" "Join") (param $separator i32) (param $array i32) (result i32))
    (func $Strings.AscW (import "Strings" "AscW") (param $text i32) (result i32))
    ;; JAVASCRIPT [string]
    (func $string.i32.toString (import "string" "I32ToString") (param $x i32) (result i32))
    (func $string.f64.toString (import "string" "F64ToString") (param $x f64) (result i32))
    (func $string.add (import "string" "Add") (param $a i32) (param $b i32) (result i32))
    (func $string.length (import "string" "Length") (param $s i32) (result i32))
    (func $string.charAt (import "string" "CharAt") (param $s i32) (param $index i32) (result i32))
    (func $string.substr (import "string" "Substring") (param $s i32) (param $start i32) (param $len i32) (result i32))
    (func $string.replace (import "string" "Replace") (param $s i32) (param $find i32) (param $replacement i32) (result i32))
    (func $string.trim (import "string" "Trim") (param $s i32) (result i32))
    (func $string.indexOf (import "string" "IndexOf") (param $s i32) (param $find i32) (result i32))
    (func $string.upper (import "string" "ToUpper") (param $s i32) (result i32))
    (func $string.lower (import "string" "ToLower") (param $s i32) (result i32))
    (func $string.equals (import "string" "Equals") (param $a i32) (param $b i32) (result i32))
    ;; JAVASCRIPT [Convert]
    (func $Convert.ToBase64String (import "Convert" "ToBase64String") (param $bytes i32) (result i32))
    (func $Convert.FromBase64String (import "Convert" "FromBase64String") (param $text i32) (result i32))
    (func $Convert.ToString (import "Convert" "ToString") (param $value i32) (result i32))
    ;; JAVASCRIPT [Random]
    (func $Random.New (import "Random" "New") (param $seed i32) (result i32))
    (func $Random.Next (import "Random" "Next") (param $rnd i32) (param $minValue i32) (param $maxValue i32) (result i32))
    (func $Random.NextDouble (import "Random" "NextDouble") (param $rnd i32) (result f64))
    ;; JAVASCRIPT [Array]
    (func $Array.New (import "Array" "New") (param $size i32) (result i32))
    (func $Array.length (import "Array" "Length") (param $array i32) (result i32))
    (func $Array.get.i32 (import "Array" "GetI32") (param $array i32) (param $index i32) (result i32))
    (func $Array.set.i32 (import "Array" "SetI32") (param $array i32) (param $index i32) (param $value i32) (result i32))
    (func $Array.get.f64 (import "Array" "GetF64") (param $array i32) (param $index i32) (result f64))
    (func $Array.set.f64 (import "Array" "SetF64") (param $array i32) (param $index i32) (param $value f64) (result i32))
    
    ;; ----- END OF JAVASCRIPT IMPORTS -----    

    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    
    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $WATMemoryBufferSize (mut i32) (i32.const 1320))

        
    ;; memory allocate in javascript runtime
    ;; functions in [WASM]


    ;; Public Function WASM.GetMemorySize() As System.Int32
    (func $WASM.GetMemorySize  (result i32)

        

        (return (get_global $ObjectManager.HeapSize))
    )

    ;; Public Function WASM.AllocateObject(sizeof As System.Int32, class_id As System.Int32) As System.Int32
    (func $WASM.AllocateObject (param $sizeof i32) (param $class_id i32) (result i32)

        

        (set_global $ObjectManager.HeapSize (i32.add (get_global $ObjectManager.HeapSize) (get_local $sizeof)))
        (return (get_global $ObjectManager.HeapSize))
    )

    ;; Public Function WASM.AllocateArray(sizeof As System.Int32, class_id As System.Int32, length As System.Int32) As System.Int32
    (func $WASM.AllocateArray (param $sizeof i32) (param $class_id i32) (param $length i32) (result i32)

        

        (return (get_global $ObjectManager.HeapSize))
    )


    
    ;; Export Api to JavaScript runtime for
    ;; expose GC in WASM module.
    (export "WASM.GetMemorySize" (func $WASM.GetMemorySize))
    

    ;; Memory data for string constant
        
    ;; String from 0d000000 with 64 bytes in memory
    (data (i32.const 13) "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/\00")
        
    ;; String from 50000000 with 2 bytes in memory
    (data (i32.const 80) "==\00")
        
    ;; String from 58000000 with 1 bytes in memory
    (data (i32.const 88) "=\00")
        
    ;; String from 60000000 with 1 bytes in memory
    (data (i32.const 96) "=\00")
        
    ;; String from 68000000 with 1 bytes in memory
    (data (i32.const 104) "=\00")
        
    ;; String from 70000000 with 1 bytes in memory
    (data (i32.const 112) "=\00")
        
    ;; String from 78000000 with 1 bytes in memory
    (data (i32.const 120) "=\00")
        
    ;; String from 80000000 with 1 bytes in memory
    (data (i32.const 128) "=\00")
        
    ;; String from 88000000 with 1 bytes in memory
    (data (i32.const 136) "=\00")
        
    ;; String from 90000000 with 1 bytes in memory
    (data (i32.const 144) "=\00")
        
    ;; String from 98000000 with 26 bytes in memory
    (data (i32.const 152) "========== \e7\bb\8f\e5\85\b8\e7\a4\ba\e4\be\8b ==========\00")
        
    ;; String from b8000000 with 3 bytes in memory
    (data (i32.const 184) "Man\00")
        
    ;; String from c0000000 with 2 bytes in memory
    (data (i32.const 192) "Ma\00")
        
    ;; String from c8000000 with 1 bytes in memory
    (data (i32.const 200) "M\00")
        
    ;; String from d0000000 with 34 bytes in memory
    (data (i32.const 208) "========== \e4\b8\ad\e6\96\87\ef\bc\88UTF-8 \e5\a4\9a\e5\ad\97\e8\8a\82\ef\bc\89==========\00")
        
    ;; String from f8000000 with 10 bytes in memory
    (data (i32.const 248) "\e4\bd\a0\e5\a5\bd\ef\bc\8cBase64\ef\bc\81\00")
        
    ;; String from 08010000 with 26 bytes in memory
    (data (i32.const 264) "========== \e8\be\b9\e7\95\8c\e6\83\85\e5\86\b5 ==========\00")
        
    ;; String from 28010000 with 0 bytes in memory
    (data (i32.const 296) "\00")
        
    ;; String from 30010000 with 37 bytes in memory
    (data (i32.const 304) "========== \e4\b8\8e .NET \e5\86\85\e7\bd\ae\e5\ae\9e\e7\8e\b0\e4\ba\a4\e5\8f\89\e9\aa\8c\e8\af\81 ==========\00")
        
    ;; String from 58010000 with 3 bytes in memory
    (data (i32.const 344) "\e9\95\bf\e5\ba\a6 \00")
        
    ;; String from 60010000 with 11 bytes in memory
    (data (i32.const 352) " \e7\9a\84\e7\bb\93\e6\9e\9c\e4\b8\8e\e6\a0\87\e5\87\86\e4\b8\8d\e4\b8\80\e8\87\b4\ef\bc\81\00")
        
    ;; String from 70010000 with 24 bytes in memory
    (data (i32.const 368) "\e9\95\bf\e5\ba\a6 0~60 \e5\85\b1 61 \e7\bb\84\e9\9a\8f\e6\9c\ba\e6\95\b0\e6\8d\ae\e5\85\a8\e9\83\a8\e9\80\9a\e8\bf\87: \00")
        
    ;; String from 90010000 with 10 bytes in memory
    (data (i32.const 400) "\e5\8e\9f\e6\96\87     : [\00")
        
    ;; String from a0010000 with 1 bytes in memory
    (data (i32.const 416) "]\00")
        
    ;; String from a8010000 with 12 bytes in memory
    (data (i32.const 424) "Base64   : [\00")
        
    ;; String from b8010000 with 1 bytes in memory
    (data (i32.const 440) "]\00")
        
    ;; String from c0010000 with 8 bytes in memory
    (data (i32.const 448) "\e8\a7\a3\e7\a0\81\e8\bf\98\e5\8e\9f : [\00")
        
    ;; String from d0010000 with 1 bytes in memory
    (data (i32.const 464) "]\00")
        
    ;; String from d8010000 with 7 bytes in memory
    (data (i32.const 472) "\e8\bf\98\e5\8e\9f\e4\b8\80\e8\87\b4 : \00")
        
    ;; String from e0010000 with 1 bytes in memory
    (data (i32.const 480) "-\00")
    
    ;; AssemblyInfo.vb
        
    ;; String from e8010000 with 531 bytes in memory
    (data (i32.const 488) "d15:AssemblyCompany22:https://vanillavb.app/17:AssemblyCopyright22:https://vanillavb.app/19:AssemblyDescription46:WebAssembly Compiler for Microsoft VisualBasic19:AssemblyFileVersion0:16:AssemblyFullName6:base6428:AssemblyInformationalVersion0:15:AssemblyProduct22:https://vanillavb.app/13:AssemblyTitle46:WebAssembly Compiler for Microsoft VisualBasic17:AssemblyTrademark22:https://vanillavb.app/15:AssemblyVersion0:9:BuiltTime0:10:ComVisible5:False4:Guid36:f45ebe40-1963-4688-8992-efd513675ddc4:Name0:15:TargetFramework7:net10.0e\00")

    (func $AssemblyInfo (result i32)
        (return (i32.const 488))    
    )

    (export "AssemblyInfo" (func $AssemblyInfo)) 

    ;; AssemblyInfo fields (consumed by the javascript runtime)
        
    ;; String from 00040000 with 46 bytes in memory
    (data (i32.const 1024) "WebAssembly Compiler for Microsoft VisualBasic\00")
    (func $AssemblyInfo.AssemblyTitle (result i32)
        (return (i32.const 1024))
    )
    (export "AssemblyInfo.AssemblyTitle" (func $AssemblyInfo.AssemblyTitle))
        
    ;; String from 30040000 with 46 bytes in memory
    (data (i32.const 1072) "WebAssembly Compiler for Microsoft VisualBasic\00")
    (func $AssemblyInfo.AssemblyDescription (result i32)
        (return (i32.const 1072))
    )
    (export "AssemblyInfo.AssemblyDescription" (func $AssemblyInfo.AssemblyDescription))
        
    ;; String from 60040000 with 22 bytes in memory
    (data (i32.const 1120) "https://vanillavb.app/\00")
    (func $AssemblyInfo.AssemblyCompany (result i32)
        (return (i32.const 1120))
    )
    (export "AssemblyInfo.AssemblyCompany" (func $AssemblyInfo.AssemblyCompany))
        
    ;; String from 78040000 with 22 bytes in memory
    (data (i32.const 1144) "https://vanillavb.app/\00")
    (func $AssemblyInfo.AssemblyProduct (result i32)
        (return (i32.const 1144))
    )
    (export "AssemblyInfo.AssemblyProduct" (func $AssemblyInfo.AssemblyProduct))
        
    ;; String from 90040000 with 22 bytes in memory
    (data (i32.const 1168) "https://vanillavb.app/\00")
    (func $AssemblyInfo.AssemblyCopyright (result i32)
        (return (i32.const 1168))
    )
    (export "AssemblyInfo.AssemblyCopyright" (func $AssemblyInfo.AssemblyCopyright))
        
    ;; String from a8040000 with 22 bytes in memory
    (data (i32.const 1192) "https://vanillavb.app/\00")
    (func $AssemblyInfo.AssemblyTrademark (result i32)
        (return (i32.const 1192))
    )
    (export "AssemblyInfo.AssemblyTrademark" (func $AssemblyInfo.AssemblyTrademark))
        
    ;; String from c0040000 with 0 bytes in memory
    (data (i32.const 1216) "\00")
    (func $AssemblyInfo.AssemblyInformationalVersion (result i32)
        (return (i32.const 1216))
    )
    (export "AssemblyInfo.AssemblyInformationalVersion" (func $AssemblyInfo.AssemblyInformationalVersion))
        
    ;; String from c8040000 with 7 bytes in memory
    (data (i32.const 1224) "net10.0\00")
    (func $AssemblyInfo.TargetFramework (result i32)
        (return (i32.const 1224))
    )
    (export "AssemblyInfo.TargetFramework" (func $AssemblyInfo.TargetFramework))
        
    ;; String from d0040000 with 5 bytes in memory
    (data (i32.const 1232) "False\00")
    (func $AssemblyInfo.ComVisible (result i32)
        (return (i32.const 1232))
    )
    (export "AssemblyInfo.ComVisible" (func $AssemblyInfo.ComVisible))
        
    ;; String from d8040000 with 0 bytes in memory
    (data (i32.const 1240) "\00")
    (func $AssemblyInfo.Name (result i32)
        (return (i32.const 1240))
    )
    (export "AssemblyInfo.Name" (func $AssemblyInfo.Name))
        
    ;; String from e0040000 with 36 bytes in memory
    (data (i32.const 1248) "f45ebe40-1963-4688-8992-efd513675ddc\00")
    (func $AssemblyInfo.Guid (result i32)
        (return (i32.const 1248))
    )
    (export "AssemblyInfo.Guid" (func $AssemblyInfo.Guid))
        
    ;; String from 08050000 with 0 bytes in memory
    (data (i32.const 1288) "\00")
    (func $AssemblyInfo.AssemblyVersion (result i32)
        (return (i32.const 1288))
    )
    (export "AssemblyInfo.AssemblyVersion" (func $AssemblyInfo.AssemblyVersion))
        
    ;; String from 10050000 with 0 bytes in memory
    (data (i32.const 1296) "\00")
    (func $AssemblyInfo.AssemblyFileVersion (result i32)
        (return (i32.const 1296))
    )
    (export "AssemblyInfo.AssemblyFileVersion" (func $AssemblyInfo.AssemblyFileVersion))
        
    ;; String from 18050000 with 6 bytes in memory
    (data (i32.const 1304) "base64\00")
    (func $AssemblyInfo.AssemblyFullName (result i32)
        (return (i32.const 1304))
    )
    (export "AssemblyInfo.AssemblyFullName" (func $AssemblyInfo.AssemblyFullName))
        
    ;; String from 20050000 with 0 bytes in memory
    (data (i32.const 1312) "\00")
    (func $AssemblyInfo.BuiltTime (result i32)
        (return (i32.const 1312))
    )
    (export "AssemblyInfo.BuiltTime" (func $AssemblyInfo.BuiltTime))

    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    
    ;; ------------------------------------
    ;; No user type meta data at here....
    ;; ------------------------------------

    ;; Math constant values in .NET Framework
    (global $Math.PI (mut f64) (f64.const 3.141592653589793))
    (global $Math.E (mut f64) (f64.const 2.718281828459045))
    (global $Math.Tau (mut f64) (f64.const 6.283185307179586))
    (global $Integer.MinValue (mut i32) (i32.const -2147483648))
    (global $Integer.MaxValue (mut i32) (i32.const 2147483647))
    (global $Long.MinValue (mut i64) (i64.const -9223372036854775808))
    (global $Long.MaxValue (mut i64) (i64.const 9223372036854775807))
    (global $Single.MinValue (mut f32) (f32.const -3.4028235E+38))
    (global $Single.MaxValue (mut f32) (f32.const 3.4028235E+38))
    (global $Double.MinValue (mut f64) (f64.const -1.7976931348623157E+308))
    (global $Double.MaxValue (mut f64) (f64.const 1.7976931348623157E+308))

    ;; Global variables in this module
    (global $Alphabet (mut i32) (i32.const 0))
    (global $ObjectManager.HeapSize (mut i32) (i32.const 1320))

    ;; Export methods of this module
    (export "Base64Codec.EncodeText" (func $base64.Base64Codec.EncodeText))
    (export "Base64Codec.DecodeText" (func $base64.Base64Codec.DecodeText))
    (export "Base64Codec.EncodeBytes" (func $base64.Base64Codec.EncodeBytes))
    (export "Base64Codec.DecodeBytes" (func $base64.Base64Codec.DecodeBytes))
    (export "Program.Main" (func $base64.Program.Main)) 

    ;; functions in [base64.Base64Codec]


    ;; Public Function base64.Base64Codec.EncodeText(text As System.String) As System.String
    (func $base64.Base64Codec.EncodeText (param $text i32) (result i32)

        (local $bytes i32)

        (set_local $bytes (call $Encoding.UTF8.GetBytes (get_local $text)))
        (return (call $base64.Base64Codec.EncodeBytes (get_local $bytes)))
    )

    ;; Public Function base64.Base64Codec.DecodeText(base64 As System.String) As System.String
    (func $base64.Base64Codec.DecodeText (param $base64 i32) (result i32)

        (local $bytes i32)

        (set_local $bytes (call $base64.Base64Codec.DecodeBytes (get_local $base64)))
        (return (call $Encoding.UTF8.GetString (get_local $bytes)))
    )

    ;; Public Function base64.Base64Codec.EncodeBytes(data As System.Int32) As System.String
    (func $base64.Base64Codec.EncodeBytes (param $data i32) (result i32)

        (local $sb i32)
    (local $i i32)
    (local $b1 i32)
    (local $b2 i32)
    (local $b3 i32)
    (local $combined i32)
    (local $remaining i32)

        
    (if (i32.add (i32.eq (get_local $data) (i32.const 0)) (i32.eq (call $Array.length (get_local $data)) (i32.const 0))) 
        (then
                    (return (call $string.i32.toString (i32.const 0)))
        ) 
    )
        (set_local $sb (call $StringBuilder.New ))
        (set_local $i (i32.const 0))
        
    (block $WASMClosure_br_3ce79db40f8a3e3c 
        (loop $loop_tmp7034c0a1c8e65b41

                        
    ;; while ((boolean) (i32) ((i32) (i + (i32.const 3)) <= VanillaBasic.WebAssembly.Syntax.FunctionInvoke))
                (br_if $WASMClosure_br_3ce79db40f8a3e3c (i32.eqz (i32.le_s (i32.add (get_local $i) (i32.const 3)) (call $Array.length (get_local $data)))))
            (set_local $b1 (call $Array.get.i32 (get_local $data) (get_local $i)))
            (set_local $b2 (call $Array.get.i32 (get_local $data) (i32.add (get_local $i) (i32.const 1))))
            (set_local $b3 (call $Array.get.i32 (get_local $data) (i32.add (get_local $i) (i32.const 2))))
            (set_local $combined (i32.or (i32.or (i32.shl (get_local $b1) (i32.const 16)) (i32.shl (get_local $b2) (i32.const 8))) (get_local $b3)))
            (drop (call $StringBuilder.Append (get_local $sb) (call $string.charAt (get_global $Alphabet) (i32.and (i32.shr_s (get_local $combined) (i32.const 18)) (i32.const 63)))))
            (drop (call $StringBuilder.Append (get_local $sb) (call $string.charAt (get_global $Alphabet) (i32.and (i32.shr_s (get_local $combined) (i32.const 12)) (i32.const 63)))))
            (drop (call $StringBuilder.Append (get_local $sb) (call $string.charAt (get_global $Alphabet) (i32.and (i32.shr_s (get_local $combined) (i32.const 6)) (i32.const 63)))))
            (drop (call $StringBuilder.Append (get_local $sb) (call $string.charAt (get_global $Alphabet) (i32.and (get_local $combined) (i32.const 63)))))
            (set_local $i (i32.add (get_local $i) (i32.const 3)))
            (br $loop_tmp7034c0a1c8e65b41)

        )
    )
        (set_local $remaining (i32.sub (call $Array.length (get_local $data)) (get_local $i)))
        
    (if (i32.eq (get_local $remaining) (i32.const 1)) 
        (then
                    (set_local $b1 (call $Array.get.i32 (get_local $data) (get_local $i)))
        (set_local $combined (i32.shl (get_local $b1) (i32.const 16)))
        (drop (call $StringBuilder.Append (get_local $sb) (call $string.charAt (get_global $Alphabet) (i32.and (i32.shr_s (get_local $combined) (i32.const 18)) (i32.const 63)))))
        (drop (call $StringBuilder.Append (get_local $sb) (call $string.charAt (get_global $Alphabet) (i32.and (i32.shr_s (get_local $combined) (i32.const 12)) (i32.const 63)))))
        (drop (call $StringBuilder.Append (get_local $sb) (i32.const 80)))
        ) (else
                    
    (if (i32.eq (get_local $remaining) (i32.const 2)) 
        (then
                    (set_local $b1 (call $Array.get.i32 (get_local $data) (get_local $i)))
        (set_local $b2 (call $Array.get.i32 (get_local $data) (i32.add (get_local $i) (i32.const 1))))
        (set_local $combined (i32.or (i32.shl (get_local $b1) (i32.const 16)) (i32.shl (get_local $b2) (i32.const 8))))
        (drop (call $StringBuilder.Append (get_local $sb) (call $string.charAt (get_global $Alphabet) (i32.and (i32.shr_s (get_local $combined) (i32.const 18)) (i32.const 63)))))
        (drop (call $StringBuilder.Append (get_local $sb) (call $string.charAt (get_global $Alphabet) (i32.and (i32.shr_s (get_local $combined) (i32.const 12)) (i32.const 63)))))
        (drop (call $StringBuilder.Append (get_local $sb) (call $string.charAt (get_global $Alphabet) (i32.and (i32.shr_s (get_local $combined) (i32.const 6)) (i32.const 63)))))
        (drop (call $StringBuilder.Append (get_local $sb) (i32.const 88)))
        ) 
    )
    )
    )
        (return (call $string.i32.toString (i32.const 0)))
    )

    ;; Public Function base64.Base64Codec.DecodeBytes(base64 As System.String) As System.Int32
    (func $base64.Base64Codec.DecodeBytes (param $base64 i32) (result i32)

        (local $j i32)
    (local $padCount i32)
    (local $totalBytes i32)
    (local $result i32)
    (local $outPos i32)
    (local $i i32)
    (local $v1 i32)
    (local $v2 i32)
    (local $c3 i32)
    (local $c4 i32)
    (local $v3 i32)
    (local $v4 i32)
    (local $combined i32)

        
    (if (i32.eq (get_local $base64) (i32.const 0)) 
        (then
            
        ) 
    )
        
    (if (i32.eq (call $string.length (get_local $base64)) (i32.const 0)) 
        (then
                    (return (i32.const 0))
        ) 
    )
        
    (if (i32.ne (i32.rem_s (call $string.length (get_local $base64)) (i32.const 4)) (i32.const 0)) 
        (then
            
        ) 
    )
            
    ;; For j As Integer = 0 To base64.Length - 3
    (set_local $j (i32.const 0))
    
    (block $WASMClosure_br_b5a26c14613135f0 
        (loop $loop_tmp56f84b43dd69b231

                        
    ;; (boolean) (boolean) (i32) (j = (i32) (VanillaBasic.WebAssembly.Syntax.FunctionInvoke - (i32.const 3)))
                (br_if $WASMClosure_br_b5a26c14613135f0 (i32.eq (get_local $j) (i32.sub (call $string.length (get_local $base64)) (i32.const 3))))
            
    (if (call $string.equals (call $string.charAt (get_local $base64) (get_local $j)) (i32.const 96)) 
        (then
            
        ) 
    )
                        ;;For loop control step: (i32.const 1)
            (set_local $j (i32.add (get_local $j) (i32.const 1)))
            (br $loop_tmp56f84b43dd69b231)
                        ;;For Loop Next On loop_tmp56f84b43dd69b231

        )
    )
    
        (set_local $padCount (i32.const 0))
        
    (if (call $string.equals (call $string.charAt (get_local $base64) (i32.sub (call $string.length (get_local $base64)) (i32.const 1))) (i32.const 104)) 
        (then
                    (set_local $padCount (i32.add (get_local $padCount) (i32.const 1)))
        ) 
    )
        
    (if (i32.mul (i32.ge_s (get_local $padCount) (i32.const 1)) (call $string.equals (call $string.charAt (get_local $base64) (i32.sub (call $string.length (get_local $base64)) (i32.const 2))) (i32.const 112))) 
        (then
                    (set_local $padCount (i32.add (get_local $padCount) (i32.const 1)))
        ) 
    )
        (set_local $totalBytes (i32.sub (i32.mul (i32.div_s (call $string.length (get_local $base64)) (i32.const 4)) (i32.const 3)) (get_local $padCount)))
        (set_local $result (call $Array.New (i32.add (i32.sub (get_local $totalBytes) (i32.const 1)) (i32.const 1))))
        (set_local $outPos (i32.const 0))
        (set_local $i (i32.const 0))
        
    (block $WASMClosure_br_eea6c93e818517fc 
        (loop $loop_tmp415175a91dc83c48

                        
    ;; while ((boolean) (i32) (i < VanillaBasic.WebAssembly.Syntax.FunctionInvoke))
                (br_if $WASMClosure_br_eea6c93e818517fc (i32.eqz (i32.lt_s (get_local $i) (call $string.length (get_local $base64)))))
            (set_local $v1 (call $base64.Base64Codec.CharToValue (call $string.charAt (get_local $base64) (get_local $i))))
            (set_local $v2 (call $base64.Base64Codec.CharToValue (call $string.charAt (get_local $base64) (i32.add (get_local $i) (i32.const 1)))))
            (set_local $c3 (call $string.charAt (get_local $base64) (i32.add (get_local $i) (i32.const 2))))
            (set_local $c4 (call $string.charAt (get_local $base64) (i32.add (get_local $i) (i32.const 3))))
            (set_local $v3 (select (i32.const 0) (call $base64.Base64Codec.CharToValue (get_local $c3)) (call $string.equals (get_local $c3) (i32.const 120))))
            (set_local $v4 (select (i32.const 0) (call $base64.Base64Codec.CharToValue (get_local $c4)) (call $string.equals (get_local $c4) (i32.const 128))))
            (set_local $combined (i32.or (i32.or (i32.or (i32.shl (get_local $v1) (i32.const 18)) (i32.shl (get_local $v2) (i32.const 12))) (i32.shl (get_local $v3) (i32.const 6))) (get_local $v4)))
            (drop (i32.const 0))
            (set_local $outPos (i32.add (get_local $outPos) (i32.const 1)))
            
    (if (i32.eqz (call $string.equals (get_local $c3) (i32.const 136))) 
        (then
                    (drop (i32.const 0))
        (set_local $outPos (i32.add (get_local $outPos) (i32.const 1)))
        ) 
    )
            
    (if (i32.eqz (call $string.equals (get_local $c4) (i32.const 144))) 
        (then
                    (drop (i32.const 0))
        (set_local $outPos (i32.add (get_local $outPos) (i32.const 1)))
        ) 
    )
            (set_local $i (i32.add (get_local $i) (i32.const 4)))
            (br $loop_tmp415175a91dc83c48)

        )
    )
        (return (get_local $result))
    )

    ;; Public Function base64.Base64Codec.CharToValue(c As System.String) As System.Int32
    (func $base64.Base64Codec.CharToValue (param $c i32) (result i32)

        (local $i i32)

            
    ;; For i As Integer = 0 To Alphabet.Length - 1
    (set_local $i (i32.const 0))
    
    (block $WASMClosure_br_af20d2b8cd42a122 
        (loop $loop_tmp9f9d05e98b3d366e

                        
    ;; (boolean) (boolean) (i32) (i = (i32) (VanillaBasic.WebAssembly.Syntax.FunctionInvoke - (i32.const 1)))
                (br_if $WASMClosure_br_af20d2b8cd42a122 (i32.eq (get_local $i) (i32.sub (call $string.length (get_global $Alphabet)) (i32.const 1))))
            
    (if (call $string.equals (call $string.charAt (get_global $Alphabet) (get_local $i)) (get_local $c)) 
        (then
                    (return (get_local $i))
        ) 
    )
                        ;;For loop control step: (i32.const 1)
            (set_local $i (i32.add (get_local $i) (i32.const 1)))
            (br $loop_tmp9f9d05e98b3d366e)
                        ;;For Loop Next On loop_tmp9f9d05e98b3d366e

        )
    )
    
        (return (i32.const 0))
    )


    ;; functions in [base64.Program]


    ;; Public Function base64.Program.Main() As System.Void
    (func $base64.Program.Main  

        (local $rnd i32)
    (local $allPass i32)
    (local $len i32)
    (local $sb i32)
    (local $k i32)
    (local $text i32)
    (local $mine i32)
    (local $standard i32)

        (call $Console.WriteLine (i32.const 152))
        (call $base64.Program.ShowDemo (i32.const 184))
        (call $base64.Program.ShowDemo (i32.const 192))
        (call $base64.Program.ShowDemo (i32.const 200))
        (call $Console.WriteLine (i32.const 208))
        (call $base64.Program.ShowDemo (i32.const 248))
        (call $Console.WriteLine (i32.const 264))
        (call $base64.Program.ShowDemo (i32.const 296))
        (call $Console.WriteLine (i32.const 304))
        (set_local $rnd (call $Random.New (i32.const 2026)))
        (set_local $allPass (i32.const 1))
            
    ;; For len As Integer = 0 To 60
    (set_local $len (i32.const 0))
    
    (block $WASMClosure_br_e6ae2ed0b0d8f2df 
        (loop $loop_tmpb283c8a18b536cac

                        
    ;; (boolean) (boolean) (i32) (len = (i32.const 60))
                (br_if $WASMClosure_br_e6ae2ed0b0d8f2df (i32.eq (get_local $len) (i32.const 60)))
            (set_local $sb (call $StringBuilder.New ))
                
    ;; For k As Integer = 1 To len
    (set_local $k (i32.const 1))
    
    (block $WASMClosure_br_05a79de37f2c7004 
        (loop $loop_tmp1b07820bcdee34d2

                                
    ;; (boolean) (boolean) (i32) (k = len)
                        (br_if $WASMClosure_br_05a79de37f2c7004 (i32.eq (get_local $k) (get_local $len)))
                    (drop (call $StringBuilder.Append (get_local $sb) (call $Strings.ChrW (call $Random.Next (get_local $rnd) (i32.const 32) (i32.const 55295)))))
                                        ;;For loop control step: (i32.const 1)
                    (set_local $k (i32.add (get_local $k) (i32.const 1)))
                    (br $loop_tmp1b07820bcdee34d2)
                                        ;;For Loop Next On loop_tmp1b07820bcdee34d2

        )
    )
    
            (set_local $text (call $string.i32.toString (i32.const 0)))
            (set_local $mine (call $base64.Base64Codec.EncodeText (get_local $text)))
            (set_local $standard (call $Convert.ToBase64String (call $Encoding.UTF8.GetBytes (get_local $text))))
            
    (if (i32.add (i32.eqz (call $string.equals (get_local $mine) (get_local $standard))) (i32.eqz (call $string.equals (call $base64.Base64Codec.DecodeText (get_local $mine)) (get_local $text)))) 
        (then
                    (set_local $allPass (i32.const 0))
        (call $Console.WriteLine (call $string.add (call $string.add (i32.const 344) (call $string.i32.toString (get_local $len))) (i32.const 352)))
        ) 
    )
                        ;;For loop control step: (i32.const 1)
            (set_local $len (i32.add (get_local $len) (i32.const 1)))
            (br $loop_tmpb283c8a18b536cac)
                        ;;For Loop Next On loop_tmpb283c8a18b536cac

        )
    )
    
        (call $Console.WriteLine (call $string.add (i32.const 368) (call $string.i32.toString (i32.const 0))))
    )

    ;; Public Function base64.Program.ShowDemo(text As System.String) As System.Void
    (func $base64.Program.ShowDemo (param $text i32) 

        (local $encoded i32)
    (local $decoded i32)

        (set_local $encoded (call $base64.Base64Codec.EncodeText (get_local $text)))
        (set_local $decoded (call $base64.Base64Codec.DecodeText (get_local $encoded)))
        (call $Console.WriteLine (call $string.add (call $string.add (i32.const 400) (get_local $text)) (i32.const 416)))
        (call $Console.WriteLine (call $string.add (call $string.add (i32.const 424) (get_local $encoded)) (i32.const 440)))
        (call $Console.WriteLine (call $string.add (call $string.add (i32.const 448) (get_local $decoded)) (i32.const 464)))
        (call $Console.WriteLine (call $string.add (i32.const 472) (call $string.i32.toString (i32.const 0))))
        (call $Console.WriteLine (call $Strings.NewString (i32.const 480) (i32.const 46)))
    )




    
    ;; #region "VisualBasic Application Initialize Of Each Modules"
        ;; functions in [base64.Base64Codec]


    ;; Public Function base64.Base64Codec.SubNew() As System.Void
    (func $base64.Base64Codec.SubNew  

        

        (set_global $Alphabet (i32.const 13))
    )


;; functions in [base64.Program]


    ;; Public Function base64.Program.SubNew() As System.Void
    (func $base64.Program.SubNew  

        

        
    )


    ;; #endregion

    ;; --------------------------------------------------
    ;; Microsoft.VisualBasic.My.Application_Startup Event
    (func $MyApplication_Startup
        (call $base64.Base64Codec.SubNew )
(call $base64.Program.SubNew )
    )
    ;; --------------------------------------------------

    (start $MyApplication_Startup)
)
