/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class QuestionDeferWord : IWord
{
    public string Name => "?DEFER";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        var executionToken = interpreter.StackPopInteger();

        if (interpreter.IsWordRegistered(executionToken) == false)
        {
            throw new InterpreterException(-13, $"The {executionToken} execution token not found.");
        }
        
        interpreter.StackPush(interpreter.GetRegisteredWord(executionToken) is IDeferredWord);
        
        return 1;
    }
}

/*

https://forth-standard.org/standard/core/DEFERFetch

?DEFER

( xt1 -- flag )
flag is true if the word with execution token xt1 is a deferred word.

 */
 