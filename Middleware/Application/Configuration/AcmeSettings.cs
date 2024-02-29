#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Application.Configuration
{
	public class AcmeSettings
	{
		public string HostName { get; set; }
		public string CertStorage { get; set; }
		public bool GenerateSsl { get; set; }

		public string Pem => AcmePem.keyRelease;
	}
}
