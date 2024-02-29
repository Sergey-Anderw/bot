using System.Collections;

namespace Shared.Helpers
{
    /// <summary>
    /// http://www.isthe.com/chongo/tech/comp/fnv/
    /// </summary>
    public static class FnvHash
    {
        public static readonly int OffsetBasis = unchecked((int)2166136261);
        public static readonly int Prime = 16777619;

        public static int CreateHash(params object[] objects)
        {
            return objects.Aggregate(
                OffsetBasis,
                (r, o) =>
                {
                    if (o is string str)
                        return (r ^ str.GetHashCode()) * Prime;

                    if (o is IEnumerable enumerable)
                        return (r ^ CreateHash(enumerable)) * Prime;

                    return (r ^ o.GetHashCode()) * Prime;
                });
        }

        private static int CreateHash(IEnumerable enumerable)
        {
            return enumerable
                .OfType<object>()
                .Aggregate(OffsetBasis, (r, o) => (r ^ Ghc(o)) * Prime);
        }

        private static int Ghc(object o)
        {
            var a = o.GetHashCode();

            return a;
        }
    }
}