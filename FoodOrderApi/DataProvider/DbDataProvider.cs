﻿using AutoMapper;
using FoodOrderApi.Model;
using FoodOrderApi.Controllers;
using FoodOrderApi.Model.Domain;
using FoodOrderApi.Model.DTO;
using FoodOrderApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Microsoft.AspNetCore.JsonPatch;

namespace FoodOrderApi.DataProvider
{
    public class DbDataProvider : Controller, IDataProvider
    {
        private readonly FoodApiDbContext foodApiDbContext;
        private readonly IMapper mapper;
        private readonly ILogger<RestaurantController> logger;

        public DbDataProvider(FoodApiDbContext foodApiDbContext, IMapper mapper, ILogger<RestaurantController> logger)
        {
            this.foodApiDbContext = foodApiDbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IEnumerable<Menu>> GetMenus()
        {
            return await foodApiDbContext.Menus.Include("Restaurant").ToListAsync();
        }

        public async Task<IEnumerable<Order>?> GetOrderByName(string customerName)
        {
            return await foodApiDbContext.Orders.Where(item => item.CustomerName == customerName).Include("Menu").Include("Restaurant").ToListAsync();
        }

        public async Task<RestaurantsandMenusDTO> SearchMenuAndRestaurant(string searchString)
        {
            var restaurants = foodApiDbContext.Restaurants.AsQueryable();
            var menus = foodApiDbContext.Menus.AsQueryable();
            restaurants = restaurants.Where(x => x.RestaurantName.ToLower().Contains(searchString.ToLower()));
            menus = menus.Where(x => x.ProductName.ToLower().Contains(searchString.ToLower())).Include("Restaurant");
            var restaurantDTOMapper = mapper.Map<List<DisplayRestaurantDTO>>(await restaurants.ToListAsync());
            var menuDTOMapper = mapper.Map<List<DisplayMenuDTO>>(await menus.ToListAsync());

            RestaurantsandMenusDTO restaurantsandMenusDTOs = new RestaurantsandMenusDTO();
            restaurantsandMenusDTOs.menu = menuDTOMapper;
            restaurantsandMenusDTOs.restaurant = restaurantDTOMapper;
            return restaurantsandMenusDTOs;
        }

        public async Task<IEnumerable<Restaurant>> FilterRestaurant(string? filterString = null)
        {
            var restaurants = foodApiDbContext.Restaurants.AsQueryable();
            if (filterString != null)
            {
                restaurants = restaurants.Where(x => x.RestaurantName.ToLower().Contains(filterString.ToLower()));
                logger.LogInformation("Data fetched from the restaurant table.");
            }
            return await restaurants.ToListAsync();
        }

        public async Task<(IEnumerable<Restaurant>, PaginationMetadata)> GetRestaurantPaged(int pageNumber, int pageSize)
        {
            var productsCount = await foodApiDbContext.Restaurants.CountAsync();
            var paginationMetadata = new PaginationMetadata(productsCount, pageSize, pageNumber);

            var paginatedProducts = await foodApiDbContext.Restaurants
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            //return (paginatedProducts, paginationMetadata);
            return (paginatedProducts, paginationMetadata);
        }

        public async Task<IList<string>?> GetRestaurantWithMenu(string restaurantName)
        {
            var getParticularRestaurant = await (foodApiDbContext.RestaurantWithMenus.FirstOrDefaultAsync(item => item.RestaurantName.ToLower() == restaurantName.ToLower()));
            if (getParticularRestaurant != null)
            {
                return getParticularRestaurant.Menus;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Order>?> PlaceOrder(List<GetOrderDTO> newCustomerOrder)
        {
            var orderList = new List<Order>();
            foreach (var customerOrder in newCustomerOrder)
            {
                var restaurants = (await GetRestaurant()).ToList().Where(item => item.RestaurantName.ToLower() == customerOrder.RestaurantName.ToLower()).ToList();
                if (restaurants.Count() > 0)
                {
                    var products = (await GetMenus()).Where(item => (item.ProductName.ToLower() == customerOrder.ProductName.ToLower()) && (item.RestaurantID == restaurants[0].RestaurantID)).ToList();
                    if (products.Count() > 0)
                    {
                        var newOrder = mapper.Map<Order>(customerOrder);
                        newOrder.RestaurantID = products[0].RestaurantID;
                        newOrder.ProductID = products[0].ProductID;
                        newOrder.IsDelivered = false;
                        await foodApiDbContext.Orders.AddAsync(newOrder);
                        await foodApiDbContext.SaveChangesAsync();
                        orderList.Add(newOrder);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            return orderList;
        }

        public async Task<bool> OrderDelivered(Guid CustomerOrderId)
        {
            var CustomerOrder = await foodApiDbContext.Orders.FirstOrDefaultAsync(item => item.Id == CustomerOrderId);
            if (CustomerOrder == null)
            {
                return false;
            }
            CustomerOrder.IsDelivered = true;
            foodApiDbContext.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurant()
        {
            return await foodApiDbContext.Restaurants.ToListAsync();
        }

        public async Task<bool> Discount(string restaturantName, double discount)
        {
            var found = foodApiDbContext.Restaurants.ToList();
            var Restaturant = found.Where(s => s.RestaurantName == restaturantName);
            if (Restaturant.Count() != 0)
            {
                var restaurant = await foodApiDbContext.Restaurants.FirstOrDefaultAsync(x => x.RestaurantName == restaturantName);
                restaurant.RestaurantOffer = discount;
                foodApiDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteMenu(Guid MenuID)
        {
            var Menu = await foodApiDbContext.Menus.FirstOrDefaultAsync(x => x.ProductID == MenuID);
            if (Menu == null)
            {
                return false;
            }
            foodApiDbContext.Menus.Remove(Menu);
            foodApiDbContext.SaveChanges();
            return true;
        }

        public async Task<Menu> PatchMenuItems(Guid RestaurantID, JsonPatchDocument<Menu> jsonPatchDocument)
        {
            //var restaurantMenus = await foodApiDbContext.Menus.FindAsync(RestaurantID);
            var restaurantMenus = await foodApiDbContext.Menus.FirstOrDefaultAsync(x => x.RestaurantID == RestaurantID);
         
            if (restaurantMenus != null)
            {
                var menuToBePatched = mapper.Map<Menu>(jsonPatchDocument);
                
                foodApiDbContext.SaveChangesAsync();
                if (ModelState.IsValid)
                {
                    return null;
                }
                return restaurantMenus;
            }
            return null;
        }
    }
}

//public async Task<Menu> PatchMenuItems(Guid RestaurantID, JsonPatchDocument<Menu> jsonPatchDocument)
//{
//    //var restaurantMenus = await foodApiDbContext.Menus.FindAsync(RestaurantID);
//    var restaurantMenus = await foodApiDbContext.Menus.FirstOrDefaultAsync(x => x.RestaurantID == RestaurantID);

//    if (restaurantMenus != null)
//    {
//        var menuToBePatched = new Menu();
//        jsonPatchDocument.ApplyTo(menuToBePatched, ModelState);
//        foodApiDbContext.SaveChangesAsync();
//        if (ModelState.IsValid)
//        {
//            return null;
//        }
//        return restaurantMenus;
//    }
//    return null;
//}