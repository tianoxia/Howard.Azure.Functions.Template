using AutoMapper;
using Howard.FunctionApp.Model.Requests;
using Howard.FunctionApp.Model.Responses;
using Howard.FunctionApp.Repository.Tables;

namespace Howard.FunctionApp.Model.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Requests
            CreateMap<ItemRequest, Item>();
            #endregion

            #region Responses
            CreateMap<Item, ItemResponse>();
            #endregion
        }
    }
}
