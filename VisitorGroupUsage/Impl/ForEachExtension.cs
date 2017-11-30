using System;
using System.Collections.Generic;

namespace VisitorGroupUsage.Impl
{
    public static class ForEachExtension
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
    }
}
