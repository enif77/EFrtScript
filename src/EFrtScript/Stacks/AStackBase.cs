/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Stacks;


/// <summary>
/// Base class for all stacks.
/// </summary>
/// <typeparam name="T">A type of values stored in the stack.</typeparam>
public abstract class AStackBase<T> : IGenericStack<T>
{
    private T?[] _items; 


    /// <summary>
    /// Indexer. Allows direct access to the internal values storage.
    /// </summary>
    public T? this[int i]
    {
        get { return _items[i]; }
        set { _items[i] = value; }
    }

    /// <inheritdoc/>
    public T?[] Items
    {
        get
        {
            if (Count < 1)
            {
                return new T[0];
            }

            var a = new T[Count];

            Array.Copy(_items, 0, a, 0, Count);

            return a;
        }
    }

    /// <inheritdoc/>
    public int Top { get; set; }

    /// <inheritdoc/>
    public int Count => Top + 1;
    
    /// <inheritdoc/>
    public int Capacity => _items.Length;

    /// <inheritdoc/>
    public bool IsEmpty => Count < 1;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="capacity">The stack capacity.</param>
    protected AStackBase(int capacity = 32)
    {
        _items = new T[capacity];

        Init(default(T));
    }

    /// <inheritdoc/>
    public void Init(T? defaultValue)
    {
        Top = -1;
        for (var i = 0; i < _items.Length; i++)
        {
            _items[i] = defaultValue;
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
        return _items[Top];
    }

    /// <inheritdoc/>
    public T? Pick(int index)
    {
        return _items[Top - index];
    }

    /// <inheritdoc/>
    public T? Pop()
    {
        return _items[Top--];
    }

    /// <inheritdoc/>
    public void Push(T? a)
    {
        _items[++Top] = a;
    }

    /// <inheritdoc/>
    public void Dup()
    {
        _items[Top + 1] = _items[Top];
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
        var t = _items[Top];

        // a a
        _items[Top] = _items[Top - 1];

        // b a
        _items[Top - 1] = t;
    }

    /// <inheritdoc/>
    public void Over()
    {
        _items[Top + 1] = _items[Top - 1];
        Top++;
    }

    /// <inheritdoc/>
    public void Rot()
    {
        // t = a
        var t = _items[Top - 2];

        // b b c
        _items[Top - 2] = _items[Top - 1];

        // b c c
        _items[Top - 1] = _items[Top];

        // b c a
        _items[Top] = t;
    }

    /// <inheritdoc/>
    public void Roll(int index)
    {
        var item = _items[Top - index];
        for (var i = (Top - index) + 1; i <= Top; i++)
        {
            _items[i - 1] = _items[i];
        }

        _items[Top] = item;
    }
}
