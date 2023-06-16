/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.IO;


/// <summary>
/// An IOutputWriter implementation, that writes to Console.
/// </summary>
public class ConsoleOutputWriter : IOutputWriter
{
    /// <inheritdoc cref="IInterpreter"/>
    public void Write(string format, params object[] arg)
    {
        Console.Write(format, arg);
    }

    /// <inheritdoc cref="IInterpreter"/>
    public void Write(char value)
    {
        Console.Write(value);
    }

    /// <inheritdoc cref="IInterpreter"/>
    public void WriteLine()
    {
        Console.WriteLine();
    }

    /// <inheritdoc cref="IInterpreter"/>
    public void WriteLine(string format, params object[] arg)
    {
        Console.WriteLine(format, arg);
    }
}
