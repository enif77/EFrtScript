/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class RotWord : IWord
{
    public string Name => "ROT";
    public bool IsImmediate => false;
    public bool IsControlWord => false;
    

    public void Execute(IEvaluator evaluator)
    {
        var n3 = evaluator.StackPop();
        var n2 = evaluator.StackPop();
        var n1 = evaluator.StackPop();

        evaluator.StackPush(n2);
        evaluator.StackPush(n3);
        evaluator.StackPush(n1);
    }
}
