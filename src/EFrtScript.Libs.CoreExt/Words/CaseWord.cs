/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;
using EFrtScript.Libs.CoreExt.Values;

internal class CaseWord : IWord
{
    public string Name => "CASE";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        interpreter.ReturnStackFree(1);
        
        // Reference to the CASE word. Will be used by the ENDCASE word.
        var caseControlWordValue = new CaseControlWordReferenceValue(
            new CaseControlWord());
        
        // Push the CASE word reference to the return stack.
        interpreter.ReturnStackPush(caseControlWordValue);
        
        // Add the CASE word to the new word's definition.
        interpreter.WordBeingDefined!.AddWord(caseControlWordValue.ControlWord);

        return 1;
    }
}
