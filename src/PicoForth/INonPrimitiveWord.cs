/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface INonPrimitiveWord : IWord
{
    int NextWordIndex { get; }

    IWord GetWord(int index);
    int AddWord(IWord word);
}
