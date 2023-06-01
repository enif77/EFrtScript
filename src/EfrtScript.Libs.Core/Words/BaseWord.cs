/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class BaseWord : IWord
{
    public string Name => "BASE";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackFree(1);

        interpreter.StackPush(interpreter.GetNumericConversionRadixHeapIndex());

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/BASE

BASE

( -- addr )
addr is the index of a value containing the current number-conversion radix {{2...36}}.

 */
