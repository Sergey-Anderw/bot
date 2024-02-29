using MediatR;

namespace Application.Storage.Commands.Vision
{
    public class VisionCommand : IRequest
    {
        public required string ContainerName { get; set; }
        public required string UserQuery { get; set; }


    }
}
