/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Exception.Values;

using System;


/// <summary>
/// Holds an exception info on the stack.
/// </summary>
internal class ExceptionValue : IValue
{
    public bool Boolean => Integer != 0;
    public int Integer => _exception.ExceptionCode;
    public double Float => Integer;
    public string String => _exception.Message;
    public Exception Exception => _exception;


    public ExceptionValue(int exceptionCode, string? message, Exception? ex)
    {
        _exception = new InterpreterException(exceptionCode, message, ex);
    }


    public ExceptionValue(InterpreterException exception)
    {
        _exception = exception;
    }


    public ExceptionValue(Exception exception)
        : this(-100, exception.Message, exception)
    {
    }


    private readonly InterpreterException _exception;
}
