/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class EndCaseControlWord : IWord
{
    public string Name => "ENDCASE";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        interpreter.StackPop();
        
        return 1;
    }
}
