(module ;; Vanilla WebAssembly runtime demo (console output + math) - runtime contract v2

    ;; imports - identical module/field names & type signatures as the compiler emits,
    ;; so this module links against the lightweight runtime in dist/visualbasic.wasm.js

    (import "Math" "sqrt" (func $Math.sqrt (param f64) (result f64)))
    (import "Math" "sin" (func $Math.sin (param f64) (result f64)))
    (import "Math" "cos" (func $Math.cos (param f64) (result f64)))

    (import "string" "add" (func $string.add (param i32) (param i32) (result i32)))
    (import "string" "toString" (func $i32.toString (param i32) (result i32)))
    (import "string" "toString" (func $f64.toString (param f64) (result i32)))

    (import "console" "log" (func $console.log (param i32) (result i32)))
    (import "console" "warn" (func $console.warn (param i32) (result i32)))

    (import "GC" "addObject" (func $GC.addObject (param i32) (param i32)))

    ;; the linear memory is provided by the javascript runtime
    (memory (import "env" "bytechunks") 1)

    ;; the compiler managed heap marker (also returned by global.GetMemorySize)
    (global $global.ObjectManager (mut i32) (i32.const 1253))

    ;; string constants (null terminated utf-8), offsets computed bottom up:
    ;;  1024 header  | 1082 pi     | 1107 fact   | 1114 sqrt2  | 1125 sin30
    ;;  1138 cos60   | 1151 primes | 1171 done   | 1226 title  | 1244 version
    (data (i32.const 1024) "=== Vanilla WebAssembly Runtime Demo (console + math) ===\00pi (Leibniz, n=20000) = \0010! = \00sqrt(2) = \00sin(pi/6) = \00cos(pi/3) = \00primes below 100 = \00demo finished. console + math verified in WebAssembly.\00Vanilla WASM Demo\002.0-demo\00")

    (export "global.GetMemorySize" (func $global.GetMemorySize))
    (export "App.RunApp" (func $App.RunApp))
    (export "Math.Factorial" (func $factorial))
    (export "AssemblyInfo.AssemblyTitle" (func $AssemblyInfo.AssemblyTitle))
    (export "AssemblyInfo.AssemblyVersion" (func $AssemblyInfo.AssemblyVersion))

    ;; ------------------------------------------------------------------
    ;; helpers
    ;; ------------------------------------------------------------------

    (func $global.GetMemorySize (result i32)
        (get_global $global.ObjectManager))

    ;; console.log(label + f64.toString(value))
    (func $print_f64 (param $label i32) (param $x f64)
        (drop (call $console.log (call $string.add (get_local $label) (call $f64.toString (get_local $x))))))

    ;; console.log(label + i32.toString(value))
    (func $print_i32 (param $label i32) (param $x i32)
        (drop (call $console.log (call $string.add (get_local $label) (call $i32.toString (get_local $x))))))

    ;; n! (iterative)
    (func $factorial (param $n i32) (result i32)
        (local $r i32)
        (set_local $r (i32.const 1))
        (block $done
            (loop $loop
                (br_if $done (i32.eqz (get_local $n)))
                (set_local $r (i32.mul (get_local $r) (get_local $n)))
                (set_local $n (i32.sub (get_local $n) (i32.const 1)))
                (br $loop)))
        (get_local $r))

    ;; pi via the Leibniz series: 4 * sum((-1)^k / (2k+1), k=0..n-1)
    (func $pi_leibniz (param $n i32) (result f64)
        (local $sum f64)
        (local $sign f64)
        (local $k i32)
        (set_local $sum (f64.const 0))
        (set_local $sign (f64.const 1))
        (set_local $k (i32.const 0))
        (block $done
            (loop $loop
                (br_if $done (i32.ge_s (get_local $k) (get_local $n)))
                (set_local $sum
                    (f64.add (get_local $sum)
                        (f64.div (get_local $sign)
                            (f64.convert_s/i32
                                (i32.add (i32.mul (get_local $k) (i32.const 2)) (i32.const 1))))))
                (set_local $sign (f64.neg (get_local $sign)))
                (set_local $k (i32.add (get_local $k) (i32.const 1)))
                (br $loop)))
        (f64.mul (get_local $sum) (f64.const 4)))

    ;; primality test
    (func $is_prime (param $n i32) (result i32)
        (local $i i32)
        (local $lim i32)
        (local $prime i32)
        (if (i32.lt_s (get_local $n) (i32.const 2)) (then (return (i32.const 0))))
        (set_local $prime (i32.const 1))
        (set_local $lim (i32.div_s (get_local $n) (i32.const 2)))
        (set_local $i (i32.const 2))
        (block $done
            (loop $loop
                (br_if $done (i32.gt_s (get_local $i) (get_local $lim)))
                (if (i32.eqz (i32.rem_s (get_local $n) (get_local $i)))
                    (then
                        (set_local $prime (i32.const 0))
                        (br $done)))
                (set_local $i (i32.add (get_local $i) (i32.const 1)))
                (br $loop)))
        (get_local $prime))

    ;; how many primes are strictly below `limit`
    (func $count_primes (param $limit i32) (result i32)
        (local $c i32)
        (local $i i32)
        (set_local $c (i32.const 0))
        (set_local $i (i32.const 2))
        (block $done
            (loop $loop
                (br_if $done (i32.ge_s (get_local $i) (get_local $limit)))
                (if (call $is_prime (get_local $i))
                    (then (set_local $c (i32.add (get_local $c) (i32.const 1)))))
                (set_local $i (i32.add (get_local $i) (i32.const 1)))
                (br $loop)))
        (get_local $c))

    ;; ------------------------------------------------------------------
    ;; entry point + AssemblyInfo
    ;; ------------------------------------------------------------------

    (func $App.RunApp (result i32)
        (drop (call $console.log (i32.const 1024)))
        (call $print_f64 (i32.const 1082) (call $pi_leibniz (i32.const 20000)))
        (call $print_i32 (i32.const 1107) (call $factorial (i32.const 10)))
        (call $print_f64 (i32.const 1114) (call $Math.sqrt (f64.const 2)))
        (call $print_f64 (i32.const 1125)
            (call $Math.sin (f64.div (f64.const 3.141592653589793) (f64.const 6))))
        (call $print_f64 (i32.const 1138)
            (call $Math.cos (f64.div (f64.const 3.141592653589793) (f64.const 3))))
        (call $print_i32 (i32.const 1151) (call $count_primes (i32.const 100)))
        (drop (call $console.warn (i32.const 1244)))
        (drop (call $console.log (i32.const 1171)))
        (i32.const 0))

    (func $AssemblyInfo.AssemblyTitle (result i32)
        (i32.const 1226))

    (func $AssemblyInfo.AssemblyVersion (result i32)
        (i32.const 1244))

    (func $Application_SubNew)
    (start $Application_SubNew)
)
