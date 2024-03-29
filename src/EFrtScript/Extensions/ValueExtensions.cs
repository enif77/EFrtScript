/* Copyright (C) Premysl Fara and Contributors */

using EFrtScript.Values;

namespace EFrtScript.Extensions;


/// <summary>
/// Values related extensions. 
/// </summary>
public static class ValueExtensions
{
    /// <summary>
    /// Returns true, if a value is an integer value.
    /// </summary>
    /// <param name="value">An IValue instance.</param>
    /// <returns>True, if a value is an integer value.</returns>
    public static bool IsIntegerValue(this IValue value)
        => value is IntegerValue;
    
    /// <summary>
    /// Returns true, if a value is a real (floating point) value.
    /// </summary>
    /// <param name="value">An IValue instance.</param>
    /// <returns>True, if a value is a real value.</returns>
    public static bool IsFloatingPointValue(this IValue value)
        => value is FloatValue;
    
    /// <summary>
    /// Returns true, if a value is a string value.
    /// </summary>
    /// <param name="value">An IValue instance.</param>
    /// <returns>True, if a value is a string value.</returns>
    public static bool IsStringValue(this IValue value)
        => value is StringValue;
}