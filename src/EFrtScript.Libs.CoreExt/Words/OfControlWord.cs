/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


/// <summary>
/// A word that is defining OF-ENDOF condition.
/// </summary>
internal class OfControlWord : IWord
{
    public string Name => "OF";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }

    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="currentIndex">The index of this OF word inside the word it belongs to.</param>
    public OfControlWord(int currentIndex)
    {
        _thisIndex = currentIndex;
        _endOfIndexIncrement = 0;
    }
    

    /// <summary>
    /// Called by the word ENDOF. It inserts here its index in the word it belongs to.
    /// </summary>
    /// <param name="endOfIndex">The ENDOF index.</param>
    public void SetEndOfIndex(int endOfIndex)
    {
        _endOfIndexIncrement = endOfIndex - _thisIndex;
    }


    public int Execute(IInterpreter interpreter)
    { 
        interpreter.StackExpect(2);

        var valueToCompareTo = interpreter.StackPop();
        var caseSelectorValue = interpreter.StackPeek();
        
        bool areEqual;
        
        if (valueToCompareTo.IsStringValue() || caseSelectorValue.IsStringValue())
        {
            areEqual = interpreter.ConvertToString(valueToCompareTo).String == interpreter.ConvertToString(caseSelectorValue).String;    
        }
        else if (valueToCompareTo.IsFloatingPointValue() || caseSelectorValue.IsFloatingPointValue())
        {
            areEqual = Math.Abs(valueToCompareTo.Float - caseSelectorValue.Float) < Tolerance;
        }
        else
        {
            areEqual = valueToCompareTo.Integer == caseSelectorValue.Integer;
        }
        
        if (areEqual)
        {
            // Remove the caseSelectorValue from the stack.
            interpreter.StackPop();
            
            // Advance instruction index by one into the body of the OF-ENDOF structure.
            return 1;
        }

        // Advance to the word behind the ENDOF.
        return _endOfIndexIncrement + 1;
    }

    
    private const double Tolerance = 0.00001;

    private readonly int _thisIndex;
    private int _endOfIndexIncrement;
}
