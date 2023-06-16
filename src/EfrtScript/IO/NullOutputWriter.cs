/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.IO;


/// <summary>
/// This class does not write anything.
/// </summary>
public class NullOutputWriter : IOutputWriter
{
    /// <inheritdoc cref="IInterpreter"/>
    public void Write(string format, params object[] arg)
    {
    }

    /// <inheritdoc cref="IInterpreter"/>
    public void Write(char value)
    {
    }

    /// <inheritdoc cref="IInterpreter"/>
    public void WriteLine()
    {
    }

    /// <inheritdoc cref="IInterpreter"/>
    public void WriteLine(string format, params object[] arg)
    {
    }
}
