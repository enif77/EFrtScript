/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


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

        interpreter.ReturnStackPush(new IntValue(
            interpreter.WordBeingDefined!.AddWord(
                new IfControlWord(interpreter.WordBeingDefined.NextWordIndex))));

        return 1;
    }
}
