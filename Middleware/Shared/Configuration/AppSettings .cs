namespace Shared.Configuration
{
    public static class SharedAppSettings
    {
        public static bool EncryptState { get; set; }
        public static string? CommunicationStateEncryptionKey { get; set; }
    }
}
