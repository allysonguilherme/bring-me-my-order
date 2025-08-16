using OrderApplication.Services.Interfaces;
using OrderInfraIOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Dependencies.ConfigureServices(builder.Configuration, builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/orders", async (IOrderFacade orderFacade) =>
    {
        var orders = await orderFacade.GetOrders();
        return orders.Any() ? Results.Ok(orders) : Results.NoContent();
    })
    .WithName("GetOrders")
    .WithOpenApi();

app.Run();