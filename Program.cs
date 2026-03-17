using Microsoft.EntityFrameworkCore;
using ProductValidation.Data;
using ProductValidation.Services;
using ProductValidation.Services.Interfaces;
using ProductValidation.Repositories;
using ProductValidation.Extensions;
using ProductValidation.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseLazyLoadingProxies()
);

// Add services to the container.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IProductGetService, ProductService>();
builder.Services.AddScoped<IProductSetService, ProductService>();
builder.Services.AddScoped<IUserSetService, UserService>();
builder.Services.AddScoped<ICategorySetService, CategoryService>();

builder.Services.AddScoped<AuthService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add JWT authentication middleware
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

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
