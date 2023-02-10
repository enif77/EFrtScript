/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class StarWord : IWord
{
    public string Name => "*";
    public bool IsImmediate => false;


    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(new IntValue(evaluator.StackPop().Integer * evaluator.StackPop().Integer));
    }
}
