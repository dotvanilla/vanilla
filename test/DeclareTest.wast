(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 4/27/2019 4:12:47 PM

    ;; imports must occur before all non-import definitions

    
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    
    (global $MN (mut i64) (i64.const -99))

(global $E (mut i32) (i32.const 0))

(global $F (mut i64) (i64.const 0))

(global $L (mut f32) (f32.const 90))

(global $A (mut f64) (f64.const 0))

(global $B (mut f64) (f64.const 0))

(global $C (mut f64) (f64.const 0))

(global $GG (mut f32) (f32.const 0))

(global $Z (mut i64) (i64.const 0))

     

    ;; functions in [DeclareTest]
    
    (func $localDeclareTest  (result f32)
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
    (set_local $XYY (f32.const 888999))
    (set_local $MN2 (i64.extend_s/i32 (i32.sub (i32.const 0) (i32.const 99))))
    (set_local $L (f32.convert_s/i32 (i32.const 90)))
    (return (f32.demote/f64 (f64.mul (f64.mul (f64.div (f64.mul (f64.add (f64.add (f64.add (f64.add (f64.div (f64.convert_s/i64 (get_global $MN)) (f64.convert_s/i64 (get_local $MN2))) (f64.promote/f32 (get_local $L))) (get_local $A)) (get_local $B)) (get_local $C)) (f64.promote/f32 (get_local $GG))) (f64.convert_s/i64 (get_local $Z))) (f64.convert_s/i32 (get_local $E))) (f64.convert_s/i64 (get_local $F)))))
    )
    )