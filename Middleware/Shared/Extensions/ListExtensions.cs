using JetBrains.Annotations;

namespace Shared.Extensions
{
    public static class ListExtensions
    {
        public static List<T> AsList<T>([NotNull] this T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var list = new List<T>
                           {
                               item
                           };

            return list;
        }
    }
}