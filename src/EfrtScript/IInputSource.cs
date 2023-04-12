/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;


/// <summary>
/// An input - the source code.
/// </summary>
public interface IInputSource
{
    /// <summary>
    /// A character last read from the source. Its -1 (EOF) when no read happened yet
    /// or if all characters were already read from the source.
    /// </summary>
    int CurrentChar { get; }

    /// <summary>
    /// Reads and returns the next char from the source. Updates the CurrentChar property value.
    /// </summary>
    /// <returns>A next character from the source or -1 when no more characters are available in the source.</returns>
    int ReadChar();

    /// <summary>
    /// Reads and returns the next word from the source.
    /// </summary>
    /// <returns>A next word from the source or null when no more characters are available in the source.</returns>
    string? ReadWord();

    /// <summary>
    /// Reads and returns a delimited string from the source. Throws exception, if the string is not properly terminated.
    /// </summary>
    /// <returns>A delimited string from the source or an empty string when no more characters are available in the source.</returns>
    string ReadString();
}
