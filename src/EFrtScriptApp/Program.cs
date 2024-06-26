﻿/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScriptApp;

using System.IO;

using EFrtScript;
using EFrtScript.IO;
using EFrtScript.Extensions;

using EFrtScriptApp.Words;


internal static class Program
{
    private static void Main(string[] args)
    {
        var interpreter = new Interpreter(new ConsoleOutputWriter());

        new EFrtScript.Libs.Core.Library().Initialize(interpreter);
        new EFrtScript.Libs.CoreExt.Library().Initialize(interpreter);
        new EFrtScript.Libs.Exception.Library().Initialize(interpreter);
        new EFrtScript.Libs.String.Library().Initialize(interpreter);
        new EFrtScript.Libs.Tools.Library().Initialize(interpreter);

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

                if (interpreter.IsExecutionTerminated)
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

            if (interpreter.IsExecutionTerminated)
            {
                Console.WriteLine();
                Console.WriteLine("Bye!");

                break;
            }
        }

        // ev.Interpret("S\" --- Mics ---\" TYPE CR");

        // ev.Interpret("30 3 * 1 + 2 / . CR");
        // ev.Interpret("30 3 !  3 @ . CR");
        // ev.Interpret("( 30) 40 ( )3 !  3 @ . CR ( comment... )");

        // ev.Interpret("S\" aaa \" TYPE 3 . CR");
        // ev.Interpret(": StrBbb S\" bbb\" ; StrBbb TYPE CR");
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

dotnet publish -c Release --runtime win-x64 --force --self-contained true -p:PublishSingleFile=true
dotnet publish -c Release --runtime osx-x64 --force --self-contained true -p:PublishSingleFile=true
dotnet publish -c Release --runtime osx-arm64 --force --self-contained true -p:PublishSingleFile=true

dotnet publish -c Release --runtime win-x64 --force --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true

---

~/.../EFrtScript/src$ dotnet run --project ./EFrtScriptApp/EFrtScriptApp.csproj
~/.../EFrtScript/src$ dotnet run --project EfrtScriptApp/EfrtScriptApp.csproj ./EfrtScriptApp/Examples/hello-world.efrts

S" ./EfrtScriptApp/Examples/hello-world.efrts" READ-ALL-TEXT EVALUATE

pwsh run-examples.ps1

https://github.com/enif77/EFrt

---

BigInteger

https://learn.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=net-7.0

---

Integer number parsing 

https://forth-standard.org/standard/usage#usage:digits

https://forth-standard.org/standard/core/toNUMBER
 
https://stackoverflow.com/questions/65943008/c-biginteger-to-string-with-radix-specified

radix: 2 .. 36
 
  2: 0 .. 1
  3: 0 .. 2
  4: 0 .. 3 
  5: 0 .. 4
  6: 0 .. 5
  7: 0 .. 6
  8: 0 .. 7
  9: 0 .. 8
 10: 0 .. 9
 11: 0 .. 9, A
 12: 0 .. 9, A .. B
 ...
 16: 0 .. 9, A .. F
 ...
 36: 0 .. 9, A .. Z

---

https://forth-standard.org/standard/float

If the Floating-Point word set is present in the dictionary and the current base is DECIMAL, the input number-conversion
algorithm shall be extended to recognize floating-point numbers in this form:

Convertible string	:=	<significand><exponent>
<significand>	:=	[<sign>]<digits>[.<digits0>]
<exponent>	:=	E[<sign>]<digits0>
<sign>	:=	{ + | - }
<digits>	:=	<digit><digits0>
<digits0>	:=	<digit>*
<digit>	:=	{ 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 }

These are examples of valid representations of floating-point numbers in program source:

1E      1.E      1.E0      +1.23E-1      -1.23E+1

---

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