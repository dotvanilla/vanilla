# 批量编译 test/test 下的所有 .vb 文件，输出 OK/FAIL 清单
$exe = "G:\vanilla\src\vanilla\bin\Debug\net10.0\vanilla.exe"
$wat2wasm = "G:\vanilla\dist\tools\wat2wasm.exe"
$outDir = "G:\vanilla\test\_build_out"
$result = "$outDir\result.txt"

New-Item -ItemType Directory -Force -Path $outDir | Out-Null
Remove-Item $result -ErrorAction SilentlyContinue

$ok = 0
$fail = 0

foreach ($f in (Get-ChildItem "G:\vanilla\test\test\*.vb" | Sort-Object Name)) {
    $n = $f.BaseName
    $wast = "$outDir\$n.wast"
    $wasm = "$outDir\$n.wasm"

    Remove-Item $wast, $wasm -ErrorAction SilentlyContinue

    & $exe $f.FullName /out $wasm *>$null

    if (-not (Test-Path $wast)) {
        Add-Content $result "FAIL $n : compiler produced no wast"
        $fail++
        continue
    }

    $log = & $wat2wasm $wast -o $wasm 2>&1
    if (Test-Path $wasm) {
        Add-Content $result "OK   $n"
        $ok++
    }
    else {
        $err = ($log | Select-String "error" | Select-Object -First 1)
        Add-Content $result "FAIL $n : $err"
        $fail++
    }
}

Add-Content $result "==== OK=$ok FAIL=$fail TOTAL=$($ok + $fail) ===="
