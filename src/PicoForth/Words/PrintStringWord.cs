/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class PrintStringWord : IWord
{
    public string Name => "S.";
    public bool IsImmediate => false;
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.OutputWriter.Write(evaluator.StackPop().String);
    }
}
