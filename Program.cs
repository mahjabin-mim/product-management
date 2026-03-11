using Microsoft.EntityFrameworkCore;
using ProductValidation.Data;
using ProductValidation.Services;
using ProductValidation.Services.Interfaces;
using ProductValidation.Repositories;
using ProductValidation.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseLazyLoadingProxies()
    
);

// Add services to the container.
builder.Services.AddScoped<IProductGetService, ProductService>();
builder.Services.AddScoped<IProductSetService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<AuthService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add JWT authentication middleware
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
