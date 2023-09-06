/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class GetOuterLoopIndexControlWord : IWord
{
    public string Name => "J";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.ReturnStackExpect(3);
        interpreter.StackFree(1);

        // ( -- outer-index)  [ outer-limit outer-index inner-limit inner-index -- outer-limit outer-index inner-limit inner-index ]
        interpreter.StackPush(interpreter.ReturnStackPick(2));   

        return 1;
    }
}
