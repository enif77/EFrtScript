/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface INonPrimitiveWord : IWord
{
    int AddWord(IWord word);
}
