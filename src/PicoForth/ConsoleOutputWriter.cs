/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


/// <summary>
/// An IOutputWriter implementation, that writes to Console.
/// </summary>
public class ConsoleOutputWriter : IOutputWriter
{
    public void Write(string format, params object[] arg)
    {
        Console.Write(format, arg);
    }


    public void WriteLine()
    {
        Console.WriteLine();
    }


    public void WriteLine(string format, params object[] arg)
    {
        Console.WriteLine(format, arg);
    }
}
