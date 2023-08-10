/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Stacks;


/// <summary>
/// Base class for all stacks.
/// </summary>
/// <typeparam name="T">A type of values stored in the stack.</typeparam>
public abstract class AStackBase<T> : IGenericStack<T>
{
    private readonly int _initialCapacity;
    private readonly int _growthFactor;
    private int _top;
    private T?[] _items; 
    

    /// <summary>
    /// Indexer. Allows direct access to the internal values storage.
    /// </summary>
    public T? this[int i]
    {
        get => _items[i];
        protected set => _items[i] = value;
    }

    /// <inheritdoc/>
    public T?[] Items
    {
        get
        {
            if (Count < 1)
            {
                return Array.Empty<T>();
            }

            var a = new T[Count];

            Array.Copy(_items, 0, a, 0, Count);

            return a;
        }
    }

    /// <inheritdoc/>
    public int Top
    {
        get => _top;

        set
        {
            if (value > _top && value >= _items.Length)
            {
                // We are growing above the current capacity.
                var lastCapacity = _items.Length;
                
                var newCapacity = lastCapacity + _growthFactor;
                while (newCapacity <= value && newCapacity > 0)
                {
                    newCapacity += _growthFactor;
                }

                if (newCapacity < 0)
                {
                    throw new InvalidOperationException("Cannot grow stack beyond the Int32.MaxValue.");
                }
                
                Array.Resize(ref _items, newCapacity);
                InitInternal(default, lastCapacity, _items.Length - 1);
            }
            else if (value < _top)
            {
                var halfCapacity = _items.Length / 2;
                if (halfCapacity > _initialCapacity)
                {
                    if (value < halfCapacity)
                    {
                        // We are returning to the half of the current capacity,
                        // but we will be still bigger than the initial capacity.
                        Array.Resize(ref _items, halfCapacity);
                        InitInternal(default, value + 1, _items.Length - 1);
                    }
                }
                else if (_items.Length > _initialCapacity && value < _initialCapacity)
                {
                    // We are returning to the initial capacity.
                    Array.Resize(ref _items, _initialCapacity);
                    InitInternal(default, value + 1, _items.Length - 1);
                }
            }

            _top = value;
        }
    }

    /// <inheritdoc/>
    public int Count => Top + 1;
    
    /// <inheritdoc/>
    public int Capacity => int.MaxValue;

    /// <inheritdoc/>
    public bool IsEmpty => Count < 1;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="capacity">The stack capacity.</param>
    protected AStackBase(int capacity = 32)
    {
        _initialCapacity = capacity;
        _growthFactor = Math.Max(capacity / 2, 8);
        _items = new T[capacity];

        Init(default);
    }

    /// <inheritdoc/>
    public void Init(T? defaultValue)
    {
        InitInternal(defaultValue, 0, _items.Length - 1);
    }

    /// <inheritdoc/>
    public void Clear()
    {
        // Clear/free up the old stuff.
        Init(default);

        // Create new items at the initial capacity.
        _items = new T[_initialCapacity];

        // Set all stack items to the default value.
        Init(default);
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
        Top++;
        _items[Top] = _items[Top - 1];
    }

    /// <inheritdoc/>
    public void Drop(int n = 1)
    {
        Top -= n;
    }

    /// <inheritdoc/>
    public void Swap()
    {
        (_items[Top], _items[Top - 1]) = (_items[Top - 1], _items[Top]);
    }

    /// <inheritdoc/>
    public void Over()
    {
        Top++;
        _items[Top] = _items[Top - 2];
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


    private void InitInternal(T? defaultValue, int from, int to)
    {
        _top = -1;
        for (var i = from; i <= to; i++)
        {
            _items[i] = defaultValue;
        }
    }
}
