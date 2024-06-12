/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class CaseControlWord : IWord
{
    public string Name => "CASE";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        return 1;
    }
}
