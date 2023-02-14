/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class ToReturnStackWord : IWord
{
    public string Name => ">R";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.ReturnStackPush(interpreter.StackPop());

        return 1;
    }
}
