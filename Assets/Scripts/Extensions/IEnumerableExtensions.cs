using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Archero.Extensions
{
    public static class IEnumerableExtensions
    {
        public static T GetRandomElement<T>(this IEnumerable<T> source)
        {
            return source.ElementAt(Random.Range(0, source.Count()));
        }
    }
}
