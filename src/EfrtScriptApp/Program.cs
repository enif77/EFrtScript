/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScriptApp;

using System.IO;

using EFrtScript;
using EFrtScript.IO;
using EFrtScript.Extensions;
using EFrtScript.Libs.Core;


internal static class Program
{
    private static void Main(string[] args)
    {
        var ev = new Interpreter(new ConsoleOutputWriter());

        var coreLib = new Library();
        coreLib.Initialize(ev);

        ev.Interpret(File.ReadAllText("./Examples/hello-world.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/print-2-swapped.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/stack-operations.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/not-case-sensitive.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/comparisons.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/type.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/spaces.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/custom-words.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/if-then-else.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/do-loop.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/store-fetch.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/try-catch.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/abort.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/abort-with-message.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/evaluate.efrts"));
        ev.Interpret(File.ReadAllText("./Examples/negate.efrts"));
        
        ev.StackClear();
        ev.Interpret(File.ReadAllText("./Examples/depth.efrts"));

        ev.Interpret("S\" --- Mics ---\" S. CR");

        ev.Interpret("30 3 * 1 + 2 / . CR");
        ev.Interpret("30 3 !  3 @ . CR");
        ev.Interpret("( 30) 40 ( )3 !  3 @ . CR ( comment... )");

        ev.Interpret("S\" aaa \" S. 3 . CR");
        ev.Interpret(": StrBbb S\" bbb\" ; StrBbb S. CR");
    }
}

/*

~/.../EFrtScript/src$ dotnet run --project ./EFrtScriptApp/EFrtScriptApp.csproj

https://github.com/enif77/EFrt

BEGIN ?DO +LOOP I J LEAVE REPEAT UNLOOP UNTIL WHILE AGAIN 

COUNT
EVALUATE

>NUMBER S>D
,
2DROP 2DUP 2OVER 2SWAP
ABS LSHIFT RSHIFT MAX MIN MODE NEGATE
+! 2! 2@
"* /" "* /MOD" /MOD 1+ 1- 2* 2/ FM/MOD INVERT M* SM/REM
AND OR XOR
QUIT

*/
