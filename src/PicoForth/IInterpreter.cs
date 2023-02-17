/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface IInterpreter
{
    IInterpreterState State { get; }
    IOutputWriter Output { get; }
    

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


    #region heap

    void HeapStore(int address, IValue value);
    IValue HeapFetch(int address);

    #endregion
}
