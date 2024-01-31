using RestoProject.API.DTOs;
using RestoProject.API.Entities;
using RestoProject.API.Repositories;

namespace RestoProject.API.Endpoints;

public static class CustomerEndpoints
{
  const string GetCustomerEndpointName = "GetCustomer";

  public static RouteGroupBuilder MapCustomerEndpoints(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("/customer").WithParameterValidation();

    group.MapGet("/", async (ICustomersRepository repository) => (await repository.GetAllAsync()).Select(cust => cust.AsDTO()));

    group.MapGet("/{id}", async (ICustomersRepository repository, int id) =>
    {
      Customer? cust = await repository.GetAsync(id);
      return cust is not null ? Results.Ok(cust.AsDTO()) : Results.NotFound();
    })
    .WithName(GetCustomerEndpointName);

    group.MapPost("/", async (ICustomersRepository repository, CreateCustomerDTO custDTO) =>
    {
      Customer cust = new()
      {
        Name = custDTO.Name,
        Meja = custDTO.Meja,
        BirthDate = custDTO.BirthDate,
      };
      await repository.CreateAsync(cust);
      return Results.CreatedAtRoute<Customer>(GetCustomerEndpointName, new { id = cust.Id }, cust);
    });

    group.MapPut("/{id}", async (ICustomersRepository repository, int id, UpdateCustomerDTO updateCustDTO) =>
    {
      Customer? existingCustomer = await repository.GetAsync(id);

      if (existingCustomer is null)
      {
        return Results.NotFound();
      }

      existingCustomer.Name = updateCustDTO.Name;
      existingCustomer.Meja = updateCustDTO.Meja;
      existingCustomer.BirthDate = updateCustDTO.BirthDate;

      await repository.UpdateAsync(existingCustomer);

      return Results.NoContent();
    });

    group.MapDelete("/{id}", async (ICustomersRepository repository, int id) =>
    {
      Customer? cust = await repository.GetAsync(id);

      if (cust is not null)
      {
        await repository.DeleteAsync(id);
      }

      return Results.NoContent();
    });

    return group;
  }
}