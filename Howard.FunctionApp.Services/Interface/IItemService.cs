using Howard.FunctionApp.Model.Requests;
using Howard.FunctionApp.Model.Responses;
using Howard.FunctionApp.Repository.Tables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Howard.FunctionApp.Services.Interface
{
    public interface IItemService
    {
        /// <summary>
        ///     Gets all items from DB
        /// </summary>
        /// <returns></returns>
        Task<IList<ItemResponse>> GetItems();

        /// <summary>
        ///     Gets item from DB based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ItemResponse> GetItem(Guid id);

        /// <summary>
        ///     Gets T with LINQ expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        ItemResponse GetItemWithExpression(Func<Item, bool> predicate);

        /// <summary>
        ///     Gets list of items with LINQ expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IList<ItemResponse> GetAllItemsWithExpression(Func<Item, bool> predicate);

        /// <summary>
        ///     Creates item on DB
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<ItemResponse> PostItem(ItemRequest request);

        /// <summary>
        ///     Updates item on DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<ItemResponse> PutItem(Guid id, ItemRequest request);

        /// <summary>
        ///     Deletes item from DB based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteItem(Guid id);
    }
}
