/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScriptScriptableApp;

using System.IO;

using EFrtScript;
using EFrtScript.IO;
using EFrtScript.Extensions;

internal static class Program
{
    static void Main(string[] args)
    {
        var interpreter = new Interpreter(new ConsoleOutputWriter());

        new EFrtScript.Libs.Core.Library().Initialize(interpreter);
        new EFrtScript.Libs.CoreExt.Library().Initialize(interpreter);
        new EFrtScript.Libs.Exception.Library().Initialize(interpreter);
        new EFrtScript.Libs.Tools.Library().Initialize(interpreter);
        
        Console.WriteLine("ScriptableApp is starting...");

        SetupAppState(interpreter);
        AddCustomWords(interpreter);

        // The app script...
        interpreter.Interpret(File.ReadAllText("Resources/app.efrts"));
    }
    
    private static void SetupAppState(IInterpreter interpreter)
    {
        AppState.AppName = "App";
        AppState.AppVersion = "0";

        AppState.VariableAdded += HandleAppStateVariableAdded;
        AppState.VariableValueUpdated += HandleAppStateVariableValueUpdated;
        AppState.VariableRemoved += HandleAppStateVariableRemoved;
    }


    #region event handlers

    private static void HandleAppStateVariableAdded(object? sender, AppStateEventArgs e)
    {
        Console.WriteLine($"The {e.VariableName} variable added with value: '{e.NewValue!.String}'.");
    }


    private static void HandleAppStateVariableValueUpdated(object? sender, AppStateEventArgs e)
    {
        Console.WriteLine($"The {e.VariableName} variable value: '{e.OldValue!.String}' updated to: '{e.NewValue!.String}'.");
    }


    private static void HandleAppStateVariableRemoved(object? sender, AppStateEventArgs e)
    {
        Console.WriteLine($"The {e.VariableName} variable removed. Its value was: '{e.OldValue!.String}'.");
    }

    #endregion


    // private const string AppNamePropertyName = "appname";
    // private const string AppVersionPropertyName = "appversion";
    // private const string DebugPropertyName = "debug";
    

    private static void AddCustomWords(IInterpreter interpreter)
    {
        // HELLO ( -- )
        interpreter.AddPrimitiveWord("HELLO", (i) => 
        {
            Console.WriteLine("Hello from programmable app!");

            return 1;
        });


        // READ-LINE ( -- string flag ior)
        interpreter.AddPrimitiveWord("READ-LINE", (i) => 
        {
            i.StackFree(3);

            var flag = -1;
            var ior = 0;
            string? s = null;
            try
            {
                s = Console.ReadLine();
                if (s == null)
                {
                    // We cannot return null as a EOF mark,
                    // so we flag it as false and return an empty string instead.
                    flag = 0;
                    s = string.Empty;
                }
            }
            catch (IOException ex)
            {
                s = ex.Message;
                flag = 0;
                ior = -71;  // READ-LINE
            }
            catch (OutOfMemoryException ex)
            {
                s = ex.Message;
                flag = 0;
                ior = -59;  // ALLOCATE
            }
            catch (ArgumentOutOfRangeException ex)
            {
                s = ex.Message;
                flag = 0;
                ior = -71;  // READ-LINE
            }

            i.StackPush(s!);
            i.StackPush(flag);
            i.StackPush(ior);

            return 1;
        });


        // DEBUG (value -- )
        interpreter.AddPrimitiveWord("DEBUG", (i) => 
        {
            i.StackExpect(1);

            var value = i.StackPop().String;
            if (AppState.DebugEnabled)
            {
                Console.WriteLine("Debug: {0}", value);
            }

            return 1;
        });


        // GET-APP-STATE-JSON ( -- string)
        interpreter.AddPrimitiveWord("GET-APP-STATE-JSON", (i) => 
        {
            i.StackFree(1);

            i.StackPush(AppState.ToJson());

            return 1;
        });

        // GET (var-name -- value)
        interpreter.AddPrimitiveWord("GET", (i) => 
        {
            i.StackExpect(1);

            // We store all variables in lowercase, so a user does not have to remember the correct variable name case.
            var normalizedVariableName = AppState.GetNormalizedVariableName(i.StackPop().String);

            // A direct property access example.
            // switch (normalizedVariableName)
            // {
            //     case AppNamePropertyName:
            //         interpreter.StackPush(_appState.AppName);
            //         break;

            //     case AppVersionPropertyName:
            //         interpreter.StackPush(_appState.AppVersion);
            //         break;

            //     case DebugPropertyName:
            //         interpreter.StackPush(_appState.DebugEnabled ? "true" : "false");
            //         break;

            //     default:
            //         if (_appState.HasVariable(normalizedVariableName) == false)
            //         {
            //             throw new InvalidOperationException($"Variable '{normalizedVariableName}' does not exists.");
            //         }

            //         interpreter.StackPush(_appState.Get(normalizedVariableName)!);
            //         break;
            // }

            if (AppState.StateProperties.TryGetValue(normalizedVariableName, out var property))
            {
                // bool, string, int, float, double, decimal
                switch (property.PropertyType.Name)
                {
                    case "String":
                        i.StackPush((string)property.GetValue(AppState)!);
                        break;

                    case "Boolean":
                        i.StackPush((bool)property.GetValue(AppState)!);
                        break;

                    case "Int32":
                        i.StackPush((int)property.GetValue(AppState)!);
                        break;

                    case "Single":
                        i.StackPush((float)property.GetValue(AppState)!);
                        break;

                    case "Double":
                        i.StackPush((double)property.GetValue(AppState)!);
                        break;

                    case "Decimal":
                        i.StackPush((double)(decimal)property.GetValue(AppState)!);
                        break;

                    default:
                        throw new InvalidOperationException($"Property '{property.Name}' has unsupported type '{property.PropertyType.Name}'.");
                }
            }
            else
            {
                if (AppState.HasVariable(normalizedVariableName) == false)
                {
                    throw new InvalidOperationException($"Variable '{normalizedVariableName}' does not exists.");
                }

                i.StackPush(AppState.Get(normalizedVariableName)!);
            }

            return 1;
        });

        // SET (value var-name -- )
        interpreter.AddPrimitiveWord("SET", (i) => 
        {
            i.StackExpect(2);

            var normalizedVariableName = AppState.GetNormalizedVariableName(i.StackPop().String);

            // // A direct property access example.
            // switch (normalizedVariableName)
            // {
            //     case AppNamePropertyName:
            //         _appState.AppName = interpreter.StackPop().String;
            //         break;

            //     case AppVersionPropertyName:
            //         _appState.AppVersion = interpreter.StackPop().String;
            //         break;

            //     case DebugPropertyName:
            //         _appState.DebugEnabled = interpreter.StackPop().Boolean;
            //         break;

            //     default:
            //         _appState.Set(normalizedVariableName, interpreter.StackPop());
            //         break;
            // }

            if (AppState.StateProperties.TryGetValue(normalizedVariableName, out var property))
            {
                // bool, string, int, float, double, decimal
                switch (property.PropertyType.Name)
                {
                    case "String":
                        property.SetValue(AppState, i.StackPop().String);
                        break;

                    case "Boolean":
                        property.SetValue(AppState, i.StackPop().Boolean);
                        break;

                    case "Int32":
                        property.SetValue(AppState, i.StackPop().Integer);
                        break;

                    case "Single":
                        property.SetValue(AppState, (float)i.StackPop().Float);
                        break;

                    case "Double":
                        property.SetValue(AppState, i.StackPop().Float);
                        break;

                    case "Decimal":
                        property.SetValue(AppState, (decimal)i.StackPop().Float);
                        break;

                    default:
                        throw new InvalidOperationException($"Property '{property.Name}' has unsupported type '{property.PropertyType.Name}'.");
                }
            }
            else 
            {
                AppState.Set(normalizedVariableName, i.StackPop());
            }

            return 1;
        });


        // REMOVE-VARIABLE (var-name -- )
        interpreter.AddPrimitiveWord("REMOVE-VARIABLE", (i) => 
        {
            i.StackExpect(1);

            AppState.Set(AppState.GetNormalizedVariableName(i.StackPop().String), null);

            return 1;
        });


        // INCLUDE-SCRIPT (script-path -- )
        interpreter.AddPrimitiveWord("INCLUDE-SCRIPT", (i) => 
        {
            i.StackExpect(1);

            var scriptPath = i.StackPop().String;
            if (File.Exists(scriptPath) == false)
            {
                throw new Exception($"Script '{scriptPath}' does not exist.");
            }

            i.Interpret(File.ReadAllText(scriptPath));

            return 1;
        });

        // EXECUTE-COMMAND (command -- exit-code)
        interpreter.AddPrimitiveWord("EXECUTE-COMMAND", (i) => 
        {
            i.StackExpect(1);

            var command = i.StackPop().String;
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.OutputDataReceived += (sender, e) =>
            {
                if (string.IsNullOrEmpty(e.Data))
                {
                    return;
                }

                i.Output.WriteLine($"   {e.Data}");
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (string.IsNullOrEmpty(e.Data))
                {
                    return;
                }
                
                i.Output.WriteLine($"E: {e.Data}");
            };

            try
            {
                process.Start();

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();

                i.StackPush(process.ExitCode); 
            }
            finally
            {
                process.Close();    
            }

            return 1;
        });
    } 
    

    private static readonly AppState AppState = new AppState();
}
