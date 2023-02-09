/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class EndNewWordCompilationWord : IWord
{
    public string Name => "EndNewWordCompilation";
    

    public void Execute(IEvaluator evaluator)
    {
        throw new NotImplementedException(Name);
    }
}

/*
 
// : word-name body ;
private int SemicolonAction()
{
    // Each user defined word exits with the EXIT word.
    _interpreter.WordBeingDefined.AddWord(new ExitControlWord(_interpreter, _interpreter.WordBeingDefined));

    _interpreter.EndNewWordCompilation();

    return 1;
} 
  
 */