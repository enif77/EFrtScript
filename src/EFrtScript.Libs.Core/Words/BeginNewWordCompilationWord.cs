/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using System;


internal class BeginNewWordCompilationWord : IWord
{
    public string Name => ":";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.BeginNewWordCompilation(
            interpreter.CurrentInputSource!.ReadWord() ?? throw new Exception("A new word name expected."));

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/Colon

:

( -- )
Skip leading space delimiters. Parse name delimited by a space. Create a definition for name, called a "colon definition".
Enter compilation state and start the current definition, producing colon-sys.

The execution semantics of name will be determined by the words compiled into the body of the definition.
The current definition shall not be findable in the dictionary until it is ended.

 */
 