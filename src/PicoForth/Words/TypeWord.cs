/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class TypeWord : IWord
{
    public string Name => "TYPE";
    public bool IsImmediate => false;


    public void Execute(IEvaluator evaluator)
    {
        evaluator.OutputWriter.Write(evaluator.StackPop().String);
    }
}
