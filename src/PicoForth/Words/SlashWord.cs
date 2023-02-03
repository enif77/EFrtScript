/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class SlashWord : IWord
{
    public string Name => "/";
    

    public void Execute(IEvaluator evaluator)
    {
        var b = evaluator.StackPop().Integer;
        evaluator.StackPush(new IntValue(evaluator.StackPop().Integer / b));
    }
}
