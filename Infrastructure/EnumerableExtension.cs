using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elementalium.Infrastructure
{
    public static class EnumerableExtension
    {
        public static int ElementWiseHashcode<T>(this IEnumerable<T> items)
        {
            unchecked
            {
                return items.Select(t => t.GetHashCode()).Aggregate((res, next) => (res * 379) ^ next);
            }
        }
	}
}
