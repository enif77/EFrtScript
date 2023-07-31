/* Copyright (C) Premysl Fara and Contributors */

using EFrtScript.Extensions;

namespace EFrtScript.Libs.Core.Words;


internal class EvaluateWord : IWord
{
    public string Name => "EVALUATE";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
        
        interpreter.Interpret(interpreter.StackPop().String);

        return 1;
    }
}
