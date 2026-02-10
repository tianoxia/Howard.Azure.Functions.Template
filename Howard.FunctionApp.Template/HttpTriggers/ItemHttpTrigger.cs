using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Howard.FunctionApp.Services.Interface;
using Howard.FunctionApp.Model.Constants;
using Howard.FunctionApp.Model.Requests;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using System.Collections.Generic;
using Howard.FunctionApp.Model.Responses;

namespace Howard.FunctionApp.Template.HttpTriggers
{
    public class ItemHttpTrigger
    {
        private readonly ILogger _logger;
        private readonly IItemService _itemService;

        public ItemHttpTrigger(ILogger<ItemHttpTrigger> logger,
                                IItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }

        /// <summary>
        ///     Gets list of items
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "getItems", tags: new[] { "Item" }, Summary = "Gets list of Items", Description = "Returns list of items", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IList<ItemResponse>), Summary = "Gets list of items", Description = "Returns list of items")]
        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_GETALL)]
        public async Task<IActionResult> GetAllItems(
            [HttpTrigger(AuthorizationLevel.Function, "get",
            Route = HttpTriggerFunctionRouteConstants.ITEM)] HttpRequest req
            )
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_GETALL} - Started");

                var result = await _itemService.GetItems();

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_GETALL} - Finished");
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.ITEM_GETALL} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        ///     Gets item by Id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "getItem", tags: new[] { "Item" }, Summary = "Gets item", Description = "Returns item", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Summary = "Id", Description = "Id", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ItemResponse), Summary = "Gets item", Description = "Returns item")]
        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_GET)]
        public async Task<IActionResult> GetItem(
            [HttpTrigger(AuthorizationLevel.Function, "get",
            Route = HttpTriggerFunctionRouteConstants.ITEM_BYID)] HttpRequest request, Guid id)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_GET} - Started");

                var result = await _itemService.GetItem(id);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_GET} - Finished");
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.ITEM_GET} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        ///     Creates item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "postItem", tags: new[] { "Item" }, Summary = "Creates item", Description = "Creates item", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ItemRequest), Required = true, Description = "Request")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(ItemResponse), Summary = "Creates item", Description = "Creates item")]
        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_POST)]
        public async Task<IActionResult> PostItem(
            [HttpTrigger(AuthorizationLevel.Function, "post",
            Route = HttpTriggerFunctionRouteConstants.ITEM)] HttpRequest request)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_POST} - Started");

                var input = await request.ReadAsStringAsync();
                var emailBodyRequest = JsonConvert.DeserializeObject<ItemRequest>(input);
                var result = await _itemService.PostItem(emailBodyRequest);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_POST} - Finished");
                return new CreatedResult(string.Empty, result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.ITEM_POST} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        ///     Updates item
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "putItem", tags: new[] { "Item" }, Summary = "Updates item", Description = "Updates item", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Summary = "Id", Description = "Id", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ItemRequest), Required = true, Description = "Request")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ItemResponse), Summary = "Updates item", Description = "Updates item")]
        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_PUT)]
        public async Task<IActionResult> PutItem(
            [HttpTrigger(AuthorizationLevel.Function, "put",
            Route = HttpTriggerFunctionRouteConstants.ITEM_BYID)] HttpRequest request, Guid id)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_PUT} - Started");

                var input = await request.ReadAsStringAsync();
                var emailBodyRequest = JsonConvert.DeserializeObject<ItemRequest>(input);
                var result = await _itemService.PutItem(id, emailBodyRequest);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_PUT} - Finished");
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.ITEM_PUT} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        ///     Delets item
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "deleteItem", tags: new[] { "Item" }, Summary = "Deletes item", Description = "Deletes item", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Summary = "Id", Description = "Id", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Summary = "Deletes item", Description = "Deletes item")]
        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_DELETE)]
        public async Task<IActionResult> DeleteItem(
            [HttpTrigger(AuthorizationLevel.Function, "delete",
            Route = HttpTriggerFunctionRouteConstants.ITEM_BYID)] HttpRequest request, Guid id)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_DELETE} - Started");

                var result = await _itemService.DeleteItem(id);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_DELETE} - Finished");
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.ITEM_DELETE} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
