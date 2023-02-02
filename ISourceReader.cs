public interface ISourceReader
{
    int CurrentChar { get; }

    int NextChar();
}
