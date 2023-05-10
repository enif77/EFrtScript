/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;
using EFrtScript.Values;


internal class DecimalWord : IWord
{
    public string Name => "DECIMAL";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.HeapStore(Library.NumericConversionRadixHeapIndex, 10);

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/DECIMAL

DECIMAL

( -- )
Set the numeric conversion radix to ten (decimal).

 */
