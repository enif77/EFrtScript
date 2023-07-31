/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class DecimalWord : IWord
{
    public string Name => "DECIMAL";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.SetNumericConversionRadix(10);

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/DECIMAL

DECIMAL

( -- )
Set the numeric conversion radix to ten (decimal).

 */
