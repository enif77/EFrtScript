/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class DepthWord : IWord
{
    public string Name => "DEPTH";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackFree(1);

        interpreter.StackPush(interpreter.GetStackDepth());

        return 1;
    }
}
