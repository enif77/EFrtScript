/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class StringWord : IWord
{
    public string Name => "STRING";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
        
        var a = interpreter.StackPop();
        if (a.IsStringValue())
        {
            interpreter.StackPush(a);
        }
        else
        {
            interpreter.StackPush(a.String);
        }

        return 1;
    }
}

/*

STRING

(x -- str)
Converts a value on the top of the stack to a string.

 */
