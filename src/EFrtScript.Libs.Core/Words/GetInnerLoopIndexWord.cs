/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using System;

using EFrtScript.Extensions;


internal class GetInnerLoopIndexWord : IWord
{
    public string Name => "I";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        
        _ = interpreter.WordBeingDefined!.AddWord(
            new GetInnerLoopIndexControlWord());

        return 1;        
    }
}

/*

https://forth-standard.org/standard/core/I

I

*/