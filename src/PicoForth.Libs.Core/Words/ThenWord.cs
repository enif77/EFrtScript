/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class ThenWord : IWord
{
    public string Name => "THEN";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        if (interpreter.IsCompiling == false)
        {
            throw new Exception("THEN outside a new word definition.");
        }
        
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
