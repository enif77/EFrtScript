/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class DoWord : IWord
{
    public string Name => "DO";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        interpreter.ReturnStackFree(1);

        interpreter.ReturnStackPush(
            interpreter.WordBeingDefined!.AddWord(
                new DoControlWord()));

        return 1;
    }
}
