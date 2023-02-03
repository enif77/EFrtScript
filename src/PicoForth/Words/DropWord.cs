/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class DropWord : IWord
{
    public string Name => "DROP";
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPop();
    }
}
