/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class DupeWord : IWord
{
    public string Name => "DUP";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
        interpreter.StackFree(1);

        interpreter.StackPush(interpreter.StackPeek());

        return 1;
    }
}
