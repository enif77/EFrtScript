/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;


public interface INonPrimitiveWord : IWord
{
    int NextWordIndex { get; }

    IWord GetWord(int index);
    int AddWord(IWord word);
}
