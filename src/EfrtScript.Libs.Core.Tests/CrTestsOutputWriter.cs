/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;


public class CrTestsOutputWriter : IOutputWriter
{
    public bool WriteLineCalled { get; private set; }

    
    public void Write(string format, params object[] arg)
    {
        throw new NotImplementedException();
    }


    public void WriteLine()
    {
        WriteLineCalled = true;
    }


    public void WriteLine(string format, params object[] arg)
    {
        throw new NotImplementedException();
    }
}
