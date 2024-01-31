using RestoProject.API.Entities;

namespace RestoProject.API.Repositories;

public interface ICustomersRepository
{
  Task CreateAsync(Customer cust);
  Task DeleteAsync(int id);
  Task<Customer?> GetAsync(int id);
  Task<IEnumerable<Customer>> GetAllAsync();
  Task UpdateAsync(Customer updatedCustomer);
}
