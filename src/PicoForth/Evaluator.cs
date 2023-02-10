/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;

using System.Text;

using PicoForth.Values;
using PicoForth.Words;


public class Evaluator : IEvaluator
{
    private const int DefaultStackSize = 10;
    private const int DefaultReturnStackSize = 10;
    private const int DefaultHeapSize = 64;


    public IOutputWriter OutputWriter { get; }

    
    public Evaluator(IOutputWriter outputWriter)
    {
        OutputWriter = outputWriter ?? throw new ArgumentNullException(nameof(outputWriter));

        _source = new StringSourceReader(string.Empty);
        _tokenizer = new Tokenizer(_source);
        _words = new Dictionary<string, IWord>();
        _stack = new Stack<IValue>(DefaultStackSize);
        _returnStack = new Stack<IValue>(DefaultReturnStackSize);
        _heap = new IValue[DefaultHeapSize];

        RegisterBuildInWords();
    }


    public void Eval(string src)
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
        }
    }


    private void CompileWord(string wordName)
    {
        if (IsWordRegistered(wordName))
        {
            var word = _words[wordName];
            if (word.IsImmediate)
            {
                word.Execute(this);
            }
            else if (word.Name == WordBeingDefined?.Name)
            {
                WordBeingDefined.AddWord(WordBeingDefined);
            }
            else
            {
                WordBeingDefined!.AddWord(word);
            }

            return;
        }

        if (int.TryParse(wordName, out var val))
        {
            WordBeingDefined!.AddWord(new ConstantValueWord(val));

            return;
        }

        throw new Exception($"Unknown word: {wordName}");
    }


    private void ExecuteWord(string wordName)
    {
        if (IsWordRegistered(wordName))
        {
            _words[wordName].Execute(this);

            return;
        }

        if (int.TryParse(wordName, out var val))
        {
            StackPush(new IntValue(val));

            return;
        }

        throw new Exception($"Unknown word: {wordName}");
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

    public bool IsCompiling { get; private set; }
    public INonPrimitiveWord? WordBeingDefined { get; private set; }


    public bool IsWordRegistered(string wordName)
        => string.IsNullOrWhiteSpace(wordName) == false && _words.ContainsKey(wordName);


    public void RegisterWord(IWord word)
        => _words.Add(word.Name.ToUpperInvariant(), word);


    public void BeginNewWordCompilation(string wordName)
    {
        if (IsCompiling)
        {
            throw new Exception("A word compilation is already running.");
        }

        WordBeingDefined = new NonPrimitiveWord(wordName);
        IsCompiling = true;
    }


    public void EndNewWordCompilation()
    {
        if (IsCompiling == false)
        {
            throw new Exception("Not in a new word compilation.");
        }

        RegisterWord(WordBeingDefined ?? throw new InvalidOperationException(nameof(WordBeingDefined) + " is null."));

        WordBeingDefined = null;
        IsCompiling = false;
    }


    private void RegisterBuildInWords()
    {
        RegisterWord(new CommentWord());

        RegisterWord(new BeginNewWordCompilationWord());
        RegisterWord(new EndNewWordCompilationWord());

        RegisterWord(new StoreWord());
        RegisterWord(new FetchWord());

        RegisterWord(new ToReturnStackWord());
        RegisterWord(new FromReturnStackWord());
        RegisterWord(new FetchReturnStackWord());

        RegisterWord(new ConditionalDuplicateWord());
        RegisterWord(new DropWord());
        RegisterWord(new DupWord());
        RegisterWord(new SwapWord());
        RegisterWord(new OverWord());
        RegisterWord(new RotWord());
        RegisterWord(new DepthWord());

        RegisterWord(new DotWord());
        RegisterWord(new CrWord());
        RegisterWord(new SpaceWord());
        RegisterWord(new SpacesWord());
        RegisterWord(new TypeWord());

        RegisterWord(new PlusWord());
        RegisterWord(new MinusWord());
        RegisterWord(new StarWord());
        RegisterWord(new SlashWord());

        RegisterWord(new EqualToZeroWord());
        RegisterWord(new EqualWord());
        RegisterWord(new GreaterThanWord());
        RegisterWord(new LessThanWord());
        RegisterWord(new LessThanZeroWord());

        RegisterWord(new PrintStringLitWord());
        RegisterWord(new PrintStringWord());
        RegisterWord(new StringLitWord());
    }

    #endregion


    #region stack

    private Stack<IValue> _stack;


    public int StackDepth => _stack.Count;


    public void StackClear()
        => _stack = new Stack<IValue>(DefaultStackSize);

    public bool StackIsEmpty()
        => _stack.Count == 0;

    public IValue StackPeek()
        => _stack.Peek();

    public void StackPush(IValue v)
        => _stack.Push(v);

    public IValue StackPop()
        => _stack.Pop();

    #endregion


    #region return stack

    private Stack<IValue> _returnStack;


    public int ReturnStackDepth => _returnStack.Count;
    

    public void ReturnStackClear()
        => _returnStack = new Stack<IValue>(DefaultReturnStackSize);

    public bool ReturnStackIsEmpty()
        => _returnStack.Count == 0;

    public IValue ReturnStackPeek()
        => _returnStack.Peek();

    public void ReturnStackPush(IValue v)
        => _returnStack.Push(v);

    public IValue ReturnStackPop()
        => _returnStack.Pop();

    #endregion


    #region heap

    private readonly IValue[] _heap;

    public void HeapStore(int address, IValue value)
        => _heap[address] = value;


    public IValue HeapFetch(int address)
        => _heap[address];

    #endregion
}
