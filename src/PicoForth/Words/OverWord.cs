/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class OverWord : IWord
{
    public string Name => "OVER";
    public bool IsImmediate => false;


    public void Execute(IEvaluator evaluator)
    {
        var n2 = evaluator.StackPop();
        var n1 = evaluator.StackPeek();

        evaluator.StackPush(n2);
        evaluator.StackPush(n1);
    }
}
