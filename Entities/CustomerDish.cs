namespace RestoProject.API.Entities;

public class CustomerDish
{
  public int IdCustomer { get; set; }
  public int IdDish { get; set; }
  public virtual Customer? Customer { get; set; }
  public virtual Dish? Dish { get; set; }
}

public class CustDishOut
{
  public int IdCustomer { get; set; }
  public int IdDish { get; set; }
  public required string CustomerName { get; set; }
  public required string DishName { get; set; }
}

public class DishByCust
{
  public int IdDish { get; set; }
  public required string DishName { get; set; }
}