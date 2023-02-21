/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class DoWord : IWord
{
    public string Name => "DO";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        if (interpreter.IsCompiling == false)
        {
            throw new Exception("DO outside a new word definition.");
        }

        interpreter.ReturnStackFree(1);

        interpreter.ReturnStackPush(
            interpreter.WordBeingDefined!.AddWord(
                new DoControlWord()));

        return 1;
    }
}
