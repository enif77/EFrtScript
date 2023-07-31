/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class HexWord : IWord
{
    public string Name => "HEX";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.SetNumericConversionRadix(16);

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/HEX

HEX

( -- )
Set the numeric conversion radix to sixteen (hexadecimal).

 */
