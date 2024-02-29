namespace Shared.Interfaces
{
    public interface ICloneable<out T> : ICloneable where T : class
    {
        new T Clone();
    }
}