/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class StringLitWord : IWord
{
    public string Name => "S\"";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        var stringLiteral = interpreter.ReadStringFromSource();

        if (interpreter.IsCompiling)
        {
            interpreter.WordBeingDefined!.AddWord(new ConstantValueWord(stringLiteral));
        }
        else
        {
            interpreter.StackPush(new StringValue(stringLiteral));
        }

        return 1;
    }
}
