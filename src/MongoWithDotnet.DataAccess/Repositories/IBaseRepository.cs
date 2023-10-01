using System.Linq.Expressions;
using MongoDB.Driver;
using MongoWithDotnet.Core.Common;
using MongoWithDotnet.DataAccess.Helpers.Pagination.DTO;
using MongoWithDotnet.Shared.DTO;

namespace MongoWithDotnet.DataAccess.Repositories;

/// <summary>
/// Base repository.
/// </summary>
/// <typeparam name="TNonSqlDocument"></typeparam>
public interface IBaseRepository<TNonSqlDocument> where TNonSqlDocument : BaseEntity
{
    /// <summary>
    /// Get first (only one) item
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<TNonSqlDocument> GetFirstAsync(FilterDefinition<TNonSqlDocument> filter);

    /// <summary>
    /// Insert item
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    Task<TNonSqlDocument> InsertAsync(TNonSqlDocument document);

    /// <summary>
    /// Insert many items
    /// </summary>
    /// <param name="documents"></param>
    /// <returns></returns>
    Task<List<TNonSqlDocument>> InsertManyAsync(List<TNonSqlDocument> documents);

    /// <summary>
    /// Get all items
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<List<TNonSqlDocument>> GetAllAsync(FilterDefinition<TNonSqlDocument>? filter);

    /// <summary>
    /// Get all items with paging
    /// </summary>
    /// <param name="optionDto"></param>
    /// <param name="filter"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    Task<PageDto<TNonSqlDocument>> GetAllWithPagingAsync(PageOptionDto optionDto,
        FilterDefinition<TNonSqlDocument>? filter,
        SortDefinition<TNonSqlDocument>? sort);

    /// <summary>
    /// Update item
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    Task<TNonSqlDocument> UpdateAsync(TNonSqlDocument document);

    /// <summary>
    /// Delete item
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(string id);

    /// <summary>
    /// Update many items
    /// </summary>
    /// <param name="documents"></param>
    /// <param name="filter"></param>
    /// <param name="update"></param>
    /// <returns></returns>
    Task<List<TNonSqlDocument>> UpdateManyAsync(List<TNonSqlDocument> documents);

    /// <summary>
    /// Delete many items
    /// </summary>
    /// <param name="documents"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task DeleteManyAsync(List<TNonSqlDocument> documents);
}