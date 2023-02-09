/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface INonPrimitiveWord : IWord
{
    void AddWord(IWord word);
}
