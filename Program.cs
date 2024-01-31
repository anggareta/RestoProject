using RestoProject.API.Data;
using RestoProject.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

app.Services.InitializeDb();

app.MapGet("/", () => "Hello World!");

app.MapCustomerEndpoints();
app.MapDishEndpoints();
app.MapCustDishEndpoints();

app.Run();