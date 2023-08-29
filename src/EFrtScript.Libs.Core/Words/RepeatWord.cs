/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class RepeatWord : IWord
{
    public string Name => "REPEAT";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);

        // REPEAT word doesn't have a runtime behavior.

        interpreter.ReturnStackExpect(2);

        // Get the index of the next free slot in the non-primitive word being defined.
        var repeatIndex = interpreter.WordBeingDefined!.NextWordIndex;

        var controlWord = interpreter.WordBeingDefined!.GetWord(interpreter.ReturnStackPop().Integer);
        if (controlWord is WhileControlWord)
        {
            // We had a previous WHILE. Set the REPEAT index into
            // the WHILE control word.
            ((WhileControlWord)controlWord).SetRepeatIndex(repeatIndex);
        }
        else
        {
            throw new InterpreterException("REPEAT requires a previous WHILE.");
        }

        interpreter.WordBeingDefined!.AddWord(
            new RepeatControlWord(
                interpreter.ReturnStackPop().Integer - interpreter.WordBeingDefined.NextWordIndex));

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/REPEAT

REPEAT

Typical use:

: FACTORIAL ( +n1 -- +n2 )
   DUP 2 < IF DROP 1 EXIT THEN
   DUP
   BEGIN DUP 2 > WHILE
   1- SWAP OVER * SWAP
   REPEAT DROP
;

*/