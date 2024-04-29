/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.String.Words;

using EFrtScript.Extensions;


internal class StringTrimWord : IWord
{
    public string Name => "STRING-TRIM";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
            
        interpreter.StackPush(interpreter.StackPop().String.Trim());
        
        return 1;
    }
}

/*
 
STRING-TRIM

( string -- string )

Removes all leading and trailing white-space characters from the string.
 
 */