/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class PickWord : IWord
{
    public string Name => "PICK";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
        
        var index = interpreter.StackPop().Integer;
        if (index < 0)
        {
            throw new InvalidOperationException("PICK: index must be >= 0.");
        }
        
        if (index >= interpreter.State.Stack.Count)
        {
            throw new InvalidOperationException("PICK: index is out of range.");
        }
        
        interpreter.StackPush(interpreter.State.Stack.Pick(index)!);

        return 1;
    }
}

/*

PICK

(xu ... x1 x0 u -- xu ... x1 x0 xu)
Removes u. Copy the xu to the top of the stack. 0 PICK is equivalent to DUP. 1 PICK is equivalent to OVER.

 */
