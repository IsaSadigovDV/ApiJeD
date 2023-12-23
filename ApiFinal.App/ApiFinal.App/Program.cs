using ApiFinal.App.Contexts;
using ApiFinal.App.Profiles.CategoriesMap;
using ApiFinal.App.Repositories.Implementations;
using ApiFinal.App.Repositories.Interfaces;
using ApiFinal.App.Services.Implementations;
using ApiFinal.App.Services.Interfaces;
using ApiFinal.App.Validations.Categories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApiDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Huseyn"));
}
);
builder.Services.AddControllers()?.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CategoryPostDtoValidation>());
builder.Services.AddAutoMapper(typeof(CategoryMapProfile));
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
