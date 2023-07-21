/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Words;

using EFrtScript.Extensions;
using EFrtScript.Values;


/// <summary>
/// A word that pushes a constant value on the stack.
/// </summary>
public class ConstantValueWord : IWord
{
    /// <inheritdoc/>
    public string Name => $"LITERAL({_value.String})";
    
    /// <inheritdoc/>
    public bool IsImmediate => false;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="value">A value to be pushed to the stack.</param>
    /// <exception cref="ArgumentNullException">Thrown, when the value parameter is null.</exception>
    public ConstantValueWord(IValue value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    
    /// <summary>
    /// Creates a new instance of the ConstantValueWord class with a boolean value.
    /// </summary>
    /// <param name="value">A value.</param>
    public ConstantValueWord(bool value)
        : this(value ? -1 : 0)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the ConstantValueWord class with an integer value.
    /// </summary>
    /// <param name="value">A value.</param>
    public ConstantValueWord(int value)
        : this(new IntegerValue(value))
    {
    }

    /// <summary>
    /// Creates a new instance of the ConstantValueWord class with a double value.
    /// </summary>
    /// <param name="value">A value.</param>
    public ConstantValueWord(double value)
        : this(new FloatValue(value))
    {
    }
    
    /// <summary>
    /// Creates a new instance of the ConstantValueWord class with a string value.
    /// </summary>
    /// <param name="value">A value.</param>
    public ConstantValueWord(string value)
        : this(new StringValue(value))
    {
    }


    /// <inheritdoc/>
    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackFree(1);

        interpreter.StackPush(_value);

        return 1;
    }

    private readonly IValue _value; 
}
