/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;


/// <summary>
/// Defines a value.
/// </summary>
public interface IValue
{
    /// <summary>
    /// A boolean value.
    /// </summary>
    bool Boolean { get; }
    
    /// <summary>
    /// An integer value.
    /// </summary>
    int Integer { get; }

    /// <summary>
    /// A real (floating point) value.
    /// </summary>
    double Float { get; }

    /// <summary>
    /// A string value.
    /// </summary>
    string String { get; }
}
