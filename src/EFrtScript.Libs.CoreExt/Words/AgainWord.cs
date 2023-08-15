/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class AgainWord : IWord
{
    public string Name => "AGAIN";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);

        interpreter.ReturnStackExpect(1);

        interpreter.WordBeingDefined!.AddWord(
            new AgainControlWord(
                interpreter.ReturnStackPop().Integer - interpreter.WordBeingDefined!.NextWordIndex));

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/AGAIN

AGAIN

(R: dest -- )
Continue execution at the location specified by dest.

 */
