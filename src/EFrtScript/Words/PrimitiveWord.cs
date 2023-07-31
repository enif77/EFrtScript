/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Words;


/// <summary>
/// A word that is defining itself.
/// </summary>
internal class PrimitiveWord : IWord
{
    /// <inheritdoc/>
    public string Name { get; }
    
    /// <inheritdoc/>
    public bool IsImmediate => false;

    /// <inheritdoc/>
    public int ExecutionToken { get; set; }

    /// <summary>
    /// The body of this word.
    /// </summary>
    private readonly Func<IInterpreter, int> _action;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="wordName">A word name.</param>
    /// <param name="action">An action, this word is doing.</param>
    public PrimitiveWord(string wordName, Func<IInterpreter, int> action)
    {
        Name = wordName;
        _action = action;
    }


    public int Execute(IInterpreter interpreter)
    {
        return _action(interpreter);
    }
}
