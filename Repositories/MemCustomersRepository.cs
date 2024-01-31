using RestoProject.API.Entities;

namespace RestoProject.API.Repositories;

public class MemCustomersRepository : ICustomersRepository
{
  private readonly List<Customer> customer = new()
  {
    new Customer(){
      Id = 1,
      Name = "Ayus",
      BirthDate = new DateTime(1981,3,11),
      Meja = "1",
    },
    new Customer(){
      Id = 2,
      Name = "Meisha",
      BirthDate = new DateTime(1994,7,7),
      Meja = "2",
    },
    new Customer(){
      Id = 3,
      Name = "ZetDebKen",
      BirthDate = new DateTime(2015,11,15),
      Meja = "3",
    }
  };

  public async Task<IEnumerable<Customer>> GetAllAsync()
  {
    return await Task.FromResult(customer);
  }

  public async Task<Customer?> GetAsync(int id)
  {
    return await Task.FromResult(customer.Find(cust => cust.Id == id));
  }

  public async Task CreateAsync(Customer cust)
  {
    cust.Id = customer.Max(cust => cust.Id) + 1;
    customer.Add(cust);

    await Task.CompletedTask;
  }

  public async Task UpdateAsync(Customer updatedCust)
  {
    var index = customer.FindIndex(cust => cust.Id == updatedCust.Id);
    customer[index] = updatedCust;

    await Task.CompletedTask;
  }

  public async Task DeleteAsync(int id)
  {
    var index = customer.FindIndex(cust => cust.Id == id);
    customer.RemoveAt(index);

    await Task.CompletedTask;
  }
}