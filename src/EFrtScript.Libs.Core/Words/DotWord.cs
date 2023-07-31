/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using System.Globalization;

using EFrtScript.Extensions;


internal class DotWord : IWord
{
    public string Name => ".";
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
        
        interpreter.Output.Write(a.IsFloatingPointValue()
            ? string.Format(CultureInfo.InvariantCulture, "{0}", a.Float)
            : interpreter.ToStringValue(a.Integer, interpreter.GetNumericConversionRadix()));

        return 1;
    }
}

/*

.

( n -- )
Display n in free field format.

*/
