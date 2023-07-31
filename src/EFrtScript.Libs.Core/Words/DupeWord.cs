/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class DupeWord : IWord
{
    public string Name => "DUP";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
        interpreter.StackFree(1);

        interpreter.StackPush(interpreter.StackPeek());

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/DUP

DUP

( x -- x x )
Duplicate x.

Testing:

T{ 1 DUP -> 1 1 }T

 */
 