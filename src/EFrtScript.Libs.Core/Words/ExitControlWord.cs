/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;


/// <summary>
/// A word that is exiting a word definition.
/// </summary>
internal class ExitControlWord : IWord
{
    public string Name => "EXIT";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="indexIncrement">The index increment to jump to the beginning of a loop.</param>
    public ExitControlWord(INonPrimitiveWord definitionWord)
    {
        _definitionWord = definitionWord;
    }


    public int Execute(IInterpreter interpreter)
    {
        // Do not execute remaining words from the currently running word.
        _definitionWord.BreakExecution();

        return 1;
    }


    private readonly INonPrimitiveWord _definitionWord;
}
