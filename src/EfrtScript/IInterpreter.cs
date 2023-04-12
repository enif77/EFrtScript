/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;


/// <summary>
/// Defines an interpreter.
/// </summary>
public interface IInterpreter
{
    IInterpreterState State { get; }
    IOutputWriter Output { get; }
    IInputSource? CurrentInputSource { get; }


    #region words

    /// <summary>
    /// True, if is this interpreter is compilling a new word.
    /// </summary>
    bool IsCompiling { get; }

    /// <summary>
    /// A new word, that is currently compiled, or null.
    /// </summary>
    INonPrimitiveWord? WordBeingDefined { get; }


    /// <summary>
    /// Checks, if a word is already registered.
    /// </summary>
    /// <returns>True, if a word is already registered.</returns>
    bool IsWordRegistered(string wordName);
    
    /// <summary>
    /// Gets a registered word. Throws an exception if no such word is registered.
    /// </summary>
    /// <returns>A registered word instance.</returns>
    IWord GetRegisteredWord(string wordName);
    
    /// <summary>
    /// Registers a new word. Throws an exception, if such word is already registered.
    /// </summary>
    void RegisterWord(IWord word);

    /// <summary>
    /// Begins a new word compilation. Throws an exception, if a new word compilation is already happening.
    /// Sets the WordBeingDefined property to the instance of the newly compiled word.
    /// </summary>
    /// <param name="wordName">A new word name. Required parameter.</param>
    void BeginNewWordCompilation(string wordName);
    
    /// <summary>
    /// Ends a new word compilation an adds the new word to the words dictionary.
    /// </summary>
    void EndNewWordCompilation();

    #endregion


    #region execution

    /// <summary>
    /// The state, in which is this interpreter.
    /// </summary>
    InterpreterStateCode InterpreterState { get; }
    
    /// <summary>
    /// True, if this program execution is currently terminated.
    /// </summary>
    bool IsExecutionTerminated { get; }
    
    /// <summary>
    /// An event fired, when a word is being executed.
    /// </summary>
    event EventHandler<InterpreterEventArgs>? ExecutingWord;

    /// <summary>
    /// An event fired, when a word was executed.
    /// </summary>
    event EventHandler<InterpreterEventArgs>? WordExecuted;

    
    /// <summary>
    /// Interprets a string.
    /// </summary>
    /// <param name="src">A string representing a Forth program.</param>
    void Interpret(string src);
    
    /// <summary>
    /// Executes a word. 
    /// Call this for a each word execution.
    /// </summary>
    /// <param name="word">A word to be executed.</param>
    /// <returns>A next word index.</returns>
    int ExecuteWord(IWord word);
    
    /// <summary>
    /// Cleans up the internal interpreters state.
    /// </summary>
    void Reset();

    /// <summary>
    /// Clears the stack and the object stack and calls the Quit() method.
    /// </summary>
    void Abort();

    /// <summary>
    /// The return stack is cleared and control is returned to the interpreter. The stack and the object stack are not disturbed.
    /// </summary>
    void Quit();

    /// <summary>
    /// Throws an system exception based on the exception code.
    /// </summary>
    /// <param name="exceptionCode">An exception code.</param>
    /// <param name="message">An optional exception message.</param>
    void Throw(int exceptionCode, string? message = null);
    
    /// <summary>
    /// Asks the interpreter to terminate the current script execution.
    /// </summary>
    void TerminateExecution();

    #endregion
}
