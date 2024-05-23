/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class SlashWord : IWord
{
    public string Name => "/";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(2);

        var b = interpreter.StackPop();
        var a = interpreter.StackPop();
        
        if (a.IsFloatingPointValue() || b.IsFloatingPointValue())
        {
            try
            {
                interpreter.StackPush(a.Float / b.Float);
            }
            catch (DivideByZeroException)
            {
                interpreter.Throw(-10, "division by zero");
            }
        }
        else
        {
            if (b.Integer == 0)
            {
                interpreter.Throw(-10, "division by zero");
            }
            
            interpreter.StackPush(a.Integer / b.Integer);
        }
        
        return 1;
    }
}
