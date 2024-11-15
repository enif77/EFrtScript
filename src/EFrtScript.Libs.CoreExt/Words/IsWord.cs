/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


public class IsWord : IWord
{
    public string Name => "IS";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

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

        var registeredWord = interpreter.GetRegisteredWord(normalizedWordName);
        if (registeredWord is not IDeferredWord deferredWord)
        {
            throw new InterpreterException(-13, $"The '{registeredWord.Name}' word is not a DEFERred word.");
        }

        deferredWord.SetDeferredWordBodyExecutionToken(interpreter.StackPopInteger());

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/IS

IS

Interpretation:

( xt "<spaces>name" -- )
Skip leading spaces and parse name delimited by a space. Set name to execute xt.

An ambiguous condition exists if name was not defined by DEFER.


Compilation:

( "<spaces>name" -- )
Skip leading spaces and parse name delimited by a space. Append the run-time semantics given below to the current definition.
An ambiguous condition exists if name was not defined by DEFER.


Run-time:

( xt -- )
Set name to execute xt.

An ambiguous condition exists if POSTPONE, [COMPILE], ['] or ' is applied to IS.


Testing:

T{ DEFER defer5 -> }T
T{ : is-defer5 IS defer5 ; -> }T
T{ ' * IS defer5 -> }T
T{ 2 3 defer5 -> 6 }T

T{ ' + is-defer5 -> }T
T{ 1 2 defer5 -> 3 }T

*/
