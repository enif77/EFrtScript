/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using System;


internal class CatchWord : IWord
{
    public string Name => "CATCH";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        if (interpreter.IsCompiling == false)
        {
            throw new Exception("CATCH outside a new word definition.");
        }

        interpreter.WordBeingDefined!
            .AddWord(new CatchControlWord(
                interpreter.WordBeingDefined,
                interpreter.WordBeingDefined.NextWordIndex));

        return 1;
    }
}
