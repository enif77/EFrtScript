/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class QuestionDoWord : IWord
{
    public string Name => "?DO";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);

        interpreter.ReturnStackFree(1);

        interpreter.ReturnStackPush(
            interpreter.WordBeingDefined!.AddWord(
                new QuestionDoControlWord(interpreter.WordBeingDefined!.NextWordIndex)));

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/qDO

?DO

(R: -- dest)


Example:

: qd ?DO I . CR LOOP ;

789 789 qd      \ -> 
-9876 -9876 qd  \ -> 
5 0 qd          \ -> 0 1 2 3 4

 */
