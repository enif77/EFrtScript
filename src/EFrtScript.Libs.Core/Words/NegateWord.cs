/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class NegateWord : IWord
{
    public string Name => "NEGATE";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
        
        var a = interpreter.StackPop();
        
        if (a.IsFloatingPointValue())
        {
            interpreter.StackPush(-a.Float);
        }
        else
        {     
            try   
            {
                checked
                {
                    interpreter.StackPush(-a.Integer);
                }
            }
            catch (OverflowException)
            {
                interpreter.StackPush(-a.Float);
            }
        }
        
        return 1;
    }
}
