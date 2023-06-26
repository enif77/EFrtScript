/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class EqualWord : IWord
{
    public string Name => "=";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(2);

        var b = interpreter.StackPop();
        var a = interpreter.StackPop();

        if (a.IsStringValue() || b.IsStringValue())
        {
            interpreter.StackPush(interpreter.ConvertToString(a).String == interpreter.ConvertToString(b).String);    
        }
        else if (a.IsFloatingPointValue() || b.IsFloatingPointValue())
        {
            interpreter.StackPush(Math.Abs(a.Float - b.Float) < Tolerance);
        }
        else
        {
            interpreter.StackPush(a.Integer == b.Integer);
        }

        return 1;
    }
    

    private const double Tolerance = 0.00001;
}
