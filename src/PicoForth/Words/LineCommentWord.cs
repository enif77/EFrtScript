/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class LineCommentWord : IWord
{
    public string Name => "\\";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.NextChar();
        while (interpreter.CurrentChar >= 0)
        {
            if (interpreter.CurrentChar == '\n')
            {
                interpreter.NextChar();

                break;
            }

            interpreter.NextChar();
        }

        return 1;
    }
}
