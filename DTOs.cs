using System.ComponentModel.DataAnnotations;

namespace RestoProject.API.DTOs;

public record CustomerDTO(
  int Id,
  string Name,
  DateTime BirthDate,
  string Meja
);

public record CreateCustomerDTO(
  [Required][StringLength(50)] string Name,
  DateTime BirthDate,
  [Required][StringLength(100)] string Meja
);

public record UpdateCustomerDTO(
  [Required][StringLength(50)] string Name,
  DateTime BirthDate,
  [Required][StringLength(100)] string Meja
);

public record DishDTO(
  int Id,
  string DishName,
  [Range(0, 100)] decimal Price
);

public record CreateDishDTO(
  [Required][StringLength(50)] string DishName,
  [Range(0, 100)] decimal Price
);

public record UpdateDishDTO(
  [Required][StringLength(50)] string DishName,
  [Range(0, 100)] decimal Price
);

public record CustomerDishDTO(
  int IdCustomer,
  int IdDish,
  string CustomerName,
  string DishName
);

public record CustDishRecDTO(
  int IdCustomer,
  int IdDish
);