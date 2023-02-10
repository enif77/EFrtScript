/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class FromReturnStackWord : IWord
{
    public string Name => "R>";
    public bool IsImmediate => false;


    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(evaluator.ReturnStackPop());
    }
}
