/* Copyright (C) Premysl Fara and Contributors */

using PicoForth.Values;

namespace PicoForth.Words;


internal class EqualWord : IWord
{
    public string Name => "=";
    public bool IsImmediate => false;
    public bool IsControlWord => false;
    

    public void Execute(IEvaluator evaluator)
    {
        var b = evaluator.StackPop();
        evaluator.StackPush(new IntValue(evaluator.StackPop().Integer == b.Integer ? -1 : 0));
    }
}
