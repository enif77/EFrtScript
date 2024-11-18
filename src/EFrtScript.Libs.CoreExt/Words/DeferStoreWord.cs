/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class DeferStoreWord : IWord
{
    public string Name => "DEFER!";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(2);

        var executionToken = interpreter.StackPopInteger();

        if (interpreter.IsWordRegistered(executionToken) == false)
        {
            throw new InterpreterException(-13, $"A word with the {executionToken} execution token not found.");
        }

        var registeredWord = interpreter.GetRegisteredWord(executionToken);
        if (registeredWord is not IDeferredWord deferredWord)
        {
            throw new InterpreterException(-13, $"The {executionToken} execution token does not point to a DEFERred word. it points to the '{registeredWord.Name}' word.");
        }

        deferredWord.SetDeferredWordBodyExecutionToken(interpreter.StackPopInteger());
        
        return 1;
    }
}

/*

https://forth-standard.org/standard/core/DEFERStore

DEFER!

( xt2 xt1 -- )
Set the word xt1 to execute xt2. An ambiguous condition exists if xt1 is not for a word defined by DEFER.

 */
 