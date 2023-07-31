/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;


/// <summary>
/// A word that is defining IF-THEN-ELSE condition.
/// </summary>
internal class ElseControlWord : IWord
{
    public string Name => "ELSE";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="indexFollowingElse">The index of a word following this ELSE word.</param>
    public ElseControlWord(int indexFollowingElse)
    {
        _indexFollowingElse = indexFollowingElse;
        _thenIndexIncrement = 0;
    }


    /// <summary>
    /// Sets the offset for jumping to the THEN word.
    /// </summary>
    public void SetThenIndexIncrement(int thenIndexIncrement)
    {
        _thenIndexIncrement = thenIndexIncrement;
    }


    public int Execute(IInterpreter interpreter)
    {
        return _thenIndexIncrement - _indexFollowingElse + 1;  // Skip words inside of the ELSE block.
    }


    private readonly int _indexFollowingElse;
    private int _thenIndexIncrement;
}
