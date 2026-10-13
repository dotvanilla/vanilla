(module ;; Microsoft VisualBasic Project 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 
    ;; build: 2026-10-13 17:17:15
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
    (func $dump (import "console" "dump") (param $obj i32) )
    (func $println (import "console" "println") (param $info i32) )
    (func $warning (import "console" "warning") (param $info i32) )
    (func $viewLine (import "console" "viewLine") (param $line i32) )
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
    (global $WATMemoryBufferSize (mut i32) (i32.const 760))

        
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
        
    ;; String from 0d000000 with 1 bytes in memory
    (data (i32.const 13) "#\00")
        
    ;; String from 10000000 with 13 bytes in memory
    (data (i32.const 16) " addressOf:=&\00")
        
    ;; String from 20000000 with 8 bytes in memory
    (data (i32.const 32) ", name:=\00")
        
    ;; String from 30000000 with 11 bytes in memory
    (data (i32.const 48) "   [x,y]:=[\00")
        
    ;; String from 40000000 with 1 bytes in memory
    (data (i32.const 64) ",\00")
        
    ;; String from 48000000 with 1 bytes in memory
    (data (i32.const 72) "]\00")
        
    ;; String from 50000000 with 98 bytes in memory
    (data (i32.const 80) "As the point and line object is reference type, so the point value in lines array is also changed!\00")
        
    ;; String from b8000000 with 24 bytes in memory
    (data (i32.const 184) "Assign a random [x,y]:=[\00")
        
    ;; String from d8000000 with 1 bytes in memory
    (data (i32.const 216) ",\00")
        
    ;; String from e0000000 with 1 bytes in memory
    (data (i32.const 224) "]\00")
    
    ;; AssemblyInfo.vb
        
    ;; String from e8000000 with 295 bytes in memory
    (data (i32.const 232) "d15:AssemblyCompany0:17:AssemblyCopyright0:19:AssemblyDescription0:19:AssemblyFileVersion0:16:AssemblyFullName0:28:AssemblyInformationalVersion0:15:AssemblyProduct0:13:AssemblyTitle0:17:AssemblyTrademark0:15:AssemblyVersion0:9:BuiltTime0:10:ComVisible5:False4:Guid0:4:Name0:15:TargetFramework0:e\00")

    (func $AssemblyInfo (result i32)
        (return (i32.const 232))    
    )

    (export "AssemblyInfo" (func $AssemblyInfo)) 

    ;; AssemblyInfo fields (consumed by the javascript runtime)
        
    ;; String from 10020000 with 0 bytes in memory
    (data (i32.const 528) "\00")
    (func $AssemblyInfo.AssemblyTitle (result i32)
        (return (i32.const 528))
    )
    (export "AssemblyInfo.AssemblyTitle" (func $AssemblyInfo.AssemblyTitle))
        
    ;; String from 18020000 with 0 bytes in memory
    (data (i32.const 536) "\00")
    (func $AssemblyInfo.AssemblyDescription (result i32)
        (return (i32.const 536))
    )
    (export "AssemblyInfo.AssemblyDescription" (func $AssemblyInfo.AssemblyDescription))
        
    ;; String from 20020000 with 0 bytes in memory
    (data (i32.const 544) "\00")
    (func $AssemblyInfo.AssemblyCompany (result i32)
        (return (i32.const 544))
    )
    (export "AssemblyInfo.AssemblyCompany" (func $AssemblyInfo.AssemblyCompany))
        
    ;; String from 28020000 with 0 bytes in memory
    (data (i32.const 552) "\00")
    (func $AssemblyInfo.AssemblyProduct (result i32)
        (return (i32.const 552))
    )
    (export "AssemblyInfo.AssemblyProduct" (func $AssemblyInfo.AssemblyProduct))
        
    ;; String from 30020000 with 0 bytes in memory
    (data (i32.const 560) "\00")
    (func $AssemblyInfo.AssemblyCopyright (result i32)
        (return (i32.const 560))
    )
    (export "AssemblyInfo.AssemblyCopyright" (func $AssemblyInfo.AssemblyCopyright))
        
    ;; String from 38020000 with 0 bytes in memory
    (data (i32.const 568) "\00")
    (func $AssemblyInfo.AssemblyTrademark (result i32)
        (return (i32.const 568))
    )
    (export "AssemblyInfo.AssemblyTrademark" (func $AssemblyInfo.AssemblyTrademark))
        
    ;; String from 40020000 with 0 bytes in memory
    (data (i32.const 576) "\00")
    (func $AssemblyInfo.AssemblyInformationalVersion (result i32)
        (return (i32.const 576))
    )
    (export "AssemblyInfo.AssemblyInformationalVersion" (func $AssemblyInfo.AssemblyInformationalVersion))
        
    ;; String from 48020000 with 0 bytes in memory
    (data (i32.const 584) "\00")
    (func $AssemblyInfo.TargetFramework (result i32)
        (return (i32.const 584))
    )
    (export "AssemblyInfo.TargetFramework" (func $AssemblyInfo.TargetFramework))
        
    ;; String from 50020000 with 5 bytes in memory
    (data (i32.const 592) "False\00")
    (func $AssemblyInfo.ComVisible (result i32)
        (return (i32.const 592))
    )
    (export "AssemblyInfo.ComVisible" (func $AssemblyInfo.ComVisible))
        
    ;; String from 58020000 with 0 bytes in memory
    (data (i32.const 600) "\00")
    (func $AssemblyInfo.Name (result i32)
        (return (i32.const 600))
    )
    (export "AssemblyInfo.Name" (func $AssemblyInfo.Name))
        
    ;; String from 60020000 with 0 bytes in memory
    (data (i32.const 608) "\00")
    (func $AssemblyInfo.Guid (result i32)
        (return (i32.const 608))
    )
    (export "AssemblyInfo.Guid" (func $AssemblyInfo.Guid))
        
    ;; String from 68020000 with 0 bytes in memory
    (data (i32.const 616) "\00")
    (func $AssemblyInfo.AssemblyVersion (result i32)
        (return (i32.const 616))
    )
    (export "AssemblyInfo.AssemblyVersion" (func $AssemblyInfo.AssemblyVersion))
        
    ;; String from 70020000 with 0 bytes in memory
    (data (i32.const 624) "\00")
    (func $AssemblyInfo.AssemblyFileVersion (result i32)
        (return (i32.const 624))
    )
    (export "AssemblyInfo.AssemblyFileVersion" (func $AssemblyInfo.AssemblyFileVersion))
        
    ;; String from 78020000 with 0 bytes in memory
    (data (i32.const 632) "\00")
    (func $AssemblyInfo.AssemblyFullName (result i32)
        (return (i32.const 632))
    )
    (export "AssemblyInfo.AssemblyFullName" (func $AssemblyInfo.AssemblyFullName))
        
    ;; String from 80020000 with 0 bytes in memory
    (data (i32.const 640) "\00")
    (func $AssemblyInfo.BuiltTime (result i32)
        (return (i32.const 640))
    )
    (export "AssemblyInfo.BuiltTime" (func $AssemblyInfo.BuiltTime))

    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
        
    ;; String from 88020000 with 55 bytes in memory
    (data (i32.const 648) "d9:ExportApile4:Name5:Point9:Namespace13:.arrayObjectse\00")
        
    ;; String from c0020000 with 54 bytes in memory
    (data (i32.const 704) "d9:ExportApile4:Name4:line9:Namespace13:.arrayObjectse\00")

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
    (global $ps (mut i32) (i32.const 0))
    (global $lines (mut i32) (i32.const 0))
    (global $ObjectManager.HeapSize (mut i32) (i32.const 760))

    ;; Export methods of this module
    (export "objectArrayTest.printArray" (func $.objectArrayTest.printArray))
    (export "objectArrayTest.GetPoints" (func $.objectArrayTest.GetPoints)) 

    ;; functions in [.objectArrayTest]


    ;; Public Function .objectArrayTest.printArray() As System.Void
    (func $.objectArrayTest.printArray  

        (local $i i32)
    (local $j i32)

        (call $.objectArrayTest.modifyArray )
            
    ;; For i As Integer = 0 To ps.Length - 1
    (set_local $i (i32.const 0))
    
    (block $WASMClosure_br_97b66793bedc3c0b 
        (loop $loop_tmp85c33b179034ad20

                        
    ;; (boolean) (boolean) (i32) (i > (i32) (VanillaBasic.WebAssembly.Syntax.FunctionInvoke - (i32.const 1)))
                (br_if $WASMClosure_br_97b66793bedc3c0b (i32.gt_s (get_local $i) (i32.sub (call $Array.length (get_global $ps)) (i32.const 1))))
            (drop (i32.const 0))
            (drop (i32.const 0))
            (drop (i32.const 0))
                        ;;For loop control step: (i32.const 1)
            (set_local $i (i32.add (get_local $i) (i32.const 1)))
            (br $loop_tmp85c33b179034ad20)
                        ;;For Loop Next On loop_tmp85c33b179034ad20

        )
    )
    
        (drop (i32.const 0))
            
    ;; For j As Integer = 0 To lines.Length - 1
    (set_local $j (i32.const 0))
    
    (block $WASMClosure_br_80cf53eeebcf4c57 
        (loop $loop_tmpacc1607b6435b484

                        
    ;; (boolean) (boolean) (i32) (j > (i32) (VanillaBasic.WebAssembly.Syntax.FunctionInvoke - (i32.const 1)))
                (br_if $WASMClosure_br_80cf53eeebcf4c57 (i32.gt_s (get_local $j) (i32.sub (call $Array.length (get_global $lines)) (i32.const 1))))
            (drop (i32.const 0))
                        ;;For loop control step: (i32.const 1)
            (set_local $j (i32.add (get_local $j) (i32.const 1)))
            (br $loop_tmpacc1607b6435b484)
                        ;;For Loop Next On loop_tmpacc1607b6435b484

        )
    )
    
    )

    ;; Public Function .objectArrayTest.modifyArray() As System.Void
    (func $.objectArrayTest.modifyArray  

        (local $i i32)

            
    ;; For i As Integer = 0 To ps.Length - 1
    (set_local $i (i32.const 0))
    
    (block $WASMClosure_br_d284f0678ef2c997 
        (loop $loop_tmp55a9052c11831769

                        
    ;; (boolean) (boolean) (i32) (i > (i32) (VanillaBasic.WebAssembly.Syntax.FunctionInvoke - (i32.const 1)))
                (br_if $WASMClosure_br_d284f0678ef2c997 (i32.gt_s (get_local $i) (i32.sub (call $Array.length (get_global $ps)) (i32.const 1))))
            (drop (i32.const 0))
            (drop (i32.const 0))
            (drop (i32.const 0))
                        ;;For loop control step: (i32.const 1)
            (set_local $i (i32.add (get_local $i) (i32.const 1)))
            (br $loop_tmp55a9052c11831769)
                        ;;For Loop Next On loop_tmp55a9052c11831769

        )
    )
    
    )

    ;; Public Function .objectArrayTest.GetPoints() As System.Int32
    (func $.objectArrayTest.GetPoints  (result i32)

        

        (return (get_global $ps))
    )




    
    ;; #region "VisualBasic Application Initialize Of Each Modules"
        ;; functions in [.objectArrayTest]


    ;; Public Function .objectArrayTest.SubNew() As System.Void
    (func $.objectArrayTest.SubNew  

        

        (set_global $ps (i32.const 0))
    )


;; functions in [.arrayObjects.Point]


    ;; Public Function .arrayObjects.Point.SubNew() As System.Void
    (func $.arrayObjects.Point.SubNew  

        

        
    )


;; functions in [.arrayObjects.line]


    ;; Public Function .arrayObjects.line.SubNew() As System.Void
    (func $.arrayObjects.line.SubNew  

        

        
    )


    ;; #endregion

    ;; --------------------------------------------------
    ;; Microsoft.VisualBasic.My.Application_Startup Event
    (func $MyApplication_Startup
        (call $.objectArrayTest.SubNew )
(call $.arrayObjects.Point.SubNew )
(call $.arrayObjects.line.SubNew )
    )
    ;; --------------------------------------------------

    (start $MyApplication_Startup)
)
