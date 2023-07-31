/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;


/// <summary>
/// Definnes a word.
/// </summary>
public interface IWord
{
    /// <summary>
    /// A name of this word.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// If this word should be executed immediately. Even during compilation.
    /// </summary>
    bool IsImmediate { get; }

    /// <summary>
    /// An execution token. Used by the EXECUTE word to find a words definition for execution.
    /// Its set by the IWordsList.AddWord() and the IWordsList.RemoveWord() methods.
    /// </summary>
    int ExecutionToken { get; set; }


    /// <summary>
    /// Executes this word's action.
    /// </summary>
    /// <param name="interpreter">An interpreter.</param>
    /// <returns>Next word index increment in a compiled word.</returns>
    int Execute(IInterpreter interpreter);
}
