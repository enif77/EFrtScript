/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.IO;


public class NullOutputWriter : IOutputWriter
{
    public void Write(string format, params object[] arg)
    {
    }


    public void WriteLine()
    {
    }


    public void WriteLine(string format, params object[] arg)
    {
    }
}
