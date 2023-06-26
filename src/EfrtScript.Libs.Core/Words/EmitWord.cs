/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class EmitWord : IWord
{
    public string Name => "EMIT";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        interpreter.Output.Write((char)interpreter.StackPopInteger());

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/EMIT

EMIT

( x -- )
If x is a graphic character in the implementation-defined character set, display x.
The effect of EMIT for all other values of x is implementation-defined.
 
 */