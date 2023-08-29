/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class WhileWord : IWord
{
    public string Name => "WHILE";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);

        interpreter.ReturnStackExpect(1);

        interpreter.ReturnStackPush(
            interpreter.WordBeingDefined!.AddWord(
                new WhileControlWord(interpreter.WordBeingDefined.NextWordIndex)));

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/WHILE

WHILE

*/