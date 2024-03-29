/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


/// <summary>
/// A word, that is defining the DO loop beginning.
/// </summary>
internal class DoControlWord : IWord
{
    public string Name => "DO";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }
    

    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(2);
        interpreter.ReturnStackFree(2);

        var index = interpreter.StackPop();
        interpreter.ReturnStackPush(interpreter.StackPop());  // limit
        interpreter.ReturnStackPush(index);                     // index

        return 1;
    }
}
