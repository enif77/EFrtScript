/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using System;

using EFrtScript.Extensions;


internal class GetOuterLoopIndexWord : IWord
{
    public string Name => "J";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        
        _ = interpreter.WordBeingDefined!.AddWord(
            new GetOuterLoopIndexControlWord());

        return 1;        
    }
}

/*

https://forth-standard.org/standard/core/J

J

*/
