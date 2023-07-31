/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScriptApp.Words;

using EFrtScript;
using EFrtScript.Extensions;


internal class ReadAllTextWord : IWord
{
    public string Name => "READ-ALL-TEXT";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        interpreter.StackPush(
            File.ReadAllText(
                interpreter.StackPop().String));

        return 1;
    }
}

/*

READ-ALL-TEXT

Reads a file into a string on stack.

*/