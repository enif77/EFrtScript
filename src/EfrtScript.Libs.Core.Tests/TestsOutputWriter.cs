/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using System;


public class TestsOutputWriter : IOutputWriter
{
    public bool WriteCalled { get; private set; }
    public bool WriteCharCalled { get; private set; }
    public bool WriteLineCalled { get; private set; }
    public bool WriteLineFormatCalled { get; private set; }
    
    public string? Format { get; private set; }
    public object[]? Args { get; private set; }

    public string? Output { get; private set; }

    
    public void Write(string format, params object[] args)
    {
        WriteCalled = true;
        Format = format;
        Args = args;
        Output = string.Format(format, args);
    }


    public void Write(char value)
    {
        WriteCharCalled = true;
        Output = string.Format("{0}", value);
    }


    public void WriteLine()
    {
        WriteLineCalled = true;
        Format = string.Empty;
        Args = new object[0];
        Output = Environment.NewLine;
    }


    public void WriteLine(string format, params object[] args)
    {
        WriteLineFormatCalled = true;
        Format = format;
        Args = args;
        Output = string.Format(format, args) + Environment.NewLine;
    }
}
