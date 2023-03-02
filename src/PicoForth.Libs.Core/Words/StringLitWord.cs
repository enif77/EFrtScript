/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;
using PicoForth.Words;


internal class StringLitWord : IWord
{
    public string Name => "S\"";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        var stringLiteral = interpreter.CurrentInputSource!.ReadStringFromSource();

        if (interpreter.IsCompiling)
        {
            interpreter.WordBeingDefined!.AddWord(new ConstantValueWord(stringLiteral));
        }
        else
        {
            interpreter.StackFree(1);
            interpreter.StackPush(stringLiteral);
        }

        return 1;
    }
}
