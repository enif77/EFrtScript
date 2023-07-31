/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using System;

using EFrtScript.Extensions;


internal class ElseWord : IWord
{
    public string Name => "ELSE";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        interpreter.ReturnStackExpect(1);

        var ifControlWord = interpreter.WordBeingDefined!.GetWord(interpreter.ReturnStackPeek().Integer);
        if (ifControlWord is IfControlWord)
        {
            // Get the index past where ELSE will be.
            var indexFollowingElse = interpreter.WordBeingDefined!.NextWordIndex + 1;

            // Instantiate the ELSE runtime code passing the index following ELSE.
            // Push execute address of ELSE word onto control flow stack.
            interpreter.ReturnStackFree(1);
            interpreter.ReturnStackPush(
                interpreter.WordBeingDefined!.AddWord(
                    new ElseControlWord(indexFollowingElse)));

            // Inform the if control word of this index as well
            ((IfControlWord)ifControlWord).SetElseIndex(indexFollowingElse);
        }
        else
        {
            throw new Exception("ELSE requires a previous IF.");
        }

        return 1;
    }
}
