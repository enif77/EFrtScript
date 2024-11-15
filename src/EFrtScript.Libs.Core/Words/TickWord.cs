/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class TickWord : IWord
{
    public string Name => "'";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackFree(1);

        var wordName = interpreter.CurrentInputSource!.ReadWord();
        if (string.IsNullOrEmpty(wordName))
        {
            throw new InterpreterException("A word name expected.");
        }

        var normalizedWordName = wordName.NormalizeWordName();

        if (interpreter.IsWordRegistered(normalizedWordName) == false)
        {
            throw new InterpreterException($"The word '{wordName}' is not registered.");
        }

        interpreter.StackPush(interpreter.GetRegisteredWord(normalizedWordName).ExecutionToken);

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/Tick

'

( "<spaces>name" -- xt )
Skip leading space delimiters. Parse name delimited by a space. Find name and return xt,
the execution token for name. An ambiguous condition exists if name is not found. When interpreting,
' xyz EXECUTE is equivalent to xyz.

Testing:

T{ : GT1 123 ;   ->     }T
T{ ' GT1 EXECUTE -> 123 }T

*/