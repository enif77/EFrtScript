#dotnet run --project EfrtScriptApp/EfrtScriptApp.csproj ./EfrtScriptApp/Examples/hello-world.efrts
#dotnet exec ./EfrtScriptApp/bin/Debug/net7.0/EFrtScriptApp.dll ./EfrtScriptApp/Examples/hello-world.efrts
#& ./EfrtScriptApp/bin/Debug/net7.0/EFrtScriptApp ./EfrtScriptApp/Examples/hello-world.efrts

$efrt_path = "./EfrtScriptApp/bin/Debug/net7.0/efrts"
$examples_path = "./EfrtScriptApp/Examples/"

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
    "depth.efrts")

foreach ($s in $scripts) {
    & $efrt_path ($examples_path + $s)
}
