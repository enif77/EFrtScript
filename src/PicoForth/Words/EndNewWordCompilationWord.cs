/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class EndNewWordCompilationWord : IWord
{
    public string Name => ";";
    public bool IsImmediate => true;
    public bool IsControlWord => false;
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.EndNewWordCompilation();
    }
}
