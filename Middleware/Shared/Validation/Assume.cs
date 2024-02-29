using JetBrains.Annotations;
using Shared.Conversion;

namespace Shared.Validation
{
    public static class Assume
    {
        private const string ArgumentIsInvalid = "Argument is invalid.";

        [AssertionMethod]
        public static void Id(int id, string parameterName)
        {
            if (!ValidationHelper.Id(id))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void Id(int? id, string parameterName)
        {
            if (!ValidationHelper.Id(id))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void Id(Guid id, string parameterName)
        {
            if (!ValidationHelper.Guid(id))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void PositiveNullableDecimal(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
            {
                if (!ValidationHelper.Positive((decimal?)null))
                    throw new ArgumentException(ArgumentIsInvalid, parameterName);
            }

            if (DataConversion.TryConvert(value, out decimal? result))
            {
                if (!ValidationHelper.Positive(result))
                    throw new ArgumentException(ArgumentIsInvalid, parameterName);
            }
            else
            {
                throw new ArgumentException($"String '{value}' is not a valid decimal.", parameterName);
            }
        }

        [AssertionMethod]
        public static void PositiveDecimal(string value, string parameterName)
        {
            if (decimal.TryParse(value, out decimal result))
            {
                if (!ValidationHelper.Positive(result))
                    throw new ArgumentException(ArgumentIsInvalid, parameterName);
            }
            else
            {
                throw new ArgumentException($"String '{value}' is not a valid decimal.", parameterName);
            }
        }

        [AssertionMethod]
        public static void PositiveNullableInt(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
            {
                if (!ValidationHelper.Positive(null))
                    throw new ArgumentException(ArgumentIsInvalid, parameterName);
            }

            if (DataConversion.TryConvert(value, out int? result))
            {
                if (!ValidationHelper.Positive(result))
                    throw new ArgumentException(ArgumentIsInvalid, parameterName);
            }
            else
            {
                throw new ArgumentException($"String '{value}' is not a valid int.", parameterName);
            }
        }

        [AssertionMethod]
        public static void PositiveInt(string value, string parameterName)
        {
            if (int.TryParse(value, out int result))
            {
                if (!ValidationHelper.Positive(result))
                    throw new ArgumentException(ArgumentIsInvalid, parameterName);
            }
            else
            {
                throw new ArgumentException($"String '{value}' is not a valid int.", parameterName);
            }
        }

        [AssertionMethod]
        public static void IntList(IEnumerable<string> values, string parameterName)
        {
            if (values == null)
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
            if (!ValidationHelper.IntList(values.ToList()))
                throw new ArgumentException($"Values are not a valid integer list.", parameterName);
        }

        [AssertionMethod]
        public static void PositiveIntList(IEnumerable<string> values, string parameterName)
        {
            if (values == null)
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
            if (!ValidationHelper.PositiveIntList(values.ToList()))
                throw new ArgumentException($"Values are not a valid positive integer list.", parameterName);
        }

        [AssertionMethod]
        public static void NullablePositiveIntList(IEnumerable<string>? values, string parameterName)
        {
            if (values == null)
                return;
            if (!ValidationHelper.PositiveIntList(values.ToList()))
                throw new ArgumentException($"Values are not a valid positive integer list.", parameterName);
        }

        [AssertionMethod]
        public static void Date(string value, string parameterName)
        {
            if (!DataConversion.TryConvert(value, out DateTime _))
            {
                throw new ArgumentException($"String '{value}' is not a valid Date.", parameterName);
            }
        }

        [AssertionMethod]
        public static void NullableDate(string value, string parameterName)
        {
            if (!DataConversion.TryConvert(value, out DateTime? _))
            {
                throw new ArgumentException($"String '{value}' is not a valid Date or null value.", parameterName);
            }
        }

        [AssertionMethod]
        public static void Positive(int value, string parameterName)
        {
            if (!ValidationHelper.Positive(value))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void Positive(int? value, string parameterName)
        {
            if (!ValidationHelper.Positive(value))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void Positive(decimal value, string parameterName)
        {
            if (!ValidationHelper.Positive(value))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void Positive(decimal? value, string parameterName)
        {
            if (!ValidationHelper.Positive(value))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void ZeroOrPositive(int value, string parameterName)
        {
            if (!ValidationHelper.ZeroOrPositive(value))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void ZeroOrPositive(decimal value, string parameterName)
        {
            if (!ValidationHelper.ZeroOrPositive(value))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void String(string value, string parameterName)
        {
            if (!ValidationHelper.String(value))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void String(string value, string parameterName, int maxLength)
        {
            if (!ValidationHelper.String(value, maxLength))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void IdList(List<int> ids, string parameterName)
        {
            if (!ValidationHelper.IdList(ids))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void IdArray(int[] ids, string parameterName)
        {
            if (!ValidationHelper.IdArray(ids))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void NotEmptyWithoutNulls(object[] data, string parameterName)
        {
            if (!ValidationHelper.NotEmptyWithoutNulls(data))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void NotEmptyIdList(List<int> ids, string parameterName)
        {
            if (!ValidationHelper.NotEmptyIdList(ids))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void Enum<T>(T value, string parameterName)
            where T : struct
        {
            if (!ValidationHelper.Enum(value))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void NotNullOrWhitespace(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(ArgumentIsInvalid, parameterName);
        }

        [AssertionMethod]
        public static void NotNull<T>([NoEnumeration] T obj, string parameterName)
            where T : class
        {
            if (obj == null)
                throw new ArgumentNullException(parameterName);
        }

        [AssertionMethod]
        public static void NotNull<T>(Func<T> selector, string message)
            where T : class
        {
            if (selector() == null)
                throw new InvalidOperationException(message);
        }

        [AssertionMethod]
        public static void NotNull(int? id, string parameterName)
        {
            if (id == null)
                throw new ArgumentNullException(parameterName);
        }
    }
}