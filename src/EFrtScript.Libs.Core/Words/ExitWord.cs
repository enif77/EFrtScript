/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class ExitWord : IWord
{
    public string Name => "EXIT";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);

        // EXIT word doesn't have a runtime behavior.

        interpreter.WordBeingDefined!.AddWord(new ExitControlWord(interpreter.WordBeingDefined));

        return 1;
    }
}

/*

src/EFrtScript.Libs.Core/Words/RepeatWord.cs

EXIT

( -- ) ( R: nest-sys -- )
Return control to the calling definition specified by nest-sys.
Before executing EXIT within a do-loop, a program shall discard the loop-control parameters by executing UNLOOP.

*/
