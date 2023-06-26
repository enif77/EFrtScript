/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class QuestionFloatWord : IWord
{
    public string Name => "?FLOAT";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
        
        interpreter.StackPush(interpreter.StackPop().IsFloatingPointValue());

        return 1;
    }
}

/*

?FLOAT

(x -- flag)
Checks if x is a floating point value.

 */
