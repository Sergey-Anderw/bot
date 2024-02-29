using Azure.Storage.Blobs;
using Domain.Enums;
using Domain.Models;
using MediatR;
using Newtonsoft.Json;

namespace Application.Storage.Commands.Upload
{
    internal class UploadCommandHandler(BlobServiceClient blobServiceClient) : IRequestHandler<UploadCommand>
    {
        public async Task<Unit> Handle(UploadCommand request, CancellationToken cancellationToken)
        {
            if (request.MetaData == null)
            {
                request.MetaData = new DocMetaData
                {
                    Name = "THE SPANISH CONSTITUTION",
                    AccessLevel = AccessLevel.Public,
                    Country = Country.Spanish.ToString(),
                    DocumentType = DocumentType.Constitution.ToString(),
                    Language = "en",
                    PublicationDate = DateTime.UtcNow,
                    Keywords = "basic law, human rights, government structure"
                };
            }
            var blobContainer = blobServiceClient.GetBlobContainerClient(request.ContainerName);
            var blobClient = blobContainer.GetBlobClient($"{request.FileName}.{request.FileFormat}");

            var json = JsonConvert.SerializeObject(request.MetaData);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            await blobClient.SetMetadataAsync(dictionary, cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}
