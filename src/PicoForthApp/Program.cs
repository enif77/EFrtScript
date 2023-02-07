/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForthApp;

using System.IO;

using PicoForth; 


internal class Program
{
    private static void Main(string[] args)
    {
        var ev = new Evaluator();

        ev.Eval(File.ReadAllText("./PicoForthApp/Examples/hello-world.pfrt"));
        ev.Eval(File.ReadAllText("./PicoForthApp/Examples/print-2-swapped.pfrt"));
        ev.Eval(File.ReadAllText("./PicoForthApp/Examples/stack-operations.pfrt"));
        ev.Eval(File.ReadAllText("./PicoForthApp/Examples/not-case-sensitive.pfrt"));

        ev.Eval("30 3 * 1 + 2 / . CR");
        ev.Eval("30 3 !  3 @ . CR");
        ev.Eval("( 30) 40 ( )3 !  3 @ . CR ( comment... )");

        ev.Eval(".\" aaa\" 3 . CR");
        ev.Eval("S\" bbb\" S. CR");
    }
}

/*

~/.../PicoForth/src$ dotnet run --project ./PicoForthApp/PicoForthApp.csproj

https://github.com/enif77/EFrt

0< 0= < = > 
: ;
>NUMBER S>D
,
2DROP 2DUP 2OVER 2SWAP
ABS LSHIFT RSHIFT MAX MIN MODE NEGATE
+! 2! 2@
"* /" "* /MOD" /MOD 1+ 1- 2* 2/ FM/MOD INVERT M* SM/REM
AND OR XOR
BL SPACE SPACES TYPE
COUNT
DEPTH
EVALUATE
QUIT

*/
