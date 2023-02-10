/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class StoreWord : IWord
{
    public string Name => "!";
    public bool IsImmediate => false;
    public bool IsControlWord => false;
    

    public void Execute(IEvaluator evaluator)
    {
        var addr = evaluator.StackPop().Integer;
        evaluator.HeapStore(addr, evaluator.StackPop());
    }
}
