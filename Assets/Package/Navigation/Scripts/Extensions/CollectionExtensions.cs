using System.Collections.Generic;

namespace Elselam.UnityRouter.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection) => collection == null || collection.Count == 0;
        public static bool IsNullOrEmpty(this string str) => str == null || str.Length == 0;
        public static bool IsNullOrEmpty<T, T2>(this IDictionary<T, T2> collection) => collection == null || collection.Count == 0;
    }
}