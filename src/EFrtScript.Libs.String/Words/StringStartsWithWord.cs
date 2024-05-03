/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.String.Words;

using EFrtScript.Extensions;


internal class StringStartsWithWord : IWord
{
    public string Name => "?STRING-STARTS-WITH";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(2);
          
        var shouldStartWith = interpreter.StackPop().String;

        interpreter.StackPush(
            interpreter.StackPop().String
                .StartsWith(shouldStartWith));
        
        return 1;
    }
}

/*
 
?STRING-STARTS-WITH

( string shouldStartWith -- flag )

The flag is true if the string starts with the shouldStartWith string, otherwise false.
 
 */