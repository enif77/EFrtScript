/* Copyright (C) Premysl Fara and Contributors */

using PicoForth.Values;

namespace PicoForth.Words;


internal class StringLitWord : IWord
{
    public string Name => "S\"";
    public bool IsImmediate => true;


    public void Execute(IInterpreter interpreter)
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
    }
}
