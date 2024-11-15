/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class DeferWord : IWord
{
    public string Name => "DEFER";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        var wordName = interpreter.CurrentInputSource!.ReadWord();
        if (string.IsNullOrEmpty(wordName))
        {
            throw new InterpreterException("A word name expected.");
        }

        interpreter.RegisterWord(
            new DeferControlWord(wordName.NormalizeWordName()));

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/DEFER

DEFER name

( -- )
Skip leading space delimiters. Parse name delimited by a space. Create a definition for name with the execution semantics defined below.

name Execution:
( i * x -- j * x )
Execute the xt that name is set to execute. An ambiguous condition exists if name has not been set to execute an xt.

 */
 