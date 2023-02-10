/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class CommentWord : IWord
{
    public string Name => "(";
    public bool IsImmediate => true;


    public void Execute(IInterpreter interpreter)
    {
        var c = interpreter.CurrentChar;
        while (c >= 0)
        {
            if (c == ')')
            {
                break;
            }

            c = interpreter.NextChar();
        }

        if (c < 0)
        {
            throw new Exception("A comment end expected");
        }
    }
}
