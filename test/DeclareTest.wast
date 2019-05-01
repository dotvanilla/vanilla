(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/1/2019 12:18:01 PM
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    
    (global $DeclareTest.MN (mut i64) (i64.const -99))

(global $DeclareTest.E (mut i32) (i32.const 0))

(global $DeclareTest.F (mut i64) (i64.const 0))

(global $DeclareTest.L (mut f32) (f32.const 90))

(global $DeclareTest.A (mut f64) (f64.const 0))

(global $DeclareTest.B (mut f64) (f64.const 0))

(global $DeclareTest.C (mut f64) (f64.const 0))

(global $DeclareTest.GG (mut f32) (f32.const 0))

(global $DeclareTest.Z (mut i64) (i64.const 0))

     

    ;; functions in [DeclareTest]
    
    (func $DeclareTest.localDeclareTest  (result f32)
        ;; Public Function localDeclareTest() As f32
        (local $XYY f32)
    (local $MN2 i64)
    (local $L f32)
    (local $A f64)
    (local $B f64)
    (local $C f64)
    (local $GG f32)
    (local $Z i64)
    (local $E i32)
    (local $F i64)
    (local $globalNameRefere i32)
    (set_local $XYY (f32.const 888999))
    (set_local $MN2 (i64.extend_s/i32 (i32.sub (i32.const 0) (i32.const 99))))
    (set_local $L (f32.const 90))
    (set_local $globalNameRefere (i64.eq (i64.add (get_global $DeclareTest.MN) (get_global $DeclareTest.MN)) (i64.mul (get_global $DeclareTest.MN) (i64.const 2))))
    
    (if (i32.eqz (get_local $globalNameRefere)) 
        (then
                    (return (f32.convert_s/i32 (i32.sub (i32.const 0) (i32.const 100))))
        ) 
    )
    (return (f32.demote/f64 (f64.mul (f64.mul (f64.div (f64.mul (f64.add (f64.add (f64.add (f64.add (f64.div (f64.convert_s/i64 (get_global $DeclareTest.MN)) (f64.convert_s/i64 (get_local $MN2))) (f64.promote/f32 (get_local $L))) (get_local $A)) (get_local $B)) (get_local $C)) (f64.promote/f32 (get_local $GG))) (f64.convert_s/i64 (get_local $Z))) (f64.convert_s/i32 (get_local $E))) (f64.convert_s/i64 (get_local $F)))))
    )
    )