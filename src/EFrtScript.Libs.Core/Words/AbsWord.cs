/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class AbsWord : IWord
{
    public string Name => "ABS";
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

        if (a.IsFloatingPointValue())
        {
            interpreter.StackPush(Math.Abs(a.Float));
        }
        else
        {
            interpreter.StackPush(Math.Abs(a.Integer));
        }

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/ABS

ABS

( n1 | f1 -- n2 | f2 )
n2 | f2 is the absolute value of n1 | f1.

Testing:

T{       0 ABS ->          0 }T
T{       1 ABS ->          1 }T
T{      -1 ABS ->          1 }T
T{ MIN-INT ABS -> MID-UINT+1 }T

T{       0.0 ABS ->          0.0 }T
T{       1.0 ABS ->          1.0 }T
T{      -1.0 ABS ->          1.0 }T

 */
