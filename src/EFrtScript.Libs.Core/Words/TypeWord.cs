/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using System.Globalization;

using EFrtScript.Extensions;


internal class TypeWord : IWord
{
    public string Name => "TYPE";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        var a = interpreter.StackPop();
        if (a.IsIntegerValue())
        {
            interpreter.Output.Write(interpreter.ToStringValue(a.Integer, interpreter.GetNumericConversionRadix()));
        }
        else if (a.IsFloatingPointValue())
        {
            interpreter.Output.Write(string.Format(CultureInfo.InvariantCulture, "{0}", a.Float));
        }
        else
        {
            interpreter.Output.Write(a.String);
        }

        return 1;
    }
}

/* 

TYPE

( x -- )
Display x. x may be a number or a string. Integer numbers are displayed using the current numeric conversion radix.
Floating point numbers are displayed using free field format.

*/