namespace Shared.Conversion
{
    public delegate bool TryParseMethod<T>(string input, out T value);
}