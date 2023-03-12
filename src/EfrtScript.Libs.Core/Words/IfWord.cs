/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using System;

using EFrtScript.Extensions;


internal class IfWord : IWord
{
    public string Name => "IF";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        if (interpreter.IsCompiling == false)
        {
            throw new Exception("IF outside a new word definition.");
        }

        interpreter.ReturnStackFree(1);

        interpreter.ReturnStackPush(
            interpreter.WordBeingDefined!.AddWord(
                new IfControlWord(interpreter.WordBeingDefined.NextWordIndex)));

        return 1;
    }
}
