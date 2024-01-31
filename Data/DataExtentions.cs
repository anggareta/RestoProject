using Microsoft.EntityFrameworkCore;
using RestoProject.API.Repositories;
using RestoProject.API.Entities;

namespace RestoProject.API.Data;

public static class DataExtentions
{
  public static void InitializeDb(this IServiceProvider serviceProvider)
  {
    using var scope = serviceProvider.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<RestoProjectContext>();
    dbContext.Database.Migrate();
    dbContext.Database.EnsureCreated();

    DataSeeder.SeedData(dbContext);
  }

  public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
  {
    var connString = configuration.GetConnectionString("RestoStoreContext");
    services.AddSqlServer<RestoProjectContext>(connString)
      .AddScoped<ICustomersRepository, EFCustomersRepository>()
      .AddScoped<IDishRepository, EFDishRepository>()
      .AddScoped<ICustDishRepository, EFCustDishRepository>();

    return services;
  }

}

public static class DataSeeder
{
  public static void SeedData(RestoProjectContext context)
  {
    if (!context.TMCustomer.Any())
    {
      context.TMCustomer.AddRange(
        new Customer() { Name = "Ayus", BirthDate = new DateTime(1981, 3, 11), Meja = "1" },
        new Customer() { Name = "Icha", BirthDate = new DateTime(1994, 11, 15), Meja = "2" },
        new Customer() { Name = "Zeta", BirthDate = new DateTime(2015, 10, 19), Meja = "3" },
        new Customer() { Name = "Kena", BirthDate = new DateTime(2017, 3, 21), Meja = "4" },
        new Customer() { Name = "Deba", BirthDate = new DateTime(2021, 6, 5), Meja = "5" }
      );
      context.SaveChanges();
    }

    if (!context.TMDish.Any())
    {
      context.TMDish.AddRange(
        new Dish() { DishName = "Gado-gado", Price = 11.26M },
        new Dish() { DishName = "Nasi Goreng Kambing", Price = 13.33M },
        new Dish() { DishName = "Ayam Goreng", Price = 8.25M }
      );
      context.SaveChanges();
    }

    if (!context.TTCustDish.Any())
    {
      context.TTCustDish.AddRange(
        new CustomerDish() { IdCustomer = 1, IdDish = 1 },
        new CustomerDish() { IdCustomer = 1, IdDish = 2 },
        new CustomerDish() { IdCustomer = 2, IdDish = 2 },
        new CustomerDish() { IdCustomer = 3, IdDish = 3 },
        new CustomerDish() { IdCustomer = 4, IdDish = 3 },
        new CustomerDish() { IdCustomer = 5, IdDish = 3 },
        new CustomerDish() { IdCustomer = 5, IdDish = 1 },
        new CustomerDish() { IdCustomer = 5, IdDish = 2 }
      );
      context.SaveChanges();
    }
  }

}