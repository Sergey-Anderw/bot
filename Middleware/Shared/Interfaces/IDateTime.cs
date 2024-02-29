namespace MIH.Shared.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
        int CurrentYear { get; }
    }
}
