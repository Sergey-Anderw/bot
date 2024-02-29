using System.Runtime.CompilerServices;

namespace Shared.Extensions
{
    public static class DecimalExtensions
    {
        public static string AsIdString(this decimal value)
        {
            if (value == 0)
                return null!;

            return value.ToString("0.#");
        }

        public static string AsIdString(this decimal? value)
        {
            if (!value.HasValue)
                return null!;

            return AsIdString(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal GetSessionId(this decimal? value)
        {
            return value ?? throw new InvalidOperationException("Session id isn't set.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal GetUserId(this decimal? value)
        {
            return value ?? throw new InvalidOperationException("User isn't logged in.");
        }
    }
}