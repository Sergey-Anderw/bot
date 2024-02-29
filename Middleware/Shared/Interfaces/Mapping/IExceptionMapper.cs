using Shared.Exceptions;

namespace Shared.Interfaces.Mapping
{
    public interface IExceptionMapper
    {
        ExceptionInfo Map(Exception exception);
    }
}