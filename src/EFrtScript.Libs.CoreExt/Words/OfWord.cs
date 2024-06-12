/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;
using EFrtScript.Libs.CoreExt.Values;

internal class OfWord : IWord
{
    public string Name => "OF";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        interpreter.ReturnStackExpect(1);
        interpreter.ReturnStackFree(1);
        
        // Check, if we are inside of CASE (we have CASE or ENDOF index in R).
        var expectedReturnStackValue = interpreter.ReturnStackPeek();
        if (expectedReturnStackValue is not CaseControlWordReferenceValue &&
            expectedReturnStackValue is not EndOfControlWordReferenceValue)
        {
            interpreter.Throw(-22, "OF without CASE or ENDOF.");
        }
        
        // Reference to the OF word. Will be used by the ENDOF word.
        var ofControlWordValue = new OfControlWordReferenceValue(
            new OfControlWord(interpreter.WordBeingDefined!.NextWordIndex));
        
        // Push the OF word reference to the return stack.
        interpreter.ReturnStackPush(ofControlWordValue);
        
        // Add the OF word to the new word's definition.
        interpreter.WordBeingDefined!.AddWord(ofControlWordValue.ControlWord);

        return 1;
    }
}
