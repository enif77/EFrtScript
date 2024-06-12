/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;
using EFrtScript.Libs.CoreExt.Values;

internal class EndCaseWord : IWord
{
    public string Name => "ENDCASE";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        interpreter.ReturnStackExpect(1);
        
        // Set the ENDCASE index to all ENDOF words in the dictionary and remove them.
        var endCaseIndex = interpreter.WordBeingDefined!.NextWordIndex;
        
        // ... find all ENDOF word references in the return stack and set their index to the ENDCASE index.
        var returnStackValue = interpreter.ReturnStackPeek();
        while (returnStackValue is EndOfControlWordReferenceValue)
        {
            var endOfControlWord = ((EndOfControlWordReferenceValue)interpreter.ReturnStackPop()).ControlWord;
            
            endOfControlWord.SetEndCaseIndex(endCaseIndex);
            
            returnStackValue = interpreter.ReturnStackPeek();
        }
        
        // Check, if we are inside of CASE (we have CASE index in R).
        if (interpreter.ReturnStackPeek() is not CaseControlWordReferenceValue)
        {
            interpreter.Throw(-22, "ENDCASE without CASE.");
        }
        
        // Drop the CASE index from R.
        interpreter.ReturnStackPop();
        
        // Add the ENDCASE word to the new word's definition.
        interpreter.WordBeingDefined!.AddWord(new EndCaseControlWord());

        return 1;
    }
}
