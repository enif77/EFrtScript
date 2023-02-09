/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class BeginNewWordCompilationWord : IWord
{
    public string Name => ":";
    public bool IsImmediate => false;
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.BeginNewWordCompilation(
            evaluator.ReadWordFromSource() ?? throw new Exception("A new word name expected."));
    }
}
