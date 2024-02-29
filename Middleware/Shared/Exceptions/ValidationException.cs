using FluentValidation.Results;
using Master.Domain.Exceptions;

namespace Shared.Exceptions
{
    [Serializable]
    public sealed class ValidationException : DomainException
    {
        public ValidationException()
            : this("One or more validation failures have occurred.")
        {
        }

        public ValidationException(string message)
            : base(message)
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(List<ValidationFailure> failures)
            : this("One or more validation failures have occurred.")
        {
            if (failures != null)
            {
                var propertyNames = failures
                    .Select(e => e.PropertyName)
                    .Distinct();

                foreach (var propertyName in propertyNames)
                {
                    var propertyFailures = failures
                        .Where(e => e.PropertyName == propertyName)
                        .Select(e => e.ErrorMessage)
                        .ToArray();

                    Failures.Add(propertyName, propertyFailures);
                }
            }

            foreach (var kv in Failures)
            {
                Data.Add(kv.Key, kv.Value);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}