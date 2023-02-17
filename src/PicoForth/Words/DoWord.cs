/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Extensions;
using PicoForth.Values;


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

        interpreter.ReturnStackPush(new IntValue(
            interpreter.WordBeingDefined!.AddWord(
                new DoControlWord())));

        return 1;
    }
}
