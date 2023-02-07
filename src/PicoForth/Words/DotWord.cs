/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class DotWord : IWord
{
    public string Name => ".";
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.OutputWriter.Write($"{evaluator.StackPop().Integer}");
    }
}
