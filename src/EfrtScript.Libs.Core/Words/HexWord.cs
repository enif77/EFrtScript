/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;
using EFrtScript.Values;


internal class HexWord : IWord
{
    public string Name => "HEX";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.HeapStore(Library.NumbericConversionRadixHeapIndex, 16);

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/HEX

HEX

( -- )
Set the numeric conversion radix to sixteen (hexadecimal).

 */
