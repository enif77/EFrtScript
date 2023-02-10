/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class SpaceWord : IWord
{
    public string Name => "SPACE";
    public bool IsImmediate => false;
    public bool IsControlWord => false;
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.OutputWriter.Write(" ");
    }
}
