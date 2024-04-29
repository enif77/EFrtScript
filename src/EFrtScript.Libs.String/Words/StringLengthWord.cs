/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.String.Words;

using EFrtScript.Extensions;


internal class StringLengthWord : IWord
{
    public string Name => "STRING-LENGTH";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
            
        interpreter.StackPush(interpreter.StackPop().String.Length);
        
        return 1;
    }
}

/*
 
STRING-LENGTH

( string -- n )

The n is the length of the string.
 
 */