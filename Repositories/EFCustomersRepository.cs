using Microsoft.EntityFrameworkCore;
using RestoProject.API.Data;
using RestoProject.API.Entities;

namespace RestoProject.API.Repositories;

public class EFCustomersRepository : ICustomersRepository
{
  private readonly RestoProjectContext dbContext;

  public EFCustomersRepository(RestoProjectContext dbContext)
  {
    this.dbContext = dbContext;
  }

  public async Task<IEnumerable<Customer>> GetAllAsync()
  {
    return await dbContext.TMCustomer.AsNoTracking().ToListAsync();
  }

  public async Task<Customer?> GetAsync(int id)
  {
    return await dbContext.TMCustomer.FindAsync(id);
  }

  public async Task CreateAsync(Customer cust)
  {
    dbContext.TMCustomer.Add(cust);
    await dbContext.SaveChangesAsync();
  }

  public async Task UpdateAsync(Customer updatedCustomer)
  {
    dbContext.TMCustomer.Update(updatedCustomer);
    await dbContext.SaveChangesAsync();
  }

  public async Task DeleteAsync(int id)
  {
    await dbContext.TMCustomer.Where(cust => cust.Id == id).ExecuteDeleteAsync();
  }
}