/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Extensions;

using EFrtScript;
using EFrtScript.Values;


/// <summary>
/// Heap related Interpreter class extensions. 
/// </summary>
public static class HeapExtensions
{
    /// <summary>
    /// Stores the value to the heap at the given address.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="address">A heap address.</param>
    /// <param name="value">A value.</param>
    public static void HeapStore(this IInterpreter interpreter, int address, IValue value)
        => interpreter.State.Heap.Store(address, value);

    /// <summary>
    /// Stores an int value to the heap at the given address.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="address">A heap address.</param>
    /// <param name="value">An int value.</param>
    public static void HeapStore(this IInterpreter interpreter, int address, int value)
        => interpreter.State.Heap.Store(address, new IntegerValue(value));

    /// <summary>
    /// Returns a value from the heap at the given address.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="address">A heap address.</param>
    /// <returns>A value from the heap at the given address.</returns>
    /// <exception cref="NullReferenceException">Thrown, when a null value was found on a given heap address.</exception>
    public static IValue HeapFetch(this IInterpreter interpreter, int address)
        => interpreter.State.Heap.Fetch(address) ?? throw new NullReferenceException("Null as value should not be stored in the heap.");
}