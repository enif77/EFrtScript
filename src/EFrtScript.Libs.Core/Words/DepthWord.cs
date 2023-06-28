/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class DepthWord : IWord
{
    public string Name => "DEPTH";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackFree(1);

        interpreter.StackPush(interpreter.GetStackDepth());

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/DEPTH

DEPTH

( -- +n )
+n is the number of values contained in the data stack before +n was placed on the stack.

Testing:

T{ 0 1 DEPTH -> 0 1 2 }T
T{   0 DEPTH -> 0 1   }T
T{     DEPTH -> 0     }T

*/
