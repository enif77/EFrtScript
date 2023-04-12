/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;


internal class ParenWord : IWord
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

            c = currentInputSource.ReadChar();
        }

        if (c < 0)
        {
            interpreter.Throw(-39, "unexpected end of file, ) expected");
        }

        return 1;
    }
}

/*
 
https://forth-standard.org/standard/core/p 
 
(

Compilation:

Perform the execution semantics given below.

Execution:

( "ccc<paren>" -- )
Parse ccc delimited by ) (right parenthesis). ( is an immediate word.

The number of characters in ccc may be zero to the number of characters in the parse area.

Testing:

\ There is no space either side of the ).

T{ ( A comment)1234 -> 1234 }T
T{ : pc1 ( A comment)1234 ; pc1 -> 1234 }T 
  
 */