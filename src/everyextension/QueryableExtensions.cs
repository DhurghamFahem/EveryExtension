using System.Linq.Expressions;
using System.Text;

namespace EveryExtension;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        => condition ? query.Where(predicate) : query;

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> truePredicate, Expression<Func<T, bool>> falsePredicate)
        => condition ? source.Where(truePredicate) : source.Where(falsePredicate);

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Func<T, int, bool> condition, Expression<Func<T, bool>> predicate)
        => source.Where((item, index) => condition(item, index)).Where(predicate);

    public static IQueryable<TResult> SelectIf<T, TResult>(this IQueryable<T> query, bool condition, Expression<Func<T, TResult>> selector)
        => condition ? query.Select(selector) : query.Cast<T, TResult>();

    public static IQueryable<TResult> SelectIf<T, TResult>(this IQueryable<T> query, bool condition, Expression<Func<T, TResult>> trueSelector, Expression<Func<T, TResult>> falseSelector)
        => condition ? query.Select(trueSelector) : query.Select(falseSelector);

    public static bool AnyIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        => condition ? query.Any(predicate) : true;

    public static bool AnyIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> truePredicate, Expression<Func<T, bool>> falsePredicate)
        => condition ? query.Any(truePredicate) : query.Any(falsePredicate);

    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string propertyName, bool ascending)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var lambda = Expression.Lambda(property, parameter);
        var methodName = ascending ? "OrderBy" : "OrderByDescending";
        var methodCallExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            [typeof(T), property.Type],
            query.Expression,
            Expression.Quote(lambda));

        return query.Provider.CreateQuery<T>(methodCallExpression);
    }

    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int pageSize)
        => query.Skip((page - 1) * pageSize).Take(pageSize);

    private static IQueryable<TResult> Cast<T, TResult>(this IQueryable<T> query)
        => query.Provider.CreateQuery<TResult>(query.Expression);

    private static IQueryable<IGrouping<object, T>> GroupByMultipleKeys<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] keySelectors)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var keyExpressions = keySelectors.Select(selector =>
            Expression.Invoke(selector, parameter));
        var keySelector = Expression.Lambda<Func<T, object[]>>(
            Expression.NewArrayInit(typeof(object), keyExpressions),
            parameter);
        return query.GroupBy(keySelector);
    }

    public static T? FirstOrDefaultOr<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, T? defaultValue = default)
        => query.FirstOrDefault(predicate) ?? defaultValue;

    public static string ToCsv<T>(this IQueryable<T> query)
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

    public static IQueryable<T> RandomSample<T>(this IQueryable<T> query, int sampleSize)
        => query.OrderBy(_ => Guid.NewGuid()).Take(sampleSize);

    public static IQueryable<T> WhereNotNull<T, TValue>(this IQueryable<T> query, Expression<Func<T, TValue>> selector)
        => query.Where(x => selector.Compile().Invoke(x) != null);
}
