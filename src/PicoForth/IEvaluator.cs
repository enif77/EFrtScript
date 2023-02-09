/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface IEvaluator
{
    IOutputWriter OutputWriter { get; }
    

    void Eval(string src);


    #region source

    int CurrentChar { get; }

    int NextChar();
    string? ReadWordFromSource();
    string ReadStringFromSource();

    #endregion


    #region words

    bool IsWordRegistered(string wordName);
    void RegisterWord(IWord word);

    #endregion


    #region stack

    int StackDepth { get; }

    void StackClear();
    bool StackIsEmpty();
    IValue StackPeek();
    void StackPush(IValue v);
    IValue StackPop();

    #endregion


    #region return stack

    int ReturnStackDepth { get; }

    void ReturnStackClear();
    bool ReturnStackIsEmpty();
    IValue ReturnStackPeek();
    void ReturnStackPush(IValue v);
    IValue ReturnStackPop();

    #endregion


    #region heap

    void HeapStore(int address, IValue value);
    IValue HeapFetch(int address);

    #endregion
}
