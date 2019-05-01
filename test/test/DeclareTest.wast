(module ;; Module 

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: 1.3.0.22
    ;; build: 5/1/2019 12:42:55 PM
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
    ;; Declare Function f64.toString Lib "string" Alias "toString" (x As f64) As string
    (func $f64.toString (import "string" "toString") (param $x f64) (result i32))
    
    ;; Only allows one memory block in each module
    (memory (import "env" "bytechunks") 1)

    ;; Memory data for string constant
    
    ;; String from 1 with 7 bytes in memory
    (data (i32.const 1) "Hello: \00")
    
    (global $DeclareTest.MN (mut i64) (i64.const -99))

(global $DeclareTest.E (mut i32) (i32.const 0))

(global $DeclareTest.F (mut i64) (i64.const 0))

(global $DeclareTest.L (mut f32) (f32.const 90))

(global $DeclareTest.A (mut f64) (f64.const 0))

(global $DeclareTest.B (mut f64) (f64.const 0))

(global $DeclareTest.C (mut f64) (f64.const 0))

(global $DeclareTest.GG (mut f32) (f32.const 0))

(global $DeclareTest.Z (mut i64) (i64.const 0))

(global $DeclareTest.uniqueGlobalName (mut i32) (i32.const 0))

     

    ;; functions in [DeclareTest]
    
    (func $DeclareTest.localDeclareTest  (result f32)
        ;; Public Function localDeclareTest() As f32
        (local $XYY f64)
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
    (set_local $XYY (f64.add (f64.const 888999) (get_global $DeclareTest.A)))
    (set_local $MN2 (i64.extend_s/i32 (i32.sub (i32.const 0) (i32.const 99))))
    (set_local $L (f32.const 90))
    (set_local $GG (get_global $DeclareTest.GG))
    (set_local $Z (i64.mul (get_global $DeclareTest.Z) (i64.const 99)))
    (set_local $C (f64.const 5000))
    (set_global $DeclareTest.C (f64.mul (get_local $C) (f64.add (get_global $DeclareTest.C) (f64.const 1))))
    (set_global $DeclareTest.uniqueGlobalName (call $string.add (i32.const 1) (call $f64.toString (get_local $C))))
    (set_local $globalNameRefere (i64.eq (i64.add (get_global $DeclareTest.MN) (get_global $DeclareTest.MN)) (i64.mul (get_global $DeclareTest.MN) (i64.const 2))))
    
    (if (i32.eqz (get_local $globalNameRefere)) 
        (then
                    (return (f32.convert_s/i32 (i32.sub (i32.const 0) (i32.const 100))))
        ) 
    )
    (return (f32.demote/f64 (f64.div (f64.mul (f64.mul (f64.div (f64.mul (f64.add (f64.add (f64.add (f64.add (f64.div (f64.convert_s/i64 (get_global $DeclareTest.MN)) (f64.convert_s/i64 (get_local $MN2))) (f64.promote/f32 (get_local $L))) (f64.mul (get_local $A) (get_global $DeclareTest.B))) (get_local $B)) (get_local $C)) (f64.promote/f32 (get_local $GG))) (f64.convert_s/i64 (get_local $Z))) (f64.convert_s/i32 (get_local $E))) (f64.convert_s/i64 (get_local $F))) (f64.convert_s/i64 (i64.trunc_s/f64 (get_global $DeclareTest.C))))))
    )
    )