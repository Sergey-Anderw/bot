using FluentValidation;

namespace Application.Storage.Commands.Upload
{
    internal class UploadCommandValidator : AbstractValidator<UploadCommand>
    {
        public UploadCommandValidator()
        {
            RuleFor(o => o.ContainerName).NotEmpty();
        }
    }
}
