/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using System;


internal class BeginNewWordCompilationWord : IWord
{
    public string Name => ":";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.BeginNewWordCompilation(
            interpreter.CurrentInputSource!.ReadWordFromSource() ?? throw new Exception("A new word name expected."));

        return 1;
    }
}
