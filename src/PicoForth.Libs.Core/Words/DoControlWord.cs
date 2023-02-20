/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


/// <summary>
/// A word, that is defining the DO loop beginning.
/// </summary>
internal class DoControlWord : IWord
{
    public string Name => "DO";
    public bool IsImmediate => false;
    

    public int Execute(IInterpreter interpreter)
    {
        var index = interpreter.StackPop();

        interpreter.ReturnStackPush(interpreter.StackPop());  // limit
        interpreter.ReturnStackPush(index);                     // index

        return 1;
    }
}
