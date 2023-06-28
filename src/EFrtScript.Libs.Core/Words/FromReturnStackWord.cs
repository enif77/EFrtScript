/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


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
