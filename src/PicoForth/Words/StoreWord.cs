/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class StoreWord : IWord
{
    public string Name => "!";
    

    public void Execute(IEvaluator evaluator)
    {
        var addr = evaluator.StackPop().Int;
        evaluator.HeapStore(addr, evaluator.StackPop());
    }
}
