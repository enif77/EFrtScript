/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class DotWord : IWord
{
    public string Name => ".";
    public bool IsImmediate => false;
    public bool IsControlWord => false;
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.OutputWriter.Write($"{evaluator.StackPop().Integer}");
    }
}
