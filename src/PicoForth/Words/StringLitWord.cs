/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class StringLitWord : IWord
{
    public string Name => "S\"";
    public bool IsImmediate => true;
    public bool IsControlWord => false;
    

    public void Execute(IEvaluator evaluator)
    {
        if (evaluator.IsCompiling == false)
        {
            throw new Exception("S\" outside a new word definition.");
        }
        
        evaluator.WordBeingDefined!.AddWord(new ConstantValueWord(evaluator.ReadStringFromSource()));
    }
}
