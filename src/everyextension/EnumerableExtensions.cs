using System.Text;

namespace EveryExtension;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        var random = new Random();
        return source.OrderBy(item => random.Next());
    }

    public static IEnumerable<(T Item, int Index)> Index<T>(this IEnumerable<T> source)
        => source.Select((item, index) => (item, index));

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        => condition ? source.Where(predicate) : source;

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> truePredicate, Func<T, bool> falsePredicate)
        => condition ? source.Where(truePredicate) : source.Where(falsePredicate);

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, int, bool> condition, Func<T, bool> predicate)
        => source.Where((item, index) => condition(item, index)).Where(predicate);

    public static IEnumerable<TResult> SelectIf<T, TResult>(this IEnumerable<T> query, bool condition, Func<T, TResult> selector)
       => condition ? query.Select(selector) : query.Cast<TResult>();

    public static IEnumerable<TResult> SelectIf<T, TResult>(this IEnumerable<T> query, bool condition, Func<T, TResult> trueSelector, Func<T, TResult> falseSelector)
        => condition ? query.Select(trueSelector) : query.Select(falseSelector);

    public static bool AnyIf<T>(this IEnumerable<T> query, bool condition, Func<T, bool> predicate)
        => condition ? query.Any(predicate) : true;

    public static bool AnyIf<T>(this IEnumerable<T> query, bool condition, Func<T, bool> truePredicate, Func<T, bool> falsePredicate)
        => condition ? query.Any(truePredicate) : query.Any(falsePredicate);

    public static bool SequenceEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
        => first.SequenceEqual(second, comparer);

    public static IEnumerable<T> DistinctByMany<T, TKey1, TKey2>(this IEnumerable<T> source, Func<T, TKey1> keySelector1, Func<T, TKey2> keySelector2)
    {
        var seenKeys = new HashSet<(TKey1, TKey2)>();
        return source.Where(item => seenKeys.Add((keySelector1(item), keySelector2(item))));
    }

    public static IEnumerable<TResult> SelectManyWithIndex<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        => source.Select(selector);

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

    public static T? FirstOrDefaultOr<T>(this IEnumerable<T> query, Func<T, bool> predicate, T? defaultValue = default)
        => query.FirstOrDefault(predicate) ?? defaultValue;

    public static string ToCsv<T>(this IEnumerable<T> query)
    {
        var headers = typeof(T).GetProperties().Select(p => p.Name);
        var csv = new StringBuilder();
        csv.AppendLine(string.Join(",", headers));
        foreach (var entity in query)
        {
            if (entity == null) continue;
            var values = headers.Select(h => entity.GetType().GetProperty(h)?.GetValue(entity)?.ToString() ?? string.Empty);
            csv.AppendLine(string.Join(",", values));
        }
        return csv.ToString();
    }

    public static IEnumerable<T> RandomSample<T>(this IEnumerable<T> query, int sampleSize)
        => query.OrderBy(_ => Guid.NewGuid()).Take(sampleSize);

    /// <summary>
    /// Counts the number of true values in a sequence of booleans.
    /// </summary>
    /// <param name="values">The sequence of boolean values to count.</param>
    /// <returns>The count of true values in the sequence.</returns>
    public static int CountTrue(this IEnumerable<bool> values)
        => values.Count(c => c);

    /// <summary>
    /// Counts the number of false values in a sequence of booleans.
    /// </summary>
    /// <param name="values">The sequence of boolean values to count.</param>
    /// <returns>The count of false values in the sequence.</returns>
    public static int CountFalse(this IEnumerable<bool> values)
        => values.Count(c => !c);

    /// <summary>
    /// Counts the occurrences of a specified boolean value in a sequence of booleans.
    /// </summary>
    /// <param name="values">The sequence of boolean values to count occurrences from.</param>
    /// <param name="targetValue">The boolean value to count occurrences of.</param>
    /// <returns>The count of occurrences of the specified boolean value.</returns>
    public static int CountOccurrences(this IEnumerable<bool> values, bool targetValue)
        => values.Count(v => v == targetValue);
}