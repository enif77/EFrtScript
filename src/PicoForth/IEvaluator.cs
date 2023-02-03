/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface IEvaluator
{
    void Eval(string src);


    #region source

    int CurrentChar { get; }

    int NextChar();
    string ReadStringFromSource();

    #endregion


    #region words

    bool IsWordRegistered(string wordName);
    void RegisterWord(IWord word);

    #endregion


    #region stack

    void StackClear();
    bool StackIsEmpty();
    IValue StackPeek();
    void StackPush(IValue v);
    IValue StackPop();

    #endregion


    #region return stack

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
