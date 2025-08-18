using OrderApplication.DTOs;
using OrderApplication.Services.Interfaces;
using OrderInfraIOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Dependencies.ConfigureServices(builder.Configuration, builder.Services);
builder.Services.AddApiVersioning()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();
var order = app.NewVersionedApi();
var v1 = order.MapGroup("/api/v{version:apiVersion}/order").HasApiVersion(1.0);

app.UseSwagger(); 
app.UseSwaggerUI();


app.UseHttpsRedirection();

v1.MapGet("/", async (IOrderFacade orderFacade) =>
    {
        var orders = await orderFacade.GetOrders();
        return orders.Any() ? Results.Ok(orders) : Results.NoContent();
    })
    .WithName("GetOrders")
    .WithOpenApi();

v1.MapGet("/{id}", async (IOrderFacade orderFacade, int id) =>
    {
        var order = await orderFacade.GetOrder(id);
        
        return order != null ? Results.Ok(order) : Results.NotFound();
    })
    .WithName("GetOrder")
    .WithOpenApi();

v1.MapPost("/", async (IOrderFacade orderFacade, CreateOrderDto order) =>
    {
        order.Validate();
        if (order.IsValid is false)
        {
            return Results.BadRequest(order.Notifications);
        }

        var created = await orderFacade.CreateOrder(order);
        return created ? Results.Created() : Results.UnprocessableEntity();
    })
    .WithName("CreateOrder")
    .WithOpenApi();

v1.MapDelete("/{id}", async (IOrderFacade orderFacade, int id) =>
    {
        var deleted = await orderFacade.CancelOrder(id);
        
        if (deleted is null)
        {
            return Results.NotFound();
        }

        return (bool)deleted ? Results.Ok() : Results.UnprocessableEntity();
    })
    .WithName("CancelOrder")
    .WithOpenApi();

app.Run();