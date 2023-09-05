/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class UnloopControlWord : IWord
{
    public string Name => "UNLOOP";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.ReturnStackExpect(2);

        // Remove the limit and the index.
        interpreter.ReturnStackDrop(2);

        return 1;
    }
}
