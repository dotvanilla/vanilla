# 验收 A：批量编译 test/test 下的全部 .vb 文件
#
#   powershell -ExecutionPolicy Bypass -File test/compile_all.ps1
#
# 对每个 .vb：
#   1) 调用编译器（src/vanilla）生成 <name>.wast
#   2) 调用 dist/tools/wat2wasm.exe 把 <name>.wast 编译为 <name>.wasm
# 只要 .wast 能被 wat2wasm 成功编译为 .wasm，即算通过（以“可编译”为验收标准）。
#
# 产物：test/_build_out/<name>.{wast,wasm} 与汇总清单 test/_build_out/result.txt

$ErrorActionPreference = 'Continue'

$testRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$repoRoot = Split-Path -Parent $testRoot

$exe = Join-Path $repoRoot 'src\vanilla\bin\Debug\net10.0\vanilla.exe'
$wat2wasm = Join-Path $repoRoot 'dist\tools\wat2wasm.exe'
$srcDir = Join-Path $testRoot 'test'
$outDir = Join-Path $testRoot '_build_out'
$result = Join-Path $outDir 'result.txt'

if (-not (Test-Path $exe)) {
    throw "找不到编译器：$exe`n请先执行： dotnet build src\vanilla\vanilla.NET5.vbproj -c Debug"
}
if (-not (Test-Path $wat2wasm)) {
    throw "找不到 wat2wasm：$wat2wasm"
}

New-Item -ItemType Directory -Force -Path $outDir | Out-Null
Remove-Item $result -ErrorAction SilentlyContinue

$ok = 0
$fail = 0

foreach ($f in (Get-ChildItem (Join-Path $srcDir '*.vb') | Sort-Object Name)) {
    $n = $f.BaseName
    $wast = Join-Path $outDir "$n.wast"
    $wasm = Join-Path $outDir "$n.wasm"

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
        Add-Content $result ("FAIL {0} : {1}" -f $n, ($log | Select-String 'error' | Select-Object -First 1))
        $fail++
    }
}

Add-Content $result "==== OK=$ok FAIL=$fail TOTAL=$($ok + $fail) ===="
Get-Content $result | Select-Object -Last 1
