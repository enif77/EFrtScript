/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;

internal class Program
{
    private static void Main(string[] args)
    {
        var ev = new Evaluator();
            
        ev.Eval("30 3 * 1 + 2 / . CR");
        ev.Eval("30 3 !  3 @ . CR");
        ev.Eval("( 30) 40 ( )3 !  3 @ . CR ( comment... )");

        ev.Eval(".\" aaa\" 3 . CR");
        ev.Eval("S\" bbb\" S. CR");
    }
}

/*

https://github.com/enif77/EFrt

*/