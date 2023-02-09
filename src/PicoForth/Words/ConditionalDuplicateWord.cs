/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class ConditionalDuplicateWord : IWord
{
    public string Name => "?DUP";
    public bool IsImmediate => false;
    

    public void Execute(IEvaluator evaluator)
    {
        var v = evaluator.StackPeek();
        if (v.Boolean)
        {
            evaluator.StackPush(v);
        }
        else
        {
            evaluator.StackPop();
            evaluator.StackPush(new IntValue(0));
        }
    }
}
