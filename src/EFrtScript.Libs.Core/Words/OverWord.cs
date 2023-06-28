/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class OverWord : IWord
{
    public string Name => "OVER";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(2);
        interpreter.StackFree(1);

        var n2 = interpreter.StackPop();
        var n1 = interpreter.StackPeek();

        interpreter.StackPush(n2);
        interpreter.StackPush(n1);

        return 1;
    }
}
