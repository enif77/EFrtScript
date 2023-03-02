/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;


internal class LineCommentWord : IWord
{
    public string Name => "\\";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        var currentInputSource = interpreter.CurrentInputSource!;
        
        currentInputSource.NextChar();
        while (currentInputSource.CurrentChar >= 0)
        {
            if (currentInputSource.CurrentChar == '\n')
            {
                break;
            }

            currentInputSource.NextChar();
        }

        return 1;
    }
}
