namespace Shared.Configuration
{
    public class GatewayOptions
    {
        public bool IsShowExceptionsOnClient { get; set; }
        public bool IsMockCommunication { get; set; }
        public string AppDataPath { get; set; } = null!;
        public string? CommunicationStateEncryptionKey { get; set; }
        public bool EncryptState { get; set; }
    }
}
