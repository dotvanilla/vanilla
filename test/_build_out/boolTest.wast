(module ;; Microsoft VisualBasic Project 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 
    ;; build: 2026-10-13 17:20:20
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
    (func $Random (import "Math" "Random")  (result f64))
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
    
    ;; ----- END OF JAVASCRIPT IMPORTS -----    

    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    
    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $WATMemoryBufferSize (mut i32) (i32.const 432))

        
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
    
    
    ;; AssemblyInfo.vb
        
    ;; String from 0d000000 with 295 bytes in memory
    (data (i32.const 13) "d15:AssemblyCompany0:17:AssemblyCopyright0:19:AssemblyDescription0:19:AssemblyFileVersion0:16:AssemblyFullName0:28:AssemblyInformationalVersion0:15:AssemblyProduct0:13:AssemblyTitle0:17:AssemblyTrademark0:15:AssemblyVersion0:9:BuiltTime0:10:ComVisible5:False4:Guid0:4:Name0:15:TargetFramework0:e\00")

    (func $AssemblyInfo (result i32)
        (return (i32.const 13))    
    )

    (export "AssemblyInfo" (func $AssemblyInfo)) 

    ;; AssemblyInfo fields (consumed by the javascript runtime)
        
    ;; String from 38010000 with 0 bytes in memory
    (data (i32.const 312) "\00")
    (func $AssemblyInfo.AssemblyTitle (result i32)
        (return (i32.const 312))
    )
    (export "AssemblyInfo.AssemblyTitle" (func $AssemblyInfo.AssemblyTitle))
        
    ;; String from 40010000 with 0 bytes in memory
    (data (i32.const 320) "\00")
    (func $AssemblyInfo.AssemblyDescription (result i32)
        (return (i32.const 320))
    )
    (export "AssemblyInfo.AssemblyDescription" (func $AssemblyInfo.AssemblyDescription))
        
    ;; String from 48010000 with 0 bytes in memory
    (data (i32.const 328) "\00")
    (func $AssemblyInfo.AssemblyCompany (result i32)
        (return (i32.const 328))
    )
    (export "AssemblyInfo.AssemblyCompany" (func $AssemblyInfo.AssemblyCompany))
        
    ;; String from 50010000 with 0 bytes in memory
    (data (i32.const 336) "\00")
    (func $AssemblyInfo.AssemblyProduct (result i32)
        (return (i32.const 336))
    )
    (export "AssemblyInfo.AssemblyProduct" (func $AssemblyInfo.AssemblyProduct))
        
    ;; String from 58010000 with 0 bytes in memory
    (data (i32.const 344) "\00")
    (func $AssemblyInfo.AssemblyCopyright (result i32)
        (return (i32.const 344))
    )
    (export "AssemblyInfo.AssemblyCopyright" (func $AssemblyInfo.AssemblyCopyright))
        
    ;; String from 60010000 with 0 bytes in memory
    (data (i32.const 352) "\00")
    (func $AssemblyInfo.AssemblyTrademark (result i32)
        (return (i32.const 352))
    )
    (export "AssemblyInfo.AssemblyTrademark" (func $AssemblyInfo.AssemblyTrademark))
        
    ;; String from 68010000 with 0 bytes in memory
    (data (i32.const 360) "\00")
    (func $AssemblyInfo.AssemblyInformationalVersion (result i32)
        (return (i32.const 360))
    )
    (export "AssemblyInfo.AssemblyInformationalVersion" (func $AssemblyInfo.AssemblyInformationalVersion))
        
    ;; String from 70010000 with 0 bytes in memory
    (data (i32.const 368) "\00")
    (func $AssemblyInfo.TargetFramework (result i32)
        (return (i32.const 368))
    )
    (export "AssemblyInfo.TargetFramework" (func $AssemblyInfo.TargetFramework))
        
    ;; String from 78010000 with 5 bytes in memory
    (data (i32.const 376) "False\00")
    (func $AssemblyInfo.ComVisible (result i32)
        (return (i32.const 376))
    )
    (export "AssemblyInfo.ComVisible" (func $AssemblyInfo.ComVisible))
        
    ;; String from 80010000 with 0 bytes in memory
    (data (i32.const 384) "\00")
    (func $AssemblyInfo.Name (result i32)
        (return (i32.const 384))
    )
    (export "AssemblyInfo.Name" (func $AssemblyInfo.Name))
        
    ;; String from 88010000 with 0 bytes in memory
    (data (i32.const 392) "\00")
    (func $AssemblyInfo.Guid (result i32)
        (return (i32.const 392))
    )
    (export "AssemblyInfo.Guid" (func $AssemblyInfo.Guid))
        
    ;; String from 90010000 with 0 bytes in memory
    (data (i32.const 400) "\00")
    (func $AssemblyInfo.AssemblyVersion (result i32)
        (return (i32.const 400))
    )
    (export "AssemblyInfo.AssemblyVersion" (func $AssemblyInfo.AssemblyVersion))
        
    ;; String from 98010000 with 0 bytes in memory
    (data (i32.const 408) "\00")
    (func $AssemblyInfo.AssemblyFileVersion (result i32)
        (return (i32.const 408))
    )
    (export "AssemblyInfo.AssemblyFileVersion" (func $AssemblyInfo.AssemblyFileVersion))
        
    ;; String from a0010000 with 0 bytes in memory
    (data (i32.const 416) "\00")
    (func $AssemblyInfo.AssemblyFullName (result i32)
        (return (i32.const 416))
    )
    (export "AssemblyInfo.AssemblyFullName" (func $AssemblyInfo.AssemblyFullName))
        
    ;; String from a8010000 with 0 bytes in memory
    (data (i32.const 424) "\00")
    (func $AssemblyInfo.BuiltTime (result i32)
        (return (i32.const 424))
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
    (global $b2 (mut i32) (i32.const 0))
    (global $threshold (mut f32) (f32.const 0))
    (global $ObjectManager.HeapSize (mut i32) (i32.const 432))

    ;; Export methods of this module
    (export "boolTest.logical" (func $.boolTest.logical)) 

    ;; functions in [.boolTest]


    ;; Public Function .boolTest.logical() As System.Int32
    (func $.boolTest.logical  (result i32)

        (local $b i32)

        (set_local $b (f64.ge (f64.promote/f32 (f32.convert_s/i32 (i32.const 0))) (f64.add (f64.promote/f32 (get_global $threshold)) (f64.const 0.1))))
        
    (if (i32.and (get_local $b) (get_global $b2)) 
        (then
                    (return (i32.trunc_s/f64 (f64.const 1.5)))
        ) (else
                    (return (i32.const -100))
    )
    )
        (return (i32.const 0))
    )




    
    ;; #region "VisualBasic Application Initialize Of Each Modules"
        ;; functions in [.boolTest]


    ;; Public Function .boolTest.SubNew() As System.Void
    (func $.boolTest.SubNew  

        

        (set_global $b2 (i32.const 1))
        (set_global $threshold (f32.const 0.5))
    )


    ;; #endregion

    ;; --------------------------------------------------
    ;; Microsoft.VisualBasic.My.Application_Startup Event
    (func $MyApplication_Startup
        (call $.boolTest.SubNew )
    )
    ;; --------------------------------------------------

    (start $MyApplication_Startup)
)
