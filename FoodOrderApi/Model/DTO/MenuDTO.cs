using FoodOrderApi.Model.Domain;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodOrderApi.Model.DTO
{
    public class MenuDTO
    {
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
    }
}