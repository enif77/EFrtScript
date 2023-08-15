/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;


/// <summary>
/// A word that is defining the loop goto-beginning jump.
/// </summary>
internal class AgainControlWord : IWord
{
    public string Name => "AGAIN";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="index">The index to jump to the beginning of a loop.</param>
    public AgainControlWord(int index)
    {
        _index = index;
    }


    public int Execute(IInterpreter interpreter)
    {
        return _index;
    }


    private readonly int _index;
}
