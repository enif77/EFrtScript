/* Copyright (C) Premysl Fara and Contributors */

using PicoForth.Values;

namespace PicoForth.Words;


internal class DepthWord : IWord
{
    public string Name => "DEPTH";
    public bool IsImmediate => false;
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(new IntValue(evaluator.StackDepth));
    }
}
