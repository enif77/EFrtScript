/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Extensions;

using EFrtScript;
using EFrtScript.Values;
using EFrtScript.Words;


/// <summary>
/// Heap related Interpreter class extensions. 
/// </summary>
public static class HeapExtensions
{
    public static void HeapStore(this IInterpreter interpreter, int address, IValue value)
        => interpreter.State.Heap.Store(address, value);


    public static IValue HeapFetch(this IInterpreter interpreter, int address)
        => interpreter.State.Heap.Fetch(address) ?? throw new NullReferenceException("Null as value should not be stored in the heap.");
}