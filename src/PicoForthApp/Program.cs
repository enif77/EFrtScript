﻿/* Copyright (C) Premysl Fara and Contributors */

using PicoForth.Libs.Core;

namespace PicoForthApp;

using System.IO;

using PicoForth;
using PicoForth.IO;
using PicoForth.Extensions;


internal static class Program
{
    private static void Main(string[] args)
    {
        var ev = new Interpreter(new ConsoleOutputWriter());

        var coreLib = new Library();
        coreLib.Initialize(ev);

        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/hello-world.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/print-2-swapped.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/stack-operations.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/not-case-sensitive.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/comparisons.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/type.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/spaces.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/custom-words.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/if-then-else.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/do-loop.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/store-fetch.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/try-catch.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/abort.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/abort-with-message.pfrt"));
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/evaluate.pfrt"));
        
        ev.StackClear();
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/depth.pfrt"));

        ev.Interpret("S\" --- Mics ---\" S. CR");

        ev.Interpret("30 3 * 1 + 2 / . CR");
        ev.Interpret("30 3 !  3 @ . CR");
        ev.Interpret("( 30) 40 ( )3 !  3 @ . CR ( comment... )");

        ev.Interpret("S\" aaa \" S. 3 . CR");
        ev.Interpret(": StrBbb S\" bbb\" ; StrBbb S. CR");
    }
}

/*

~/.../PicoForth/src$ dotnet run --project ./PicoForthApp/PicoForthApp.csproj

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
