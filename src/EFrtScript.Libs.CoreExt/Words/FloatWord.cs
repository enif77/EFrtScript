/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class FloatWord : IWord
{
    public string Name => "FLOAT";
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

        interpreter.StackPush(a.Float);

        return 1;
    }
}

/*

FLOAT

(x -- n)
Converts a value on the stack to a floating point value.

 */
