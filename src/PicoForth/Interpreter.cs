/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;

using System.Text;

using PicoForth.Values;
using PicoForth.Words;


public class Interpreter : IInterpreter
{
    public IInterpreterState State { get; }
    public IOutputWriter Output { get; }

    
    public Interpreter(IOutputWriter outputWriter)
    {
        State = new InterpreterState();
        Output = outputWriter ?? throw new ArgumentNullException(nameof(outputWriter));

        _source = new StringSourceReader(string.Empty);
        _tokenizer = new Tokenizer(_source);
        _words = new Dictionary<string, IWord>();
    }


    public void Interpret(string src)
    {
        _source = new StringSourceReader(src);
        _tokenizer = new Tokenizer(_source);
        
        while (true)
        {
            var word = _tokenizer.NextWord();
            if (word == null)
            {
                break;
            }

            var wordName = word.ToUpperInvariant();

            if (IsCompiling)
            {
                CompileWord(wordName);
            }
            else
            {
                ExecuteWord(wordName);
            }
            
            if (IsExecutionTerminated)
            {
                break;
            }
        }
        
        // Execution broken. Return to interpreting mode.
        if (InterpreterState == InterpreterStateCode.Breaking)
        {
            InterpreterState = InterpreterStateCode.Interpreting;
            //State.SetStateValue(false);
        }
            
        // // Restore the previous input source, if any.
        // InputSource = (State.InputSourceStack.Count > 0) 
        //     ? State.InputSourceStack.Pop() 
        //     : null;
    }


    private void CompileWord(string wordName)
    {
        if (IsWordRegistered(wordName))
        {
            var word = _words[wordName];
            if (word.IsImmediate)
            {
                _currentWord = _words[wordName];
                
                try
                {
                    ExecuteWord(_currentWord);
                }
                catch (InterpreterException ex)
                {
                    Throw(ex.ExceptionCode, ex.Message);
                }
                catch (Exception ex)
                {
                    Throw(-100, ex.Message);
                }
            }
            else
            {
                WordBeingDefined!.AddWord(word);
            }

            return;
        }
        
        if (wordName == WordBeingDefined?.Name)
        {
            // Recursive call of the currently defined word.
            WordBeingDefined.AddWord(WordBeingDefined);
            
            return;
        }
        
        if (int.TryParse(wordName, out var val))
        {
            WordBeingDefined!.AddWord(new ConstantValueWord(val));

            return;
        }

        Throw(-13, $"The '{wordName}' word is undefined.");
    }


    private void ExecuteWord(string wordName)
    {
        if (IsWordRegistered(wordName))
        {
            _currentWord = _words[wordName];

            try
            {
                ExecuteWord(_currentWord);
            }
            catch (InterpreterException ex)
            {
                Throw(ex.ExceptionCode, ex.Message);
            }
            catch (Exception ex)
            {
                Throw(-100, ex.Message);
            }
            
            return;
        }

        if (int.TryParse(wordName, out var val))
        {
            State.Stack.Push(new IntValue(val));

            return;
        }

        Throw(-13, $"The '{wordName}' word is undefined.");
    }


    #region source

    private ISourceReader _source;
    private Tokenizer _tokenizer;


    public int CurrentChar => _source.CurrentChar;


    public int NextChar()
    {
        return _source.NextChar();
    }


    public string? ReadWordFromSource()
    {
        return _tokenizer.NextWord();
    }


    public string ReadStringFromSource()
    {
        var sb = new StringBuilder();
        var c = NextChar();  // Skip the white-space behind the string literal opening word (S., .", ...).
        while (c >= 0)
        {
            if (c == '"')
            {
                break;
            }

            sb.Append((char)c);

            c = NextChar();
        }

        if (c < 0)
        {
            throw new Exception("A string literal end expected");
        }

        return sb.ToString();
    }

    #endregion


    #region words

    private readonly IDictionary<string, IWord> _words;
    
    public INonPrimitiveWord? WordBeingDefined { get; private set; }


    public bool IsWordRegistered(string wordName)
        => string.IsNullOrWhiteSpace(wordName) == false && _words.ContainsKey(wordName);

    
    public IWord GetRegisteredWord(string wordName)
    {
        if (IsWordRegistered(wordName) == false)
        {
            Throw(-13, $"The '{wordName}' word is undefined.");
        }

        return _words[wordName];
    }

    
    public void RegisterWord(IWord word)
        => _words.Add(word.Name.ToUpperInvariant(), word);


    public void BeginNewWordCompilation(string wordName)
    {
        if (IsCompiling)
        {
            throw new Exception("A word compilation is already running.");
        }

        WordBeingDefined = new NonPrimitiveWord(wordName);
        InterpreterState = InterpreterStateCode.Compiling;
    }


    public void EndNewWordCompilation()
    {
        if (IsCompiling == false)
        {
            throw new Exception("Not in a new word compilation.");
        }

        RegisterWord(WordBeingDefined ?? throw new InvalidOperationException(nameof(WordBeingDefined) + " is null."));

        WordBeingDefined = null;
        InterpreterState = InterpreterStateCode.Interpreting;
    }

    #endregion


    #region execution

    public InterpreterStateCode InterpreterState { get; private set; }
    public bool IsCompiling => InterpreterState == InterpreterStateCode.Compiling;
    public bool IsExecutionTerminated => InterpreterState == InterpreterStateCode.Breaking || InterpreterState == InterpreterStateCode.Terminating;


    /// <summary>
    /// The currently running word.
    /// </summary>
    private IWord? _currentWord;


    public int ExecuteWord(IWord word)
    {
        if (word == null) throw new ArgumentNullException(nameof(word));

        //ExecutingWord?.Invoke(this, new InterpreterEventArgs() { Word = word });

        // try
        // {
            return word.Execute(this);
        // }
        // finally
        // {
        //     WordExecuted?.Invoke(this, new InterpreterEventArgs() { Word = word });
        // }
    }
    

    public void Reset()
    {
        State.Reset();
        InterpreterState = InterpreterStateCode.Interpreting;
    }
    
    
    public void Abort()
    {
        State.Stack.Clear();
        //State.InputSourceStack.Clear();

        // TODO: Clear the heap?
        // TODO: Clear the exception stack?

        Quit();
    }


    public void Quit()
    {
        State.ReturnStack.Clear();
        InterpreterState = InterpreterStateCode.Breaking;
    }
    
    
    public void Throw(int exceptionCode, string? message = null)
    {
        if (exceptionCode == 0)
        {
            return;
        }

        if (State.ExceptionStack.IsEmpty)
        {
            switch (exceptionCode)
            {
                case -1: break;
                case -2: Output.WriteLine(message ?? "Execution aborted!"); break;
                default:
                    Output.WriteLine($"Execution aborted: [{exceptionCode}] {message ?? string.Empty}");
                    break;
            }

            Abort();

            return;
        }

        // Restore the previous execution state.
        var exceptionFrame = State.ExceptionStack.Pop();

        State.Stack.Top = exceptionFrame!.StackTop;
        State.ReturnStack.Top = exceptionFrame.ReturnStackTop;
        //State.InputSourceStack.Top = exceptionFrame.InputSourceStackTop;
        _currentWord = exceptionFrame.ExecutingWord ?? throw new InvalidOperationException("Exception frame without a executing word reference.");

        State.Stack.Push(new IntValue(exceptionCode));

        // Will be caught by the CATCH word.
        throw new InterpreterException(exceptionCode, message);
    }
    
    
    public void TerminateExecution()
    {
        InterpreterState = InterpreterStateCode.Terminating;
    }

    #endregion
}
