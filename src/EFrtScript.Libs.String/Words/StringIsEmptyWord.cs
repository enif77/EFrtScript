/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.String.Words;

using EFrtScript.Extensions;


internal class StringIsEmptyWord : IWord
{
    public string Name => "?STRING-IS-EMPTY";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
            
        interpreter.StackPush(string.IsNullOrEmpty(interpreter.StackPop().String));
        
        return 1;
    }
}

/*
 
?STRING-IS-EMPTY

( string -- flag )

The flag is true if the string is empty, otherwise false.
 
 */