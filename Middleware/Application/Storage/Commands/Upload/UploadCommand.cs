using Domain.Models;
using MediatR;

namespace Application.Storage.Commands.Upload
{
    public class UploadCommand : IRequest
    {
        public required string ContainerName { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DocMetaData? MetaData { get; set; }
        public required string FileFormat { get; set; }
    }
}
