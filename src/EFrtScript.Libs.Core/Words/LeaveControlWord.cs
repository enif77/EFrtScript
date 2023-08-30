/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


/// <summary>
/// A word that is leaving a loop.
/// </summary>
internal class LeaveControlWord : IWord
{
    public string Name => "LEAVE";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.ReturnStackExpect(2);

        // Remove the current index...
        _ = interpreter.ReturnStackPop();

        // and replace it with the limit, which effectively marks the end of the loop.
        interpreter.ReturnStackDup();

        return 1;
    }
}
