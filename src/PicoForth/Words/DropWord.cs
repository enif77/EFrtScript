/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class DropWord : IWord
{
    public string Name => "DROP";
    public bool IsImmediate => false;
    public bool IsControlWord => false;
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPop();
    }
}
