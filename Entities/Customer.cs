using System.ComponentModel.DataAnnotations;

namespace RestoProject.API.Entities;

public class Customer
{
  public int Id { get; set; }

  [Required]
  [StringLength(50)]
  public required string Name { get; set; }

  public DateTime BirthDate { get; set; }

  [Required]
  [StringLength(100)]
  public required string Meja { get; set; }

  public ICollection<CustomerDish>? CustomerDishs { get; set; }
}