/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class ReadStringLitWord : IWord
{
    public string Name => "S\"";
    public bool IsImmediate => true;
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(new StringValue(evaluator.ReadStringFromSource()));
    }
}
