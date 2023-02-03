/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class FetchReturnStackWord : IWord
{
    public string Name => "R@";
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(evaluator.ReturnStackPeek());
    }
}
