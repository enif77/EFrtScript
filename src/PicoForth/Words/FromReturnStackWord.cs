/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Extensions;


internal class FromReturnStackWord : IWord
{
    public string Name => "R>";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(interpreter.ReturnStackPop());

        return 1;
    }
}
