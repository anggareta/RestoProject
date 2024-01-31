using RestoProject.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestoProject.API.Data.Configurations;

public class MPConfigurationCP : IEntityTypeConfiguration<CustomerDish>
{
  public void Configure(EntityTypeBuilder<CustomerDish> builder)
  {
    builder.HasKey(cp => new { cp.IdCustomer, cp.IdDish });

    builder.HasOne(cp => cp.Customer)
      .WithMany(c => c.CustomerDishs)
      .HasForeignKey(cp => cp.IdCustomer)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(cp => cp.Dish)
      .WithMany(p => p.CustomerDishs)
      .HasForeignKey(cp => cp.IdDish)
      .OnDelete(DeleteBehavior.Cascade);
  }

}

public class MPConfigurationDish : IEntityTypeConfiguration<Dish>
{
  public void Configure(EntityTypeBuilder<Dish> builder)
  {
    builder.Property(dish => dish.Price).HasPrecision(5, 2);
  }

}