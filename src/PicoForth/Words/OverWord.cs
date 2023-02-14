/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class OverWord : IWord
{
    public string Name => "OVER";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        var n2 = interpreter.StackPop();
        var n1 = interpreter.StackPeek();

        interpreter.StackPush(n2);
        interpreter.StackPush(n1);

        return 1;
    }
}
