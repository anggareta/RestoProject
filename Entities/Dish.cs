using System.ComponentModel.DataAnnotations;

namespace RestoProject.API.Entities;

public class Dish
{
  public int Id { get; set; }

  [Required]
  [StringLength(50)]
  public required string DishName { get; set; }

  [Range(0, 100)]
  public decimal Price { get; set; }

  public ICollection<CustomerDish>? CustomerDishs { get; set; }
}