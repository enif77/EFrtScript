/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class QuestionStringWord : IWord
{
    public string Name => "?STRING";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
        
        interpreter.StackPush(interpreter.StackPop().IsStringValue() ? -1 : 0);

        return 1;
    }
}

/*

?STRING

(x -- flag)
Checks if x is a string value.

 */
