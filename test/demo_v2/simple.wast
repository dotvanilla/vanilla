(module ;; Microsoft VisualBasic Project 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 
    ;; build: 2026-10-13 11:57:34
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    
    ;; Javascript function imports into current WebAssembly Module
    ;;
    ;; ----- NOTE: imports must occur before all non-import definitions ------

    ;; JAVASCRIPT [Math]
    (func $Math.Pow (import "Math" "pow") (param $base f64) (param $exponent f64) (result f64))
    ;; JAVASCRIPT [console]
    (func $Console.WriteLine (import "console" "log") (param $msg i32) )
    
    ;; ----- END OF JAVASCRIPT IMPORTS -----    

    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    
    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $WATMemoryBufferSize (mut i32) (i32.const 448))

        
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
        
    ;; String from 0d000000 with 12 bytes in memory
    (data (i32.const 13) "Hello World!\00")
    
    ;; AssemblyInfo.vb
        
    ;; String from 20000000 with 295 bytes in memory
    (data (i32.const 32) "d15:AssemblyCompany0:17:AssemblyCopyright0:19:AssemblyDescription0:19:AssemblyFileVersion0:16:AssemblyFullName0:28:AssemblyInformationalVersion0:15:AssemblyProduct0:13:AssemblyTitle0:17:AssemblyTrademark0:15:AssemblyVersion0:9:BuiltTime0:10:ComVisible5:False4:Guid0:4:Name0:15:TargetFramework0:e\00")

    (func $AssemblyInfo (result i32)
        (return (i32.const 32))    
    )

    (export "AssemblyInfo" (func $AssemblyInfo)) 

    ;; AssemblyInfo fields (consumed by the javascript runtime)
        
    ;; String from 48010000 with 0 bytes in memory
    (data (i32.const 328) "\00")
    (func $AssemblyInfo.AssemblyTitle (result i32)
        (return (i32.const 328))
    )
    (export "AssemblyInfo.AssemblyTitle" (func $AssemblyInfo.AssemblyTitle))
        
    ;; String from 50010000 with 0 bytes in memory
    (data (i32.const 336) "\00")
    (func $AssemblyInfo.AssemblyDescription (result i32)
        (return (i32.const 336))
    )
    (export "AssemblyInfo.AssemblyDescription" (func $AssemblyInfo.AssemblyDescription))
        
    ;; String from 58010000 with 0 bytes in memory
    (data (i32.const 344) "\00")
    (func $AssemblyInfo.AssemblyCompany (result i32)
        (return (i32.const 344))
    )
    (export "AssemblyInfo.AssemblyCompany" (func $AssemblyInfo.AssemblyCompany))
        
    ;; String from 60010000 with 0 bytes in memory
    (data (i32.const 352) "\00")
    (func $AssemblyInfo.AssemblyProduct (result i32)
        (return (i32.const 352))
    )
    (export "AssemblyInfo.AssemblyProduct" (func $AssemblyInfo.AssemblyProduct))
        
    ;; String from 68010000 with 0 bytes in memory
    (data (i32.const 360) "\00")
    (func $AssemblyInfo.AssemblyCopyright (result i32)
        (return (i32.const 360))
    )
    (export "AssemblyInfo.AssemblyCopyright" (func $AssemblyInfo.AssemblyCopyright))
        
    ;; String from 70010000 with 0 bytes in memory
    (data (i32.const 368) "\00")
    (func $AssemblyInfo.AssemblyTrademark (result i32)
        (return (i32.const 368))
    )
    (export "AssemblyInfo.AssemblyTrademark" (func $AssemblyInfo.AssemblyTrademark))
        
    ;; String from 78010000 with 0 bytes in memory
    (data (i32.const 376) "\00")
    (func $AssemblyInfo.AssemblyInformationalVersion (result i32)
        (return (i32.const 376))
    )
    (export "AssemblyInfo.AssemblyInformationalVersion" (func $AssemblyInfo.AssemblyInformationalVersion))
        
    ;; String from 80010000 with 0 bytes in memory
    (data (i32.const 384) "\00")
    (func $AssemblyInfo.TargetFramework (result i32)
        (return (i32.const 384))
    )
    (export "AssemblyInfo.TargetFramework" (func $AssemblyInfo.TargetFramework))
        
    ;; String from 88010000 with 5 bytes in memory
    (data (i32.const 392) "False\00")
    (func $AssemblyInfo.ComVisible (result i32)
        (return (i32.const 392))
    )
    (export "AssemblyInfo.ComVisible" (func $AssemblyInfo.ComVisible))
        
    ;; String from 90010000 with 0 bytes in memory
    (data (i32.const 400) "\00")
    (func $AssemblyInfo.Name (result i32)
        (return (i32.const 400))
    )
    (export "AssemblyInfo.Name" (func $AssemblyInfo.Name))
        
    ;; String from 98010000 with 0 bytes in memory
    (data (i32.const 408) "\00")
    (func $AssemblyInfo.Guid (result i32)
        (return (i32.const 408))
    )
    (export "AssemblyInfo.Guid" (func $AssemblyInfo.Guid))
        
    ;; String from a0010000 with 0 bytes in memory
    (data (i32.const 416) "\00")
    (func $AssemblyInfo.AssemblyVersion (result i32)
        (return (i32.const 416))
    )
    (export "AssemblyInfo.AssemblyVersion" (func $AssemblyInfo.AssemblyVersion))
        
    ;; String from a8010000 with 0 bytes in memory
    (data (i32.const 424) "\00")
    (func $AssemblyInfo.AssemblyFileVersion (result i32)
        (return (i32.const 424))
    )
    (export "AssemblyInfo.AssemblyFileVersion" (func $AssemblyInfo.AssemblyFileVersion))
        
    ;; String from b0010000 with 0 bytes in memory
    (data (i32.const 432) "\00")
    (func $AssemblyInfo.AssemblyFullName (result i32)
        (return (i32.const 432))
    )
    (export "AssemblyInfo.AssemblyFullName" (func $AssemblyInfo.AssemblyFullName))
        
    ;; String from b8010000 with 0 bytes in memory
    (data (i32.const 440) "\00")
    (func $AssemblyInfo.BuiltTime (result i32)
        (return (i32.const 440))
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
    (global $ObjectManager.HeapSize (mut i32) (i32.const 448))

    ;; Export methods of this module
    (export "DEMO.Main" (func $.DEMO.Main))
    (export "DEMO.fact" (func $.DEMO.fact))
    (export "DEMO.forLoop" (func $.DEMO.forLoop)) 

    ;; functions in [.DEMO]


    ;; Public Function .DEMO.Main() As System.Int32
    (func $.DEMO.Main  (result i32)

        

        (call $Console.WriteLine (i32.const 13))
        (return (i32.const 0))
    )

    ;; Public Function .DEMO.fact(n As System.Int32) As System.Int32
    (func $.DEMO.fact (param $n i32) (result i32)

        

        
    (if (i32.add (i32.eq (get_local $n) (i32.const 0)) (i32.eq (get_local $n) (i32.const 1))) 
        (then
                    (return (i32.const 1))
        ) (else
                    (return (i32.mul (get_local $n) (call $.DEMO.fact (i32.sub (get_local $n) (i32.const 1)))))
    )
    )
        (return (i32.const 0))
    )

    ;; Public Function .DEMO.forLoop() As System.Int32
    (func $.DEMO.forLoop  (result i32)

        (local $i i32)

            
    ;; For i As Integer = 0 To 10 Step 1
    (set_local $i (i32.const 0))
    
    (block $WASMClosure_br_0ec3b524f5e3c52f 
        (loop $loop_tmpa4e5dbc5e11847da

                        
    ;; (boolean) (boolean) (i32) (i > (i32.const 10))
                (br_if $WASMClosure_br_0ec3b524f5e3c52f (i32.gt_s (get_local $i) (i32.const 10)))
            (return (get_local $i))
                        ;;For loop control step: (i32.const 1)
            (set_local $i (i32.add (get_local $i) (i32.const 1)))
            (br $loop_tmpa4e5dbc5e11847da)
                        ;;For Loop Next On loop_tmpa4e5dbc5e11847da

        )
    )
    
        (return (i32.const -1))
    )




    
    ;; #region "VisualBasic Application Initialize Of Each Modules"
        ;; functions in [.DEMO]


    ;; Public Function .DEMO.SubNew() As System.Void
    (func $.DEMO.SubNew  

        

        
    )


    ;; #endregion

    ;; --------------------------------------------------
    ;; Microsoft.VisualBasic.My.Application_Startup Event
    (func $MyApplication_Startup
        (call $.DEMO.SubNew )
    )
    ;; --------------------------------------------------

    (start $MyApplication_Startup)
)
