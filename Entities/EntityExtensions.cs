using RestoProject.API.DTOs;

namespace RestoProject.API.Entities;

public static class EntityExtensions
{
  public static CustomerDTO AsDTO(this Customer cust)
  {
    return new CustomerDTO(
      cust.Id,
      cust.Name,
      cust.BirthDate,
      cust.Meja
    );
  }

  public static DishDTO AsDishDTO(this Dish dish)
  {
    return new DishDTO(
      dish.Id,
      dish.DishName,
      dish.Price
    );
  }

  public static CustomerDishDTO AsCustDish(this CustDishOut custDishOut)
  {
    return new CustomerDishDTO(
      custDishOut.IdCustomer,
      custDishOut.IdDish,
      custDishOut.CustomerName,
      custDishOut.DishName
  );
  }
}