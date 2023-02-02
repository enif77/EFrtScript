/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class MinusWord : IWord
{
    public string Name => "-";
    

    public void Execute(IEvaluator evaluator)
    {
        var b = evaluator.StackPop().Int;
        evaluator.StackPush(new IntValue(evaluator.StackPop().Int - b));
    }
}
