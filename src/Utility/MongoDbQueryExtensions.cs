namespace AspNetCore.Identity.MongoDB.Utility;

internal static class MongoDbQueryExtensions
{
    private static FindOptions<TItem> LimitOneOption<TItem>() => new()
    {
        Limit = 1
    };

    internal static async Task<TItem> FirstOrDefaultAsync<TItem>(
        this IMongoCollection<TItem> collection,
        Expression<Func<TItem, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(collection, nameof(collection));

        var result = await collection
            .FindAsync(expression, LimitOneOption<TItem>(), cancellationToken)
            .ConfigureAwait(false);

        return await result
            .FirstOrDefaultAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    internal static async Task<IEnumerable<TItem>> WhereAsync<TItem>(
        this IMongoCollection<TItem> collection,
        Expression<Func<TItem, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(collection, nameof(collection));

        var result = await collection
            .FindAsync(expression, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return result.ToEnumerable(cancellationToken: cancellationToken);
    }
}
