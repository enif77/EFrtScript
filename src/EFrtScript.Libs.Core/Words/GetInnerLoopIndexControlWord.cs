/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class GetInnerLoopIndexControlWord : IWord
{
    public string Name => "I";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackFree(1);
        interpreter.ReturnStackExpect(1);

        // ( -- inner-index)  [ ... inner-limit inner-index -- ... inner-limit inner-index ]
        interpreter.StackPush(interpreter.ReturnStackPeek());   

        return 1;
    }
}
