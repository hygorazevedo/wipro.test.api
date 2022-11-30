using AutoMapper;
using wipro.teste.api.Controllers.GetItem;
using wipro.teste.api.Controllers.PostItem;
using wipro.teste.gateway;

namespace wipro.teste.api.Controllers.Shared
{
    public class ItemFilaProfiler : Profile
    {
        public ItemFilaProfiler()
        {
            CreateMap<PostItemRequest, ItemFila>().ReverseMap();
            CreateMap<ItemFila, GetItemResponse>().ReverseMap();
        }
    }
}
