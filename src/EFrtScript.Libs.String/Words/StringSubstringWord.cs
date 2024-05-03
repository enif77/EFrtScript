/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.String.Words;

using EFrtScript.Extensions;


internal class StringSubstringWord : IWord
{
    public string Name => "STRING-SUBSTRING";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(2);
            
        var startIndex = interpreter.StackPop().Integer;

        interpreter.StackPush(
            interpreter.StackPop().String
                .Substring(startIndex));
        
        return 1;
    }
}

/*
 
STRING-SUBSTRING

( string startIndex -- substring )

Returns a substring of the string starting at the startIndex.
 
 */