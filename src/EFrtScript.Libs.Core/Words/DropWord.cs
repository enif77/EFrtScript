/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class DropWord : IWord
{
    public string Name => "DROP";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        interpreter.StackDrop();

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/DROP

DROP

( x -- )
Remove x from the stack.

Testing:

T{ 1 2 DROP -> 1 }T
T{ 0   DROP ->   }T

*/