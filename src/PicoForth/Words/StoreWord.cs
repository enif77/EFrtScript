/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class StoreWord : IWord
{
    public string Name => "!";
    public bool IsImmediate => false;


    public void Execute(IInterpreter interpreter)
    {
        var addr = interpreter.StackPop().Integer;
        interpreter.HeapStore(addr, interpreter.StackPop());
    }
}
