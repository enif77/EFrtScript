/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class BlWord : IWord
{
    public string Name => "BL";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackFree(1);

        interpreter.StackPush(' ');

        return 1;
    }
}

/*

src/EFrtScript.Libs.Core/Words/DepthWord.cs

BL

( -- char )
char is the character value for a space.

Testing:

T{ BL -> 32 }T

*/
