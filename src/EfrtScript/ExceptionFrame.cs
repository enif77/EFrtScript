/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

/// <summary>
/// Exception frame describes the current execution state before a word is executed.
/// </summary>
public class ExceptionFrame
{
    /// <summary>
    /// A data stack Top value.
    /// </summary>
    public int StackTop { get; set; }

    /// <summary>
    /// A return stack Top value.
    /// </summary>
    public int ReturnStackTop { get; set; }

    // /// <summary>
    // /// An input stack Top value.
    // /// </summary>
    // public int InputSourceStackTop { get; set; }
        
    /// <summary>
    /// The word, that is currently running and executing the CATCH word.
    /// </summary>
    public IWord? ExecutingWord { get; set; }

    /// <summary>
    /// In index of a word following the word CATCH.
    /// </summary>
    public int NextWordIndex { get; set; }
}