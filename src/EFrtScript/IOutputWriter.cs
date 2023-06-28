/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;


/// <summary>
/// Defines an output writer.
/// </summary>
public interface IOutputWriter
{
    /// <summary>
    /// Writes the text representation of the specified array of objects to the output using the specified format information.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="arg">An array of objects to write using format.</param>
    void Write(string format, params object[] arg);

    /// <summary>
    /// Writes the specified Unicode character value to the output.
    /// </summary>
    /// <param name="value">A Unicode character.</param>
    void Write(char value);

    /// <summary>
    /// Writes a line terminator to an output.
    /// </summary>
    void WriteLine();

    /// <summary>
    /// Writes the text representation of the specified array of objects, followed by a line terminator, to an output using the format string.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="arg">An array of objects to write using format.</param>
    void WriteLine(string format, params object[] arg);
}
