/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Words;


internal class PrintStringLitWord : IWord
{
    public string Name => ".\"";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        if (interpreter.IsCompiling == false)
        {
            throw new Exception(".\" outside a new word definition.");
        }

        // ." -> S" abc" S.
        interpreter.WordBeingDefined!.AddWord(new ConstantValueWord(interpreter.CurrentInputSource!.ReadStringFromSource()));
        interpreter.WordBeingDefined!.AddWord(new PrintStringWord());

        return 1;
    }
}
