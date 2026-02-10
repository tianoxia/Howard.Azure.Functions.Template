using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Howard.FunctionApp.Model.Requests;
using Howard.FunctionApp.Model.Responses;
using Howard.FunctionApp.Repository.Pattern.Interface;
using Howard.FunctionApp.Repository.Tables;
using Howard.FunctionApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Howard.FunctionApp.Services.Implementation
{
    public class ItemService : IItemService
    {
        private readonly IRepository<Item> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ItemService(IRepository<Item> repository,
                            IMapper mapper,
                            ILogger<ItemService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteItem(Guid id)
        {
            _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(DeleteItem)} - Started, Id: {id}");
            return await _repository.Delete(id);
        }

        /// <inheritdoc/>
        public IList<ItemResponse> GetAllItemsWithExpression(Func<Item, bool> predicate)
        {
            _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(GetAllItemsWithExpression)}");

            var item = _repository.GetAllWithExpression(predicate);

            if (item.Count == 0)
            {
                _logger.LogError($"{nameof(ItemService)} " +
                $"- {nameof(GetAllItemsWithExpression)} - Not found");
                throw new KeyNotFoundException();
            }

            _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(GetAllItemsWithExpression)} - Finished");
            return _mapper.Map<IList<ItemResponse>>(item);
        }

        /// <inheritdoc/>
        public async Task<ItemResponse> GetItem(Guid id)
        {
            _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(GetItem)} - Started, Id: {id}");

            var item = await _repository.Get(id);

            if (item == null)
            {
                _logger.LogError($"{nameof(ItemService)} " +
                $"- {nameof(GetItem)} - Not found, Id: {id}");
                throw new KeyNotFoundException();
            }

            _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(GetItem)} - Finished, Id: {id}");
            return _mapper.Map<ItemResponse>(item);
        }

        /// <inheritdoc/>
        public async Task<IList<ItemResponse>> GetItems()
        {
            _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(GetItems)} - Started");
            var items = await _repository.GetAll();

            if (items.Count == 0)
            {
                _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(GetItems)} - Not found");
                throw new KeyNotFoundException();
            }

            _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(GetItems)} - Finished");
            return _mapper.Map<IList<ItemResponse>>(items);
        }

        /// <inheritdoc/>
        public ItemResponse GetItemWithExpression(Func<Item, bool> predicate)
        {
            _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(GetItemWithExpression)}");

            var item = _repository.GetWithExpression(predicate);

            if (item == null)
            {
                _logger.LogError($"{nameof(ItemService)} " +
                $"- {nameof(GetItemWithExpression)} - Not found");
                throw new KeyNotFoundException();
            }

            _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(GetItemWithExpression)} - Finished");
            return _mapper.Map<ItemResponse>(item);
        }

        /// <inheritdoc/>
        public async Task<ItemResponse> PostItem(ItemRequest request)
        {
            _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(PostItem)} - Started, Request: {JsonConvert.SerializeObject(request)}");
            var newItem = _mapper.Map<Item>(request);

            var addedItem = await _repository.Add(newItem);

            _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(PostItem)} - Finished, Request: {JsonConvert.SerializeObject(request)}");
            return _mapper.Map<ItemResponse>(addedItem);
        }

        /// <inheritdoc/>
        public async Task<ItemResponse> PutItem(Guid id, ItemRequest request)
        {
            _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(PutItem)} - Started, Request: {JsonConvert.SerializeObject(request)}");
            var item = await _repository.Get(id);

            if (item == null)
            {
                _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(PutItem)} - Not found, Request: {JsonConvert.SerializeObject(request)}");
                throw new KeyNotFoundException();
            }

            _mapper.Map(request, item);
            await _repository.Update(item);

            _logger.LogInformation($"{nameof(ItemService)} " +
                $"- {nameof(PutItem)} - Finished, Request: {JsonConvert.SerializeObject(request)}");
            return _mapper.Map<ItemResponse>(item);
        }
    }
}
