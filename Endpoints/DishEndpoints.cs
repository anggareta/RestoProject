using RestoProject.API.DTOs;
using RestoProject.API.Entities;
using RestoProject.API.Repositories;

namespace RestoProject.API.Endpoints;

public static class DishEndpoints
{
  const string GetDishEndpointName = "GetDish";

  public static RouteGroupBuilder MapDishEndpoints(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("/dish").WithParameterValidation();

    group.MapGet("/", async (IDishRepository repository) => (await repository.GetAllAsync()).Select(dish => dish.AsDishDTO()));

    group.MapGet("/{id}", async (IDishRepository repository, int id) =>
    {
      Dish? dish = await repository.GetAsync(id);
      return dish is not null ? Results.Ok(dish.AsDishDTO()) : Results.NotFound();
    })
    .WithName(GetDishEndpointName);

    group.MapPost("/", async (IDishRepository repository, CreateDishDTO dishDTO) =>
    {
      Dish dish = new()
      {
        DishName = dishDTO.DishName,
        Price = dishDTO.Price
      };

      await repository.CreateAsync(dish);
      return Results.CreatedAtRoute<Dish>(GetDishEndpointName, new { id = dish.Id }, dish);
    });

    group.MapPut("/{id}", async (IDishRepository repository, int id, UpdateDishDTO updateDishDTO) =>
    {
      Dish? existingDish = await repository.GetAsync(id);

      if (existingDish is null)
      {
        return Results.NotFound();
      }

      existingDish.DishName = updateDishDTO.DishName;
      existingDish.Price = updateDishDTO.Price;
      await repository.UpdateAsync(existingDish);
      return Results.NoContent();
    });

    group.MapDelete("/{id}", async (IDishRepository repository, int id) =>
    {
      Dish? dish = await repository.GetAsync(id);

      if (dish is not null)
      {
        await repository.DeleteAsync(id);
      }

      return Results.NoContent();
    });

    return group;
  }
}