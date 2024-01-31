using RestoProject.API.DTOs;
using RestoProject.API.Entities;

namespace RestoProject.API.Endpoints;

public static class CustDishEndpoints
{
  const string GetCPEndpointName = "GetCustomerDish";

  public static RouteGroupBuilder MapCustDishEndpoints(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("/custdish").WithParameterValidation();

    group.MapGet("/", async (ICustDishRepository repository) => (await repository.GetAllAsync()).Select(cp => cp.AsCustDish()))
      .WithName(GetCPEndpointName);

    group.MapGet("/bydish/{id}", async (ICustDishRepository repository, int id) => (await repository.GetCustAsync(id)).Select(cust => cust.AsDTO()));

    group.MapGet("/bycustomer/{id}", async (ICustDishRepository repository, int id) => (await repository.GetDishAsync(id)).Select(dish => dish.AsDishDTO()));

    group.MapPost("/", async (ICustDishRepository repository, CustDishRecDTO cp) =>
    {
      await repository.CreateAsync(cp);
      return Results.Created("/custdish", cp);
    });

    group.MapDelete("/{customer}/{dish}", async (ICustDishRepository repository, int customer, int dish) =>
    {
      await repository.DeleteAsync(customer, dish);
      return Results.NoContent();
    });

    return group;
  }
}