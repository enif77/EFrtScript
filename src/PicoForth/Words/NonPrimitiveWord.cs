/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class NonPrimitiveWord : IWord
{
    public string Name => "NonPrimitiveWord";
    

    public void Execute(IEvaluator evaluator)
    {
        throw new NotImplementedException(Name);
    }
}
