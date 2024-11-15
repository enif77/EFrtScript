/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;


/// <summary>
/// Defines a DEFERred word.
/// </summary>
public interface IDeferredWord
{
    /// <summary>
    /// An execution token of a word to be executed by this word.
    /// </summary>
    int DeferredWordBodyExecutionToken { get; }

    /// <summary>
    /// Sets an execution token to be executed by this word.
    /// </summary>
    /// <param name="executionToken">An execution token..</param>
    void SetDeferredWordBodyExecutionToken(int executionToken);
}
