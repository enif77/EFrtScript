/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;
using EFrtScript.Words;


internal class PrintStringLitWord : IWord
{
    public string Name => ".\"";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);

        // ." -> S" abc" S.
        interpreter.WordBeingDefined!.AddWord(new ConstantValueWord(interpreter.CurrentInputSource!.ReadStringFromSource()));
        interpreter.WordBeingDefined!.AddWord(new PrintStringWord());

        return 1;
    }
}
