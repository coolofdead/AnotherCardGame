using System;
using System.Collections.Generic;
using System.Linq;

static class RandomExtension
{
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        Random rnd = new Random();
        return source.OrderBy<T, int>((item) => rnd.Next());
    }
}
