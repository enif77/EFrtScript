/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using System;

using EFrtScript.Extensions;


internal class ThenWord : IWord
{
    public string Name => "THEN";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        
        // Get the index of the next free slot in the non-primitive word being defined.
        var thenIndex = interpreter.WordBeingDefined!.NextWordIndex;

        interpreter.ReturnStackExpect(1);

        var controlWord = interpreter.WordBeingDefined!.GetWord(interpreter.ReturnStackPop().Integer);
        if (controlWord is ElseControlWord)
        {
            // We had a previous else 
            ((ElseControlWord)controlWord).SetThenIndexIncrement(thenIndex);

            // Pop control stack again to find IF.
            interpreter.ReturnStackExpect(1);
            controlWord = interpreter.WordBeingDefined!.GetWord(interpreter.ReturnStackPop().Integer);
        }

        if (controlWord is IfControlWord)
        {
            // We had a previous if. Set the then index into
            // the if control word.
            ((IfControlWord)controlWord).SetThenIndex(thenIndex);
        }
        else
        {
            throw new Exception("THEN requires a previous IF or ELSE.");
        }

        return 1;
    }
}
