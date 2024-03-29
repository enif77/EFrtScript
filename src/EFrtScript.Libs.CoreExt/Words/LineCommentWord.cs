/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;


internal class LineCommentWord : IWord
{
    public string Name => "\\";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        var currentInputSource = interpreter.CurrentInputSource!;
        
        currentInputSource.ReadChar();
        while (currentInputSource.CurrentChar >= 0)
        {
            if (currentInputSource.CurrentChar == '\n')
            {
                break;
            }

            currentInputSource.ReadChar();
        }

        return 1;
    }
}
