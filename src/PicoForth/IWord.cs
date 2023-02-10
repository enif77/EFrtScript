/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


/// <summary>
/// A word definition.
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
    /// Control words do some action, but are not in the words list.
    /// </summary>
    bool IsControlWord { get; }
    

    /// <summary>
    /// Executes this word's action.
    /// </summary>
    /// <param name="evaluator"></param>
    void Execute(IEvaluator evaluator);
}
