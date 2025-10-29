using Microsoft.EntityFrameworkCore;
using MinhaApp.Application.Interfaces;
using MinhaApp.Infrastructure.Data;
using MinhaApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar o DbContext com a connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Registrar as dependências (Injeção de Dependência)
// Sempre que alguém pedir um IProductRepository, entregue uma instância de ProductRepository.
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();