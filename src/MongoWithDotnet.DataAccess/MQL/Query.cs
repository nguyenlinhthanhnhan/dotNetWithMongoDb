using MongoDB.Driver;
using MongoWithDotnet.Core.Common;
using MongoWithDotnet.DataAccess.Helpers.Pagination.DTO;
using MongoWithDotnet.Shared.DTO;

namespace MongoWithDotnet.DataAccess.MQL;

/// <summary>
/// Query extensions.
/// </summary>
public static class Query
{
    /// <summary>
    /// Get all items
    /// </summary>
    /// <param name="collection"></param>
    /// <typeparam name="TNonSqlDocument"></typeparam>
    /// <returns></returns>
    public static async Task<List<TNonSqlDocument>> GetAllAsync<TNonSqlDocument>(
        this IMongoCollection<TNonSqlDocument> collection)
    {
        return await collection.Find(_ => true).ToListAsync();
    }

    /// <summary>
    /// Get an item by filter
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="filter"></param>
    /// <param name="sort"></param>
    /// <param name="limit"></param>
    /// <typeparam name="TNonSqlDocument"></typeparam>
    /// <returns></returns>
    public static async Task<TNonSqlDocument> SingleAsync<TNonSqlDocument>(
        this IMongoCollection<TNonSqlDocument> collection, FilterDefinition<TNonSqlDocument>? filter = null,
        SortDefinition<TNonSqlDocument>? sort = null, int limit = 0) where TNonSqlDocument : BaseEntity
    {
        filter ??= Builders<TNonSqlDocument>.Filter.Empty;
        sort ??= Builders<TNonSqlDocument>.Sort.Descending(x => x.Id);
        return await collection.Find(filter).Sort(sort).Limit(limit).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Insert an item
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="document"></param>
    /// <typeparam name="TNonSqlDocument"></typeparam>
    /// <returns></returns>
    public static async Task<TNonSqlDocument> InsertAsync<TNonSqlDocument>(
        this IMongoCollection<TNonSqlDocument> collection, TNonSqlDocument document)
    {
        await collection.InsertOneAsync(document);
        return document;
    }

    /// <summary>
    /// Delete an item by id
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="id"></param>
    /// <typeparam name="TNonSqlDocument"></typeparam>
    /// <returns></returns>
    public static async Task<bool> DeleteAsync<TNonSqlDocument>(this IMongoCollection<TNonSqlDocument> collection,
        string id)
    {
        var filter = Builders<TNonSqlDocument>.Filter.Eq("_id", id);
        var result = await collection.DeleteOneAsync(filter);
        return result.IsAcknowledged;
    }

    /// <summary>
    /// Get all items with pagination
    /// </summary>
    /// <param name="data"></param>
    /// <param name="option"><see cref="PageOptionDto"/></param>
    /// <param name="filter"></param>
    /// <param name="sort"></param>
    /// <typeparam name="TNonSqlDocument"></typeparam>
    /// <returns></returns>
    public static async Task<PageDto<TNonSqlDocument>> Paginate<TNonSqlDocument>(
        this IMongoCollection<TNonSqlDocument> data,
        PageOptionDto option,
        FilterDefinition<TNonSqlDocument>? filter = null,
        SortDefinition<TNonSqlDocument>? sort = null) where TNonSqlDocument : BaseEntity
    {
        filter ??= Builders<TNonSqlDocument>.Filter.Empty;

        var totalItems = await data.CountDocumentsAsync(filter);

        var items = await data.Find(filter).Sort(sort).Skip(option.Skip).Limit(option.Limit).ToListAsync();

        var meta = new PageMetaDto
        {
            Limit = option.Limit,
            Page = option.Page,
            TotalItems = totalItems
        };

        return new PageDto<TNonSqlDocument>
        {
            Items = items,
            Meta = meta
        };
    }

    /// <summary>
    /// Bulk insert
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="documents"></param>
    /// <typeparam name="TNonSqlDocument"></typeparam>
    /// <returns></returns>
    public static async Task<List<TNonSqlDocument>> BulkInsertAsync<TNonSqlDocument>(
        this IMongoCollection<TNonSqlDocument> collection, List<TNonSqlDocument> documents)
    {
        await collection.InsertManyAsync(documents);
        return documents;
    }

    /// <summary>
    /// Update an item
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="document"></param>
    /// <typeparam name="TNonSqlDocument"></typeparam>
    /// <returns></returns>
    public static async Task<TNonSqlDocument> UpdateAsync<TNonSqlDocument>(
        this IMongoCollection<TNonSqlDocument> collection, TNonSqlDocument document) where TNonSqlDocument : BaseEntity
    {
        var filter = Builders<TNonSqlDocument>.Filter.Eq("_id", document.Id);
        await collection.ReplaceOneAsync(filter, document);
        return document;
    }

    /// <summary>
    /// Update many items
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="documents"></param>
    /// <typeparam name="TNonSqlDocument"></typeparam>
    /// <returns></returns>
    public static async Task<List<TNonSqlDocument>> UpdateManyAsync<TNonSqlDocument>(
        this IMongoCollection<TNonSqlDocument> collection, List<TNonSqlDocument> documents)
        where TNonSqlDocument : BaseEntity
    {
        var bulk = collection.BulkWriteAsync(documents.Select(x =>
            new ReplaceOneModel<TNonSqlDocument>(Builders<TNonSqlDocument>.Filter.Eq("_id", x.Id), x)));
        await bulk;
        return documents;
    }

    /// <summary>
    /// Bulk delete
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="documents"></param>
    /// <typeparam name="TNonSqlDocument"></typeparam>
    /// <returns></returns>
    public static async Task BulkDeleteAsync<TNonSqlDocument>(this IMongoCollection<TNonSqlDocument> collection,
        List<TNonSqlDocument> documents) where TNonSqlDocument : BaseEntity
    {
        var bulk = collection.BulkWriteAsync(documents.Select(x =>
            new DeleteOneModel<TNonSqlDocument>(Builders<TNonSqlDocument>.Filter.Eq("_id", x.Id))));
        await bulk;
    }
}