using FoodOrderApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceStack.Text;

namespace FoodOrderApi.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.ProductID).HasColumnName("ProductID (Numbers)");
            builder.Property(x => x.CustomerName);
        }
    }

    public class RestaurantConfigurations : IEntityTypeConfiguration<RestaurantWithMenu>
    {
        public void Configure(EntityTypeBuilder<RestaurantWithMenu> builder)
        {
            builder
            .Property(x => x.Menus)
                .HasConversion(v => CsvSerializer.SerializeToCsv(v),
                               v => CsvSerializer.DeserializeFromString<List<string>>(v),
                               new ValueComparer<IList<string>>(
                                                        (c1, c2) => c1.SequenceEqual(c2),
                                                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                                                        c => (IList<string>)c.ToList()));
        }
    }
}