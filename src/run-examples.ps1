#dotnet run --project EfrtScriptApp/EfrtScriptApp.csproj ./EfrtScriptApp/Examples/hello-world.efrts
#dotnet exec ./EfrtScriptApp/bin/Debug/net7.0/EFrtScriptApp.dll ./EfrtScriptApp/Examples/hello-world.efrts
#& ./EfrtScriptApp/bin/Debug/net7.0/EFrtScriptApp ./EfrtScriptApp/Examples/hello-world.efrts

$efrt_path = "./EFrtScriptApp/bin/Debug/net7.0/efrts"
$examples_path = "./EFrtScriptApp/Examples/"

$scripts = (
    "hello-world.efrts",
    "abort.efrts",
    "hello-world.efrts",
    "print-2-swapped.efrts",
    "stack-operations.efrts",
    "not-case-sensitive.efrts",
    "comparisons.efrts",
    "type.efrts",
    "spaces.efrts",
    "custom-words.efrts",
    "if-then-else.efrts",
    "do-loop.efrts",
    "store-fetch.efrts",
    "try-catch.efrts",
    "abort.efrts",
    "abort-with-message.efrts",
    "evaluate.efrts",
    "negate.efrts",
    "depth.efrts",
    "execute.efrts",
    "tick-execute.efrts",
    "begin-again.efrts",
    "begin-until.efrts",
    "begin-while-repeat.efrts",
    "do-loop-leave.efrts",
    "do-question-loop.efrts")

foreach ($s in $scripts) {
    & $efrt_path ($examples_path + $s)
}
