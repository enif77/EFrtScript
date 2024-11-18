/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class DeferFetchWord : IWord
{
    public string Name => "DEFER@";
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

        var registeredWord = interpreter.GetRegisteredWord(executionToken);
        if (registeredWord is not IDeferredWord deferredWord)
        {
            throw new InterpreterException(-13, $"The {executionToken} execution token does not point to a DEFERred word. it points to the '{registeredWord.Name}' word.");
        }

        // This may return an execution token of an unknown word, if the DEFERred word is not set.
        interpreter.StackPush(deferredWord.DeferredWordBodyExecutionToken);
        
        return 1;
    }
}

/*

https://forth-standard.org/standard/core/DEFERFetch

DEFER@

( xt1 -- xt2 )
xt2 is the execution token xt1 is set to execute. An ambiguous condition exists if xt1 is not the execution token
of a word defined by DEFER, or if xt1 has not been set to execute a xt.

 */
 