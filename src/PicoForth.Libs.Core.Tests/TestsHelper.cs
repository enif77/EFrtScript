/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Tests;

using PicoForth.IO;


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
