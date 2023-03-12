/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


/// <summary>
/// A word that is defining IF-THEN-ELSE condition.
/// </summary>
internal class IfControlWord : IWord
{
    public string Name => "IF";
    public bool IsImmediate => false;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="currentIndex">The index of this IF word inside of a non primitive word.</param>
    public IfControlWord(int currentIndex)
    {
        _thisIndex = currentIndex;
        _elseIndexIncrement = 0;
        _thenIndexIncrement = 0;
    }


    /// <summary>
    /// Called by the word THEN. It inserts here its index in the word it belongs to.
    /// </summary>
    /// <param name="thenIndex">The THEN index.</param>
    public void SetThenIndex(int thenIndex)
    {
        _thenIndexIncrement = thenIndex - _thisIndex;
    }

    /// <summary>
    /// Called by the word ELSE. It inserts here its index in the word it belongs to.
    /// </summary>
    /// <param name="elseIndex">The ELSE index.</param>
    public void SetElseIndex(int elseIndex)
    {
        _elseIndexIncrement = elseIndex - _thisIndex;
    }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        if (interpreter.StackPop().Boolean)
        {
            // The flag is true, advance instruction index by one to true portion of IF.
            return 1;
        }

        // The flag is false, advance to ELSE if present or to THEN if not.
        return _elseIndexIncrement != 0 
            ? _elseIndexIncrement
            : _thenIndexIncrement;
    }


    private readonly int _thisIndex;
    private int _elseIndexIncrement;
    private int _thenIndexIncrement;
}
