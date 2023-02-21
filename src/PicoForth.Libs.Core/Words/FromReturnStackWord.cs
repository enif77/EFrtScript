/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class FromReturnStackWord : IWord
{
    public string Name => "R>";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.ReturnStackExpect(1);
        interpreter.StackFree(1);

        interpreter.StackPush(interpreter.ReturnStackPop());

        return 1;
    }
}
