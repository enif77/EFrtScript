/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.IO;


/// <summary>
/// An IOutputWriter implementation, that writes to Console.
/// </summary>
public class ConsoleOutputWriter : IOutputWriter
{
    /// <inheritdoc/>
    public void Write(string format, params object[] arg)
    {
        if (arg == null || arg.Length == 0)
        {
            // We dont want he Write() method to parse the contents the format parameter if there are no args.
            Console.Write(format);    
        }
        else
        {
            Console.Write(format, arg);
        }
    }

    /// <inheritdoc/>
    public void Write(char value)
    {
        Console.Write(value);
    }

    /// <inheritdoc/>
    public void WriteLine()
    {
        Console.WriteLine();
    }

    /// <inheritdoc/>
    public void WriteLine(string format, params object[] arg)
    {
        if (arg == null || arg.Length == 0)
        {
            // We dont want he WriteLine() method to parse the contents the format parameter if there are no args.
            Console.WriteLine(format);    
        }
        else
        {
            Console.WriteLine(format, arg);
        }
    }
}
