using AutoMapper;
using FoodOrderApi.Model.Domain;
using FoodOrderApi.Model.DTO;

namespace FoodOrderApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Menu, MenuDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Restaurant, RestaurantDTO>().ReverseMap();
            //CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            //CreateMap<Walk, WalkDto>().ReverseMap();
            //CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            //CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
    }
}
}