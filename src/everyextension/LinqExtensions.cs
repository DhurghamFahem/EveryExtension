namespace EveryExtension;

public static class LinqExtensions
{
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        var random = new Random();
        return source.OrderBy(item => random.Next());
    }

    public static IEnumerable<(T Item, int Index)> Index<T>(this IEnumerable<T> source)
    {
        return source.Select((item, index) => (item, index));
    }

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> truePredicate, Func<T, bool> falsePredicate)
    {
        return condition ? source.Where(truePredicate) : source.Where(falsePredicate);
    }

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, int, bool> condition, Func<T, bool> predicate)
    {
        return source.Where((item, index) => condition(item, index)).Where(predicate);
    }


    public static bool SequenceEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
        => first.SequenceEqual(second, comparer);

    public static IEnumerable<T> DistinctByMany<T, TKey1, TKey2>(
       this IEnumerable<T> source,
       Func<T, TKey1> keySelector1,
       Func<T, TKey2> keySelector2)
    {
        var seenKeys = new HashSet<(TKey1, TKey2)>();
        return source.Where(item => seenKeys.Add((keySelector1(item), keySelector2(item))));
    }

    public static IEnumerable<TResult> SelectManyWithIndex<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, int, TResult> selector)
    {
        return source.Select(selector);
    }

    public static string ToDelimitedString<T>(this IEnumerable<T> source, string delimiter)
        => string.Join(delimiter, source);
    
    public static string ToDelimitedString<T>(this IEnumerable<T> source, string delimiter, Func<T, string> selector)
        => string.Join(delimiter, source.Select(selector)); 
    
    public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        => !source.Any(predicate);

    public static IEnumerable<T> Interleave<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        using var enumerator1 = first.GetEnumerator();
        using var enumerator2 = second.GetEnumerator();
        while (enumerator1.MoveNext() && enumerator2.MoveNext())
        {
            yield return enumerator1.Current;
            yield return enumerator2.Current;
        }
    }

    public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
        => source.SelectMany(item => item);

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        var random = new Random();
        var list = source.ToList();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random random)
    {
        var list = source.ToList();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, int seed)
    {
        var random = new Random(seed);
        return source.Shuffle(random);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random random, int seed)
    {
        random = new Random(seed);
        return source.Shuffle(random);
    }
}
