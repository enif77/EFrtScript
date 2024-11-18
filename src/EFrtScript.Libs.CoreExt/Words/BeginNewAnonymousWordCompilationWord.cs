/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;


internal class BeginNewAnonymousWordCompilationWord : IWord
{
    public string Name => ":NONAME";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.BeginNewAnonymousWordCompilation();

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/ColonNONAME

:NONAME

( -- xt )
Like the : word, but creates an anonymous definition. The execution semantics of the new definition are the same
as those of :. It pushes the execution token for the new definition onto the stack. 

Typical use:

DEFER print
:NONAME ( n -- ) . ; IS print

 */
 