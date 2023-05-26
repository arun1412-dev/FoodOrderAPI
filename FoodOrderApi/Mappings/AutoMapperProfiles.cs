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
            CreateMap<Restaurant, DisplayRestaurantDTO>().ReverseMap();
            CreateMap<RestaurantWithMenu, DisplayRestaurantDTO>().ReverseMap();
            CreateMap<Menu, DisplayMenuDTO>().ReverseMap();
            CreateMap<Order, GetOrderDTO>().ReverseMap();
            //CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            //CreateMap<Walk, WalkDto>().ReverseMap();
            //CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            //CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
    }
}