using Microsoft.EntityFrameworkCore;
using RestoProject.API.Data;
using RestoProject.API.DTOs;
using RestoProject.API.Entities;

namespace RestoProject.API.Repositories;
public class EFCustDishRepository : ICustDishRepository
{
  private readonly ILogger _logger;
  private readonly RestoProjectContext dbContext;

  public EFCustDishRepository(RestoProjectContext dbContext, ILogger<EFCustDishRepository> logger)
  {
    this.dbContext = dbContext;
    _logger = logger;
  }

  public async Task<IEnumerable<CustDishOut>> GetAllAsync()
  {
    //return await dbContext.TTDish.AsNoTracking().ToListAsync();
    var T = dbContext.TTCustDish;
    var C = dbContext.TMCustomer;
    var P = dbContext.TMDish;
    var o = from x in T
            join c in C on x.IdCustomer equals c.Id
            join p in P on x.IdDish equals p.Id
            select new CustDishOut
            {
              IdCustomer = x.IdCustomer,
              IdDish = x.IdDish,
              CustomerName = c.Name,
              DishName = p.DishName
            };
    _logger.LogError("masuk");
    return await o.ToListAsync();
  }

  public async Task<CustomerDish?> GetAsync(CustomerDish cp)
  {
    var o = from a in dbContext.TTCustDish
            where a.IdCustomer == cp.IdCustomer && a.IdDish == cp.IdDish
            select new CustomerDish
            {
              IdCustomer = a.IdCustomer,
              IdDish = a.IdDish
            };

    return await o.FirstAsync();
  }

  public async Task<IEnumerable<Customer>> GetCustAsync(int DishId)
  {
    var C = dbContext.TMCustomer;
    var T = dbContext.TTCustDish;
    var o = from c in C
            join x in T on c.Id equals x.IdCustomer
            where x.IdDish == DishId
            select new Customer
            {
              Id = c.Id,
              Name = c.Name,
              BirthDate = c.BirthDate,
              Meja = c.Meja
            };

    return await o.ToListAsync();
  }

  public async Task<IEnumerable<Dish>> GetDishAsync(int CustomerId)
  {
    var P = dbContext.TMDish;
    var T = dbContext.TTCustDish;
    var o = from p in P
            join x in T on p.Id equals x.IdDish
            where x.IdCustomer == CustomerId
            select new Dish
            {
              Id = p.Id,
              DishName = p.DishName,
              Price = p.Price
            };

    return await o.ToListAsync();
  }

  public async Task CreateAsync(CustDishRecDTO cpr)
  {
    var c = await (from x in dbContext.TMCustomer
                   where x.Id == cpr.IdCustomer
                   select x).FirstOrDefaultAsync();
    var p = await (from x in dbContext.TMDish
                   where x.Id == cpr.IdDish
                   select x).FirstOrDefaultAsync();

    if (c is not null && p is not null)
    {
      var o = await (from x in dbContext.TTCustDish
                     where x.IdCustomer == cpr.IdCustomer && x.IdDish == cpr.IdDish
                     select new CustDishOut
                     {
                       IdCustomer = x.IdCustomer,
                       IdDish = x.IdDish,
                       CustomerName = c.Name,
                       DishName = p.DishName
                     }).FirstOrDefaultAsync();
      //_logger.LogError("diisi:" + c.Name);
      if (o is null)
      {
        CustomerDish cp = new()
        {
          IdCustomer = cpr.IdCustomer,
          IdDish = cpr.IdDish
        };
        dbContext.TTCustDish.Add(cp);
        await dbContext.SaveChangesAsync();
      }
      else
      {
        await Task.CompletedTask;
      }
    }
    else
    {
      await Task.CompletedTask;
    }
  }

  public async Task DeleteAsync(int CustomerId, int DishId)
  {
    await dbContext.TTCustDish.Where(cp => cp.IdCustomer == CustomerId && cp.IdDish == DishId).ExecuteDeleteAsync();
  }

}