/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class StarWord : IWord
{
    public string Name => "*";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        var b = interpreter.StackPop();
        var a = interpreter.StackPop();
        
        if (a.IsFloatingPointValue() || b.IsFloatingPointValue())
        {
            interpreter.StackPush(a.Float * b.Float);
        }
        else
        {
            try   
            {
                checked
                {
                    interpreter.StackPush(a.Integer * b.Integer);
                }
            }
            catch (OverflowException)
            {
                interpreter.StackPush(a.Float * b.Float);
            }
        }
        
        return 1;
    }
}
