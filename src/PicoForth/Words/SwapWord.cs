/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class SwapWord : IWord
{
    public string Name => "SWAP";
    public bool IsImmediate => false;
    public bool IsControlWord => false;
    

    public void Execute(IEvaluator evaluator)
    {
        var n2 = evaluator.StackPop();
        var n1 = evaluator.StackPop();

        evaluator.StackPush(n2);
        evaluator.StackPush(n1);
    }
}
