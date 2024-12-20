/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class IntQuestionWord : IWord
{
    public string Name => "INT?";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
        
        interpreter.StackPush(interpreter.StackPop().IsIntegerValue());

        return 1;
    }
}

/*

INT?

(x -- flag)
Checks if x is an integer value.

 */
