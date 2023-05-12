/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class DotWord : IWord
{
    public string Name => ".";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        var a = interpreter.StackPop();
        interpreter.Output.Write(a.IsFloatingPointValue()
            ? $"{a.Float}"
            : interpreter.ToStringValue(a.Integer, interpreter.GetNumericConversionRadix()));

        return 1;
    }
}

/*

.

( n -- )
Display n in free field format.

*/
