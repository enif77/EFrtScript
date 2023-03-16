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


    public InterpreterException()
    {
    }


    public InterpreterException(string? message)
        : base(message)
    {
    }


    public InterpreterException(string? message, Exception? inner)
        : base(message, inner)
    {
    }


    public InterpreterException(int exceptionCode)
    {
        ExceptionCode = exceptionCode;
    }


    public InterpreterException(int exceptionCode, string? message)
        : base(message)
    {
        ExceptionCode = exceptionCode;
    }


    public InterpreterException(int exceptionCode, string? message, Exception? inner)
        : base(message, inner)
    {
        ExceptionCode = exceptionCode;
    }
}
 