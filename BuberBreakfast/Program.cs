using BuberBreakfast.Services.Breakfasts;
using BuberBreakfast.Services.Breakfasts.impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Services.AddControllers();
    // dependency injection
    // @Service
    builder.Services.AddScoped<IBreakfastService, BreakfastServiceImpl>(); //
}

var app = builder.Build();

// Configure the HTTP request pipeline.
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
