/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using System;


internal class CommentWord : IWord
{
    public string Name => "(";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        var currentInputSource = interpreter.CurrentInputSource!;
        
        var c = currentInputSource.CurrentChar;
        while (c >= 0)
        {
            if (c == ')')
            {
                break;
            }

            c = currentInputSource.NextChar();
        }

        if (c < 0)
        {
            throw new Exception("A comment end expected");
        }

        return 1;
    }
}
