using RestoProject.API.Entities;

namespace RestoProject.API.Repositories;

public interface IDishRepository
{
  Task CreateAsync(Dish dish);
  Task DeleteAsync(int id);
  Task<Dish?> GetAsync(int id);
  Task<IEnumerable<Dish>> GetAllAsync();
  Task UpdateAsync(Dish updatedDish);
}