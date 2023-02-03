/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class DupWord : IWord
{
    public string Name => "DUP";
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(evaluator.StackPeek());
    }
}
