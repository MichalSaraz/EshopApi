using DotNetEnv.Configuration;
using EshopApi.Domain.Interfaces;
using EshopApi.Infrastructure;
using EshopApi.Infrastructure.Data;
using EshopApi.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddDotNetEnv();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EshopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MasterConnection")));

builder.Services.AddScoped<InitialData>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var initialData = services.GetRequiredService<InitialData>();
    var context = services.GetRequiredService<EshopDbContext>();

    await initialData.InitializeDatabaseAsync(context);
}

app.Run();
