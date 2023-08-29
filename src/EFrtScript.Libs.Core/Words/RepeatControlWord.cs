/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;


/// <summary>
/// A word that is defining the WHILE-REPEAT loop end.
/// </summary>
internal class RepeatControlWord : IWord
{
    public string Name => "REPEAT";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="indexIncrement">The index increment to jump to the beginning of a loop.</param>
    public RepeatControlWord(int indexIncrement)
    {
        _incrementToWordAtLoopStart = indexIncrement;
    }


    public int Execute(IInterpreter interpreter)
    {
        return _incrementToWordAtLoopStart;
    }


    private readonly int _incrementToWordAtLoopStart;
}
