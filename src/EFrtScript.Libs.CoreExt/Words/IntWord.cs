/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class IntWord : IWord
{
    public string Name => "INT";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
        
        var a = interpreter.StackPop();
        if (a.IsStringValue())
        {
            var numberStr = a.String;
            if (interpreter.TryParseNumber(numberStr, out a) == false)
            {
                interpreter.Throw(-24, $"The '{numberStr}' string is not a valid number.");
            }
        }

        interpreter.StackPush(a.Integer);

        return 1;
    }
}

/*

INT

(x -- n)
Converts a value on the stack to integer.

 */
