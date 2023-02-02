/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class FetchWord : IWord
{
    public string Name => "@";
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(evaluator.HeapFetch(evaluator.StackPop().Int));
    }
}
