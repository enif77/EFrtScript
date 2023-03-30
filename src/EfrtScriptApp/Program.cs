/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScriptApp;

using System.IO;

using EFrtScript;
using EFrtScript.IO;
using EFrtScript.Extensions;
using EFrtScript.Libs.Core;

using EFrtScriptApp.Words;


internal static class Program
{
    private static void Main(string[] args)
    {
        var interpreter = new Interpreter(new ConsoleOutputWriter());

        var coreLib = new Library();
        coreLib.Initialize(interpreter);

        interpreter.RegisterWord(new ReadAllTextWord());

        interpreter.AddPrimitiveWord("TRACE", (IInterpreter i) => 
        {
            i.StackExpect(1);

            _tracing = i.StackPop().Boolean;

            return 1;
        });

        interpreter.ExecutingWord += Interpreter_ExecutingWord;
        

        var interactiveModeRequested = true;

        if (args.Length > 0)
        {
            interactiveModeRequested = false;

            foreach (var arg in args)
            {
                if (arg.ToLowerInvariant() == "--interactive")
                {
                    interactiveModeRequested = true;

                    continue;
                }

                try
                {
                    interpreter.Interpret(File.ReadAllText(arg));
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);

                    continue;
                }

                if (interpreter.InterpreterState == InterpreterStateCode.Terminating)
                {
                    Console.WriteLine();
                    Console.WriteLine("Bye!");

                    break;
                }
            }
        }

        if (interactiveModeRequested == false)
        {
            return;
        }

        while (true)
        {
            try
            {
                // Show a prompt.
                Console.Write(interpreter.IsCompiling ? ":> " : "-> ");

                // Interpret.
                interpreter.Interpret(Console.ReadLine() ?? string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            if (interpreter.InterpreterState == InterpreterStateCode.Terminating)
            {
                Console.WriteLine();
                Console.WriteLine("Bye!");

                break;
            }
        }

        // ev.Interpret("S\" --- Mics ---\" S. CR");

        // ev.Interpret("30 3 * 1 + 2 / . CR");
        // ev.Interpret("30 3 !  3 @ . CR");
        // ev.Interpret("( 30) 40 ( )3 !  3 @ . CR ( comment... )");

        // ev.Interpret("S\" aaa \" S. 3 . CR");
        // ev.Interpret(": StrBbb S\" bbb\" ; StrBbb S. CR");
    }


    private static bool _tracing = false;


    private static void Interpreter_ExecutingWord(object? sender, InterpreterEventArgs e)
    {
        if (_tracing)
        {
            Console.WriteLine("Trace: {0} ", e.Word.Name);
        }
    }
}

/*

~/.../EFrtScript/src$ dotnet run --project ./EFrtScriptApp/EFrtScriptApp.csproj
~/.../EFrtScript/src$ dotnet run --project EfrtScriptApp/EfrtScriptApp.csproj ./EfrtScriptApp/Examples/hello-world.efrts

S" ./EfrtScriptApp/Examples/hello-world.efrts" READ-ALL-TEXT EVALUATE

pwsh run-examples.ps1

https://github.com/enif77/EFrt


INT -> converts a value on the stack to integer.
?INT -> returns true, if a value on the stack is integer.

FLOAT -> converts a value on the stack to floating point.
?FLOAT -> returns true, if a value on the stack is floating point. 

STRING -> converts a value on the stack to string.
?STRING -> returns true, if a value on the stack is string.

BEGIN ?DO +LOOP I J LEAVE REPEAT UNLOOP UNTIL WHILE AGAIN 

COUNT

>NUMBER S>D
,
2DROP 2DUP 2OVER 2SWAP
LSHIFT RSHIFT MAX MIN MODE
+! 2! 2@
"* /" "* /MOD" /MOD 1+ 1- 2* 2/ FM/MOD INVERT M* SM/REM
AND OR XOR
QUIT

---

THROW code assignments
 
-1 ABORT
-2 ABORT"
-3 stack overflow
-4 stack underflow
-5 return stack overflow
-6 return stack underflow
-7 do-loops nested too deeply during execution
-8 dictionary overflow
-9 invalid memory address
-10 division by zero
-11 result out of range
-12 argument type mismatch
-13 undefined word
-14 interpreting a compile-only word
-15 invalid FORGET
-16 attempt to use zero-length string as a name
-17 pictured numeric output string overflow
-18 parsed string overflow
-19 definition name too long
-20 write to a read-only location
-21 unsupported operation (e.g., AT-XY on a too-dumb terminal)
-22 control structure mismatch
-23 address alignment exception
-24 invalid numeric argument
-25 return stack imbalance
-26 loop parameters unavailable
-27 invalid recursion
-28 user interrupt
-29 compiler nesting
-30 obsolescent feature
-31 >BODY used on non-CREATEd definition
-32 invalid name argument (e.g., TO name)
-33 block read exception
-34 block write exception
-35 invalid block number
-36 invalid file position
-37 file I/O exception
-38 non-existent file
-39 unexpected end of file
-40 invalid BASE for floating point conversion
-41 loss of precision
-42 floating-point divide by zero
-43 floating-point result out of range
-44 floating-point stack overflow
-45 floating-point stack underflow
-46 floating-point invalid argument
-47 compilation word list deleted
-48 invalid POSTPONE
-49 search-order overflow
-50 search-order underflow
-51 compilation word list changed
-52 control-flow stack overflow
-53 exception stack overflow
-54 floating-point underflow
-55 floating-point unidentified fault
-56 QUIT
-57 exception in sending or receiving a character
-58 [IF], [ELSE], or [THEN] exception
-59 ALLOCATE
-60 FREE
-61 RESIZE
-62 CLOSE-FILE
-63 CREATE-FILE
-64 DELETE-FILE
-65 FILE-POSITION
-66 FILE-SIZE
-67 FILE-STATUS
-68 FLUSH-FILE
-69 OPEN-FILE
-70 READ-FILE
-71 READ-LINE
-72 RENAME-FILE
-73 REPOSITION-FILE
-74 RESIZE-FILE
-75 WRITE-FILE
-76 WRITE-LINE
-77 Malformed xchar
-78 SUBSTITUTE
-79 REPLACES 
 
 */