using RestoProject.API.DTOs;
using RestoProject.API.Entities;

public interface ICustDishRepository
{
  Task<IEnumerable<CustDishOut>> GetAllAsync();

  Task<CustomerDish?> GetAsync(CustomerDish cp);

  Task<IEnumerable<Dish>> GetDishAsync(int CustomerId);

  Task<IEnumerable<Customer>> GetCustAsync(int DishId);

  Task CreateAsync(CustDishRecDTO cp);

  Task DeleteAsync(int CustomerId, int DishId);
}