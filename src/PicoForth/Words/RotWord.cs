/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class RotWord : IWord
{
    public string Name => "ROT";
    public bool IsImmediate => false;


    public void Execute(IInterpreter interpreter)
    {
        var n3 = interpreter.StackPop();
        var n2 = interpreter.StackPop();
        var n1 = interpreter.StackPop();

        interpreter.StackPush(n2);
        interpreter.StackPush(n3);
        interpreter.StackPush(n1);
    }
}
