/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class DupWord : IWord
{
    public string Name => "DUP";
    public bool IsImmediate => false;
    public bool IsControlWord => false;
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(evaluator.StackPeek());
    }
}
