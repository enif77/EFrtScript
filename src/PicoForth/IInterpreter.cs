/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface IInterpreter
{
    IInterpreterState State { get; }
    IOutputWriter OutputWriter { get; }
    

    void Interpret(string src);


    #region source

    int CurrentChar { get; }

    int NextChar();
    string? ReadWordFromSource();
    string ReadStringFromSource();

    #endregion


    #region words

    bool IsCompiling { get; }
    INonPrimitiveWord? WordBeingDefined { get; }

    bool IsWordRegistered(string wordName);
    void RegisterWord(IWord word);

    void BeginNewWordCompilation(string wordName);
    void EndNewWordCompilation();

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
