/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class PlusWord : IWord
{
    public string Name => "+";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(2);

        var b = interpreter.StackPop();
        var a = interpreter.StackPop();

        if (a.IsStringValue() || b.IsStringValue())
        {
            interpreter.StackPush(a.String + b.String);
        }
        else if (a.IsFloatingPointValue() || b.IsFloatingPointValue())
        {
            interpreter.StackPush(a.Float + b.Float);
        }
        else
        {
            try   
            {
                checked
                {
                    interpreter.StackPush(a.Integer + b.Integer);
                }
            }
            catch (OverflowException)
            {
                interpreter.StackPush(a.Float + b.Float);
            }
        }

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/Plus

+

( n1 | f1 | s1  n2 | f2 | s2 -- n3 | f3 | s3 )
Add n2 | f2 | s2 to n1 | f1 | s1, giving the sum n3 | f3 | s3.
If the sum of n1 and n2 overflows, it converts both n1 and n2 to floating point values and returns a floating point f3.
If either of the parameters is a floating point number, a floating point addition is performed.
If either of the parameters is a string, the concatenation is performed and s3 is returned.

Testing:

T{        0  5 + ->          5 }T
T{        5  0 + ->          5 }T
T{        0 -5 + ->         -5 }T
T{       -5  0 + ->         -5 }T
T{        1  2 + ->          3 }T
T{        1 -2 + ->         -1 }T
T{       -1  2 + ->          1 }T
T{       -1 -2 + ->         -3 }T
T{       -1  1 + ->          0 }T
T{ MID-UINT  1 + -> MID-UINT+1 }T

T{        0.0  5.0 + ->          5.0 }T
T{        5.0  0.0 + ->          5.0 }T
T{        0.0 -5.0 + ->         -5.0 }T
T{       -5.0  0.0 + ->         -5.0 }T
T{        1.0  2.0 + ->          3.0 }T
T{        1.0 -2.0 + ->         -1.0 }T
T{       -1.0  2.0 + ->          1.0 }T
T{       -1.0 -2.0 + ->         -3.0 }T
T{       -1.0  1.0 + ->          0.0 }T

T{        1.0  1 + ->            2.0 }T
T{          1  1.0 + ->          2.0 }T

T{       "abc" "def" + ->   "abcdef" }T
T{       "abc"   1 + ->       "abc1" }T
T{           1 "abc" + ->     "1abc" }T

 */
