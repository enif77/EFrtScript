using System;


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


    #region heap

    void HeapStore(int address, IValue value);
    IValue HeapFetch(int address);

    #endregion
}