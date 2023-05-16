using FoodOrderApi.DataProvider;
using FoodOrderApi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//adding food order api
builder.Services.AddFoodOrderApi();
builder.Services.AddDbContext<FoodApiDbContext>(options =>
    options.UseSqlite("Data Source = foodorder.db"));
//builder.Services.AddSingleton<IDataProvider, InMemoryDataProvider>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class MyExtensions
{
    public static void AddFoodOrderApi(this IServiceCollection services)
    {
        services.AddScoped<IDataProvider, DbDataProvider>();
        //services.AddSingleton<IDataProvider, InMemoryDataProvider>();
    }
}