/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Exception.Values;

using System;
using System.Text.Json.Serialization;


/// <summary>
/// Holds an exception info on the stack.
/// </summary>
internal class ExceptionValue : IValue
{
    [JsonIgnore]
    public bool Boolean => Integer != 0;

    [JsonIgnore]
    public int Integer => _exception.ExceptionCode;

    [JsonIgnore]
    public double Float => Integer;
    
    public string String => _exception.Message;

    [JsonIgnore]
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
