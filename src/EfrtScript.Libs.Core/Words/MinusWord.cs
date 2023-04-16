/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class MinusWord : IWord
{
    public string Name => "-";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(2);

        var b = interpreter.StackPop();
        var a = interpreter.StackPop();
        
        if (a.IsFloatingPointValue() || b.IsFloatingPointValue())
        {
            interpreter.StackPush(a.Float - b.Float);
        }
        else
        {
            try   
            {
                checked
                {
                    interpreter.StackPush(a.Integer - b.Integer);
                }
            }
            catch (OverflowException)
            {
                interpreter.StackPush(a.Float - b.Float);
            }
        }

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/Minus

-

( n1 | f1  n2 | f2 -- n3 | f3 )
Subtract n2 | f2 from n1 | f1, giving the difference n3 | f3.
If the sum of n1 and n2 overflows, it converts both n1 and n2 to floating point values and returns a floating point f3.
If either of the parameters is a floating point number, a floating point subtraction is performed.

Testing:

T{          0  5 - ->       -5 }T
T{          5  0 - ->        5 }T
T{          0 -5 - ->        5 }T
T{         -5  0 - ->       -5 }T
T{          1  2 - ->       -1 }T
T{          1 -2 - ->        3 }T
T{         -1  2 - ->       -3 }T
T{         -1 -2 - ->        1 }T
T{          0  1 - ->       -1 }T
T{ MID-UINT+1  1 - -> MID-UINT }T

T{          0.0  5.0 - ->       -5.0 }T
T{          5.0  0.0 - ->        5.0 }T
T{          0.0 -5.0 - ->        5.0 }T
T{         -5.0  0.0 - ->       -5.0 }T
T{          1.0  2.0 - ->       -1.0 }T
T{          1.0 -2.0 - ->        3.0 }T
T{         -1.0  2.0 - ->       -3.0 }T
T{         -1.0 -2.0 - ->        1.0 }T
T{          0.0  1.0 - ->       -1.0 }T

T{        1.0  2 + ->            1.0 }T
T{          1  2.0 + ->          1.0 }T

 */
 