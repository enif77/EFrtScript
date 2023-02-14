/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class SwapWord : IWord
{
    public string Name => "SWAP";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        var n2 = interpreter.StackPop();
        var n1 = interpreter.StackPop();

        interpreter.StackPush(n2);
        interpreter.StackPush(n1);

        return 1;
    }
}
