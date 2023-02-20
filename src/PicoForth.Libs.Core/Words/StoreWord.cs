/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class StoreWord : IWord
{
    public string Name => "!";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        var addr = interpreter.StackPop().Integer;
        interpreter.HeapStore(addr, interpreter.StackPop());

        return 1;
    }
}
