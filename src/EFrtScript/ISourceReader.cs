/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;


/// <summary>
/// A source code reader.
/// </summary>
public interface ISourceReader
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
    int NextChar();
}
