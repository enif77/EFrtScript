/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class QuestionDupeWord : IWord
{
    public string Name => "?DUP";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        var v = interpreter.StackPeek();
        if (v.Boolean)
        {
            interpreter.StackFree(1);
            interpreter.StackPush(v);
        }
        else
        {
            interpreter.StackPop();
            interpreter.StackPush(0);
        }

        return 1;
    }
}

/*
 
https://forth-standard.org/standard/core/qDUP

?DUP

( x -- 0 | x x )

Duplicate x if it is non-zero.

Testing:

T{ -1 ?DUP -> -1 -1 }T
T{  0 ?DUP ->  0    }T
T{  1 ?DUP ->  1  1 }T 
 
 */