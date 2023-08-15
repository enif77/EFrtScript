/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class BeginWord : IWord
{
    public string Name => "BEGIN";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        interpreter.ReturnStackFree(1);
        interpreter.ReturnStackPush(
            interpreter.WordBeingDefined!.NextWordIndex);

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/BEGIN

BEGIN

(R: -- dest)
Put the next location for a transfer of control, dest, onto the control flow stack.

*/