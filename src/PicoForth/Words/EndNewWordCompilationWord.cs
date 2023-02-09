/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class EndNewWordCompilationWord : IWord
{
    public string Name => ";";
    public bool IsImmediate => true;
    

    public void Execute(IEvaluator evaluator)
    {
        // Each user defined word exits with the EXIT word.
        //_interpreter.WordBeingDefined.AddWord(new ExitControlWord(_interpreter, _interpreter.WordBeingDefined));

        evaluator.EndNewWordCompilation();
    }
}
