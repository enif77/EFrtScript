/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.IO;


/// <summary>
/// This class does not write anything.
/// </summary>
public class NullOutputWriter : IOutputWriter
{
    /// <inheritdoc/>
    public void Write(string format, params object[] arg)
    {
    }

    /// <inheritdoc/>
    public void Write(char value)
    {
    }

    /// <inheritdoc/>
    public void WriteLine()
    {
    }

    /// <inheritdoc/>
    public void WriteLine(string format, params object[] arg)
    {
    }
}
