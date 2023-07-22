/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Stacks;


/// <summary>
/// Defines a stack.
/// </summary>
/// <typeparam name="T">A type of a stack item.</typeparam>
public interface IGenericStack<T> : IStack
{
    /// <summary>
    /// Items stored in this stack.
    /// </summary>
    T?[] Items { get; }

    /// <summary>
    /// The index of the top item.
    /// </summary>
    int Top { get; set; }


    /// <summary>
    /// Removes all items from this stack, replacing them with a default value.
    /// </summary>
    /// <param name="defaultValue">A default value.</param>
    void Init(T? defaultValue);
    
    /// <summary>
    /// Removes N itemss from the top of this stack.
    /// (a -- )
    /// </summary>
    /// <param name="n">The number of items to remove.</param>
    void Drop(int n = 1);

    /// <summary>
    /// Duplicates the topmost item on the stack.
    /// (a - a a)
    /// </summary>
    void Dup();

    /// <summary>
    /// Gets a item from the stack.
    /// </summary>
    /// <param name="index">A position of the item in the stack. 0 = the top of the stack.</param>
    /// <returns>An item from the stack.</returns>
    T? Pick(int index);
            
    /// <summary>
    /// Returns an item from the top of the stack.
    /// </summary>
    /// <returns>An item from the top of the stack.</returns>
    T? Peek();

    /// <summary>
    /// Removes an item from the top of the stack and returns it.
    /// (a -- )
    /// </summary>
    /// <returns>An item from the top of the stack.</returns>
    T? Pop();

    /// <summary>
    /// Pushes a value to the top of the stack.
    /// ( -- a)
    /// </summary>
    /// <param name="a">A value.</param>
    void Push(T? a);

    /// <summary>
    /// (a b -- a b a)
    /// </summary>
    void Over();

    /// <summary>
    /// (a b c -- b c a)
    /// </summary>
    void Rot();

    /// <summary>
    /// Rotates indexth item to the top.
    /// </summary>
    /// <param name="index">A stack item index, where 0 = stack top, 1 = first below top, etc.</param>
    void Roll(int index);

    /// <summary>
    /// Swaps the two top most items on the stack.
    /// (a b -- b a)
    /// </summary>
    void Swap();
}
