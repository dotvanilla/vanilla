(module ;; Microsoft VisualBasic Project 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 
    ;; build: 2026-10-13 17:20:44
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
    (func $print (import "console" "print") (param $message i32) (result i32))
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
    (func $string.bool.toString (import "string" "BoolToString") (param $x i32) (result i32))
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
    ;; JAVASCRIPT [document]
    (func $DOMbyId (import "document" "DOMbyId") (param $id i32) (result i32))
    (func $setAttr (import "document" "setAttr") (param $node i32) (param $name i32) (param $value i32) (result i32))
    
    ;; ----- END OF JAVASCRIPT IMPORTS -----    

    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    
    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $WATMemoryBufferSize (mut i32) (i32.const 632))

        
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
        
    ;; String from 0d000000 with 9 bytes in memory
    (data (i32.const 13) "Nothing 1\00")
        
    ;; String from 18000000 with 49 bytes in memory
    (data (i32.const 24) "This message should never display on the console!\00")
        
    ;; String from 50000000 with 9 bytes in memory
    (data (i32.const 80) "Nothing 2\00")
        
    ;; String from 60000000 with 57 bytes in memory
    (data (i32.const 96) "This message is also should never display on the console!\00")
        
    ;; String from a0000000 with 7 bytes in memory
    (data (i32.const 160) "Nothing\00")
        
    ;; String from a8000000 with 4 bytes in memory
    (data (i32.const 168) "test\00")
        
    ;; String from b0000000 with 1 bytes in memory
    (data (i32.const 176) "a\00")
        
    ;; String from b8000000 with 15 bytes in memory
    (data (i32.const 184) " is not nothing\00")
        
    ;; String from c8000000 with 1 bytes in memory
    (data (i32.const 200) "a\00")
        
    ;; String from d0000000 with 1 bytes in memory
    (data (i32.const 208) "b\00")
    
    ;; AssemblyInfo.vb
        
    ;; String from d8000000 with 295 bytes in memory
    (data (i32.const 216) "d15:AssemblyCompany0:17:AssemblyCopyright0:19:AssemblyDescription0:19:AssemblyFileVersion0:16:AssemblyFullName0:28:AssemblyInformationalVersion0:15:AssemblyProduct0:13:AssemblyTitle0:17:AssemblyTrademark0:15:AssemblyVersion0:9:BuiltTime0:10:ComVisible5:False4:Guid0:4:Name0:15:TargetFramework0:e\00")

    (func $AssemblyInfo (result i32)
        (return (i32.const 216))    
    )

    (export "AssemblyInfo" (func $AssemblyInfo)) 

    ;; AssemblyInfo fields (consumed by the javascript runtime)
        
    ;; String from 00020000 with 0 bytes in memory
    (data (i32.const 512) "\00")
    (func $AssemblyInfo.AssemblyTitle (result i32)
        (return (i32.const 512))
    )
    (export "AssemblyInfo.AssemblyTitle" (func $AssemblyInfo.AssemblyTitle))
        
    ;; String from 08020000 with 0 bytes in memory
    (data (i32.const 520) "\00")
    (func $AssemblyInfo.AssemblyDescription (result i32)
        (return (i32.const 520))
    )
    (export "AssemblyInfo.AssemblyDescription" (func $AssemblyInfo.AssemblyDescription))
        
    ;; String from 10020000 with 0 bytes in memory
    (data (i32.const 528) "\00")
    (func $AssemblyInfo.AssemblyCompany (result i32)
        (return (i32.const 528))
    )
    (export "AssemblyInfo.AssemblyCompany" (func $AssemblyInfo.AssemblyCompany))
        
    ;; String from 18020000 with 0 bytes in memory
    (data (i32.const 536) "\00")
    (func $AssemblyInfo.AssemblyProduct (result i32)
        (return (i32.const 536))
    )
    (export "AssemblyInfo.AssemblyProduct" (func $AssemblyInfo.AssemblyProduct))
        
    ;; String from 20020000 with 0 bytes in memory
    (data (i32.const 544) "\00")
    (func $AssemblyInfo.AssemblyCopyright (result i32)
        (return (i32.const 544))
    )
    (export "AssemblyInfo.AssemblyCopyright" (func $AssemblyInfo.AssemblyCopyright))
        
    ;; String from 28020000 with 0 bytes in memory
    (data (i32.const 552) "\00")
    (func $AssemblyInfo.AssemblyTrademark (result i32)
        (return (i32.const 552))
    )
    (export "AssemblyInfo.AssemblyTrademark" (func $AssemblyInfo.AssemblyTrademark))
        
    ;; String from 30020000 with 0 bytes in memory
    (data (i32.const 560) "\00")
    (func $AssemblyInfo.AssemblyInformationalVersion (result i32)
        (return (i32.const 560))
    )
    (export "AssemblyInfo.AssemblyInformationalVersion" (func $AssemblyInfo.AssemblyInformationalVersion))
        
    ;; String from 38020000 with 0 bytes in memory
    (data (i32.const 568) "\00")
    (func $AssemblyInfo.TargetFramework (result i32)
        (return (i32.const 568))
    )
    (export "AssemblyInfo.TargetFramework" (func $AssemblyInfo.TargetFramework))
        
    ;; String from 40020000 with 5 bytes in memory
    (data (i32.const 576) "False\00")
    (func $AssemblyInfo.ComVisible (result i32)
        (return (i32.const 576))
    )
    (export "AssemblyInfo.ComVisible" (func $AssemblyInfo.ComVisible))
        
    ;; String from 48020000 with 0 bytes in memory
    (data (i32.const 584) "\00")
    (func $AssemblyInfo.Name (result i32)
        (return (i32.const 584))
    )
    (export "AssemblyInfo.Name" (func $AssemblyInfo.Name))
        
    ;; String from 50020000 with 0 bytes in memory
    (data (i32.const 592) "\00")
    (func $AssemblyInfo.Guid (result i32)
        (return (i32.const 592))
    )
    (export "AssemblyInfo.Guid" (func $AssemblyInfo.Guid))
        
    ;; String from 58020000 with 0 bytes in memory
    (data (i32.const 600) "\00")
    (func $AssemblyInfo.AssemblyVersion (result i32)
        (return (i32.const 600))
    )
    (export "AssemblyInfo.AssemblyVersion" (func $AssemblyInfo.AssemblyVersion))
        
    ;; String from 60020000 with 0 bytes in memory
    (data (i32.const 608) "\00")
    (func $AssemblyInfo.AssemblyFileVersion (result i32)
        (return (i32.const 608))
    )
    (export "AssemblyInfo.AssemblyFileVersion" (func $AssemblyInfo.AssemblyFileVersion))
        
    ;; String from 68020000 with 0 bytes in memory
    (data (i32.const 616) "\00")
    (func $AssemblyInfo.AssemblyFullName (result i32)
        (return (i32.const 616))
    )
    (export "AssemblyInfo.AssemblyFullName" (func $AssemblyInfo.AssemblyFullName))
        
    ;; String from 70020000 with 0 bytes in memory
    (data (i32.const 624) "\00")
    (func $AssemblyInfo.BuiltTime (result i32)
        (return (i32.const 624))
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
    (global $nullString (mut i32) (i32.const 0))
    (global $ObjectManager.HeapSize (mut i32) (i32.const 632))

    ;; Export methods of this module
    (export "nullreferenceTest.isNothing" (func $.nullreferenceTest.isNothing))
    (export "nullreferenceTest.noReturns" (func $.nullreferenceTest.noReturns))
    (export "nullreferenceTest.test" (func $.nullreferenceTest.test)) 

    ;; functions in [.nullreferenceTest]


    ;; Public Function .nullreferenceTest.isNothing() As System.Void
    (func $.nullreferenceTest.isNothing  

        

        
    (if (i32.eq (get_global $nullString) (i32.const 0)) 
        (then
                    (drop (i32.const 0))
        ) (else
                    (drop (i32.const 0))
            (drop (i32.const 0))
    )
    )
        
    (if (i32.eq (i32.const 0) (get_global $nullString)) 
        (then
                    (drop (i32.const 0))
        ) (else
                    (drop (i32.const 0))
            (drop (i32.const 0))
    )
    )
    )

    ;; Public Function .nullreferenceTest.noReturns() As System.Void
    (func $.nullreferenceTest.noReturns  

        

        (drop (i32.const 0))
        (drop (i32.const 0))
    )

    ;; Public Function .nullreferenceTest.test() As System.Int32
    (func $.nullreferenceTest.test  (result i32)

        (local $node i32)

        (set_local $node (i32.const 0))
        (drop (i32.const 0))
        (drop (i32.const 0))
        (return (i32.const 0))
    )




    
    ;; #region "VisualBasic Application Initialize Of Each Modules"
        ;; functions in [.nullreferenceTest]


    ;; Public Function .nullreferenceTest.SubNew() As System.Void
    (func $.nullreferenceTest.SubNew  

        

        (set_global $nullString (i32.const 0))
    )


    ;; #endregion

    ;; --------------------------------------------------
    ;; Microsoft.VisualBasic.My.Application_Startup Event
    (func $MyApplication_Startup
        (call $.nullreferenceTest.SubNew )
    )
    ;; --------------------------------------------------

    (start $MyApplication_Startup)
)
