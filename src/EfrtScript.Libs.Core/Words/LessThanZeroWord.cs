/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class LessThanZeroWord : IWord
{
    public string Name => "0<";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        interpreter.StackPush(interpreter.StackPop().Integer < 0 ? -1 : 0);

        return 1;
    }
}
