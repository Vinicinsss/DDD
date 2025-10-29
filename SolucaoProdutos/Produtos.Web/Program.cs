// ----------------  INÍCIO DO CÓDIGO CORRIGIDO ----------------

using Microsoft.EntityFrameworkCore;
using Produtos.Application.Interfaces;
using Produtos.Infrastructure.Data;
using Produtos.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar a conexão com o banco de dados
// Esta linha lê a string de conexão do arquivo appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura o Entity Framework para usar o SQL Server com a string de conexão.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

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
    pattern: "{controller=Produtos}/{action=Index}/{id?}");

app.Run();

