/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using System;


/// <summary>
/// An interpretation related exception.
/// </summary>
public class InterpreterException : Exception
{
    /// <summary>
    /// An exception code.
    /// </summary>
    public int ExceptionCode { get; }


    /// <summary>
    /// Constructor.
    /// </summary>
    public InterpreterException()
    {
    }

    /// <summary>
    /// Constructor that sets the message.
    /// </summary>
    public InterpreterException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Constructor that sets the message and the inner exception.
    /// </summary>
    public InterpreterException(string? message, Exception? inner)
        : base(message, inner)
    {
    }

    /// <summary>
    /// Constructor that sets the exception code.
    /// </summary>
    public InterpreterException(int exceptionCode)
    {
        ExceptionCode = exceptionCode;
    }

    /// <summary>
    /// Constructor that sets the exception code and the message.
    /// </summary>
    public InterpreterException(int exceptionCode, string? message)
        : base(message)
    {
        ExceptionCode = exceptionCode;
    }

    /// <summary>
    /// Constructor that sets the exception code, the message and the inner exception.
    /// </summary>
    public InterpreterException(int exceptionCode, string? message, Exception? inner)
        : base(message, inner)
    {
        ExceptionCode = exceptionCode;
    }
}
 