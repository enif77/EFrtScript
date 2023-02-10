/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class PrintStringLitWord : IWord
{
    public string Name => ".\"";
    public bool IsImmediate => true;


    public void Execute(IEvaluator evaluator)
    {
        evaluator.OutputWriter.Write(evaluator.ReadStringFromSource());
    }
}
