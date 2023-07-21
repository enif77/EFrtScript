/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Stacks;


/// <summary>
/// Base class for all stacks.
/// </summary>
/// <typeparam name="T">A type of values stored in the stack.</typeparam>
public abstract class AStackBase<T> : IGenericStack<T>
{
    /// <inheritdoc/>
    public T?[] Items { get; }

    /// <inheritdoc/>
    public int Top { get; set; }

    /// <inheritdoc/>
    public int Count => Top + 1;
    
    /// <inheritdoc/>
    public bool IsEmpty => Count < 1;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="capacity">The stack capacity.</param>
    protected AStackBase(int capacity = 32)
    {
        Items = new T[capacity];

        Init(default(T));
    }

    /// <inheritdoc/>
    public void Init(T? defaultValue)
    {
        Top = -1;
        for (var i = 0; i < Items.Length; i++)
        {
            Items[i] = defaultValue;
        }
    }

    /// <inheritdoc/>
    public void Clear()
    {
        Init(default(T));
    }

    /// <inheritdoc/>
    public T? Peek()
    {
        return Items[Top];
    }

    /// <inheritdoc/>
    public T? Pick(int index)
    {
        return Items[Top - index];
    }

    /// <inheritdoc/>
    public T? Pop()
    {
        return Items[Top--];
    }

    /// <inheritdoc/>
    public void Push(T? a)
    {
        Items[++Top] = a;
    }

    /// <inheritdoc/>
    public void Dup()
    {
        Items[Top + 1] = Items[Top];
        Top++;
    }

    /// <inheritdoc/>
    public void Drop(int n = 1)
    {
        Top -= n;
    }

    /// <inheritdoc/>
    public void Swap()
    {
        // t = b
        var t = Items[Top];

        // a a
        Items[Top] = Items[Top - 1];

        // b a
        Items[Top - 1] = t;
    }

    /// <inheritdoc/>
    public void Over()
    {
        Items[Top + 1] = Items[Top - 1];
        Top++;
    }

    /// <inheritdoc/>
    public void Rot()
    {
        // t = a
        var t = Items[Top - 2];

        // b b c
        Items[Top - 2] = Items[Top - 1];

        // b c c
        Items[Top - 1] = Items[Top];

        // b c a
        Items[Top] = t;
    }

    /// <inheritdoc/>
    public void Roll(int index)
    {
        var item = Items[Top - index];
        for (var i = (Top - index) + 1; i <= Top; i++)
        {
            Items[i - 1] = Items[i];
        }

        Items[Top] = item;
    }
}
