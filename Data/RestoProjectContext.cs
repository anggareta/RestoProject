using System.Reflection;
using RestoProject.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace RestoProject.API.Data;

public class RestoProjectContext : DbContext
{
  public RestoProjectContext(DbContextOptions<RestoProjectContext> options) : base(options)
  {
  }

  public DbSet<Customer> TMCustomer => Set<Customer>();
  public DbSet<Dish> TMDish => Set<Dish>();
  public DbSet<CustomerDish> TTCustDish => Set<CustomerDish>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}