using System.Text.Json;
using Core.Entities;
using Infrastructue.Data;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }

            if (!context.Users.Any())
            {
                var usersData = File.ReadAllText("../Infrastructure/Data/SeedData/users.json");
                var users = JsonSerializer.Deserialize<List<User>>(usersData);
                context.Users.AddRange(users);
            }

            if (!context.Provinces.Any())
            {
                var provincesData = File.ReadAllText("../Infrastructure/Data/SeedData/provinces.json");
                var provinces = JsonSerializer.Deserialize<List<Province>>(provincesData);
                context.Provinces.AddRange(provinces);
            }

            if (!context.UserTypes.Any())
            {
                var userTypesData = File.ReadAllText("../Infrastructure/Data/SeedData/userTypes.json");
                var userTypes = JsonSerializer.Deserialize<List<UserType>>(userTypesData);
                context.UserTypes.AddRange(userTypes);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}