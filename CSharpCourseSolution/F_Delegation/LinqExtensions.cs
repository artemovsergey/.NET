using System;
using System.Collections.Generic;

namespace F_Delegation
{
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
                throw new ArgumentNullException();
            
            foreach(var item in source)
            {
                action(item);
            }
        }
    }
}
