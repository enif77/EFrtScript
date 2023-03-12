/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using EFrtScript.IO;


internal static class TestsHelper
{
    public static IInterpreter CreateInterpreter()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        var coreLib = new Library();
        coreLib.Initialize(interpreter);

        return interpreter;
    }
}
