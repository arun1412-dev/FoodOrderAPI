using AutoMapper;
using FoodOrderApi.Model.Domain;
using FoodOrderApi.Model.DTO;
using FoodOrderApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace FoodOrderApi.DataProvider
{
    public class DbDataProvider : Controller, IDataProvider
    {
        private readonly FoodApiDbContext foodApiDbContext;
        private readonly IMapper mapper;

        public DbDataProvider(FoodApiDbContext foodApiDbContext, IMapper mapper)
        {
            this.foodApiDbContext = foodApiDbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Menu>> GetMenus()
        {
            return await foodApiDbContext.Menus.ToListAsync();
        }

        public async Task<IEnumerable<Order>?> GetOrderByName(string customerName)
        {
            return await foodApiDbContext.Orders.Where(item => item.CustomerName == customerName).Include("Menu").Include("Restaurant").ToListAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurant()
        {
            return await foodApiDbContext.Restaurants.ToListAsync();
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
        public async Task<bool> Discount(string restaturantName, double discount)
        {
            var found = foodApiDbContext.Restaurants.ToList();
            var Restaturant = found.Where(s => s.RestaurantName == restaturantName);
            if (Restaturant.Count() != 0) {
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
    }
}