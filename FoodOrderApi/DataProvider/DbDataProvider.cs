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

        public async Task<Order?> PlaceOrder(GetOrderDTO newCustomerOrder)
        {
            var restaurants = (await GetRestaurant()).ToList().Where(item => item.RestaurantName.ToLower() == newCustomerOrder.RestaurantName.ToLower()).ToList();
            if (restaurants.Count() > 0)
            {
                var products = (await GetMenus()).Where(item => item.ProductName.ToLower() == newCustomerOrder.ProductName.ToLower()).ToList();
                if (products.Count() > 0 && products[0].RestaurantID == restaurants[0].RestaurantID)
                {
                    var newOrder = mapper.Map<Order>(newCustomerOrder);
                    newOrder.RestaurantID = products[0].RestaurantID;
                    newOrder.ProductID = products[0].ProductID;
                    newOrder.IsDelivered = false;
                    await foodApiDbContext.Orders.AddAsync(newOrder);
                    await foodApiDbContext.SaveChangesAsync();
                    return newOrder;
                }
            }
            return null;
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

        public void PlaceOrder(Order newCustomerOrder)
        {
            throw new NotImplementedException();
        }
    }
}