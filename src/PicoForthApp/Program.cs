/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForthApp;

using System.IO;

using PicoForth; 


internal class Program
{
    private static void Main(string[] args)
    {
        var ev = new Interpreter(new ConsoleOutputWriter());

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
        
        ev.StackClear();
        ev.Interpret(File.ReadAllText("./PicoForthApp/Examples/depth.pfrt"));

        ev.Interpret(".\" --- Mics ---\" CR");

        ev.Interpret("30 3 * 1 + 2 / . CR");
        ev.Interpret("30 3 !  3 @ . CR");
        ev.Interpret("( 30) 40 ( )3 !  3 @ . CR ( comment... )");

        ev.Interpret(".\" aaa \" 3 . CR");
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
