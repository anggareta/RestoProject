using Microsoft.EntityFrameworkCore;
using RestoProject.API.Data;
using RestoProject.API.Entities;

namespace RestoProject.API.Repositories;

public class EFDishRepository : IDishRepository
{
  private readonly RestoProjectContext dbContext;

  public EFDishRepository(RestoProjectContext dbContext)
  {
    this.dbContext = dbContext;
  }

  public async Task<IEnumerable<Dish>> GetAllAsync()
  {
    return await dbContext.TMDish.AsNoTracking().ToListAsync();
  }

  public async Task<Dish?> GetAsync(int id)
  {
    return await dbContext.TMDish.FindAsync(id);
  }

  public async Task CreateAsync(Dish dish)
  {
    dbContext.TMDish.Add(dish);
    await dbContext.SaveChangesAsync();
  }

  public async Task UpdateAsync(Dish updatedDish)
  {
    dbContext.TMDish.Update(updatedDish);
    await dbContext.SaveChangesAsync();
  }

  public async Task DeleteAsync(int id)
  {
    await dbContext.TMDish.Where(dish => dish.Id == id).ExecuteDeleteAsync();
  }
}
