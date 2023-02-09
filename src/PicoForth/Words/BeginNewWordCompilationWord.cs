/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class BeginNewWordCompilationWord : IWord
{
    public string Name => "BeginNewWordCompilation";
    

    public void Execute(IEvaluator evaluator)
    {
        throw new NotImplementedException(Name);
    }
}

/*

// : word-name body ;
private int ColonAction()
{
    _interpreter.BeginNewWordCompilation();
    _interpreter.WordBeingDefined = new NonPrimitiveWord(_interpreter, _interpreter.ParseWord());

    return 1;
}
 
 */