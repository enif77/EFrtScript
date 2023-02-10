/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class FetchWord : IWord
{
    public string Name => "@";
    public bool IsImmediate => false;
    public bool IsControlWord => false;
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(evaluator.HeapFetch(evaluator.StackPop().Integer));
    }
}
