/* Copyright (C) Premysl Fara and Contributors */

using EFrtScript.Words;

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;
using EFrtScript.Words;


internal class StringLitWord : IWord
{
    public string Name => "S\"";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        var stringLiteral = interpreter.CurrentInputSource!.ReadString();

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
