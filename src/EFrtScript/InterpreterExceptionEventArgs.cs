/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using System;


/// <summary>
/// Interpreter exception event args.
/// </summary>
public class InterpreterExceptionEventArgs : EventArgs
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="exception">An exception thrown by the interpreter.</param>
    /// <exception cref="ArgumentNullException">Thrown, when the exception parameter is null.</exception>
    public InterpreterExceptionEventArgs(InterpreterException exception)
    {
        Exception = exception ?? throw new ArgumentNullException(nameof(exception));
    }

    /// <summary>
    /// An exception thrown by the interpreter.
    /// </summary>
    public InterpreterException Exception { get; } 
}