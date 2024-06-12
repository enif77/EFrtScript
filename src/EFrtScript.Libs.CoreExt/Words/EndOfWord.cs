/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;
using EFrtScript.Libs.CoreExt.Values;

internal class EndOfWord : IWord
{
    public string Name => "ENDOF";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        interpreter.ReturnStackExpect(1);
        interpreter.ReturnStackFree(1);
        
        // Check, if we are inside of CASE (we have OF index in R).
        var expectedReturnStackValue = interpreter.ReturnStackPeek();
        if (expectedReturnStackValue is not OfControlWordReferenceValue)
        {
            interpreter.Throw(-22, "ENDOF without OF.");
        }
        
        // Get the OF control word.
        var ofControlWord = ((OfControlWordReferenceValue)interpreter.ReturnStackPop()).ControlWord;

        // Get the ENDOF index.
        var endOfIndex = interpreter.WordBeingDefined!.NextWordIndex;
        
        // Set the ENDOF index to the OF word.
        ofControlWord.SetEndOfIndex(endOfIndex);
        
        // Reference to the ENDOF word. Will be used by the ENDCASE word.
        var endOfControlWordValue = new EndOfControlWordReferenceValue(
            new EndOfControlWord(endOfIndex));
        
        // Push the OF word reference to the return stack.
        interpreter.ReturnStackPush(endOfControlWordValue);
        
        // Add the ENDOF word to the new word's definition.
        interpreter.WordBeingDefined!.AddWord(endOfControlWordValue.ControlWord);

        return 1;
    }
}
