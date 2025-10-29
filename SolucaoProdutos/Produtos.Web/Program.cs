// Usings necessários para EF Core, interfaces e repositórios.
using Microsoft.EntityFrameworkCore;
using Produtos.Application.Interfaces;
using Produtos.Infrastructure.Data;
using Produtos.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar a conexão com o banco de dados
// Esta linha lê a string de conexão do arquivo appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura o Entity Framework para usar o SQL Server com a string de conexão.
// Certifique-se de que os espaços aqui são espaços normais.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Registrar as dependências (Injeção de Dependência)
// Diz ao ASP.NET: "Quando um controller pedir um IProdutoRepository, 
// entregue uma instância da classe ProdutoRepository".
// AddScoped garante que a mesma instância seja usada durante uma requisição HTTP.
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Adiciona os serviços essenciais para o funcionamento do padrão MVC.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())
{
    // Certifique-se de que os espaços aqui são espaços normais.
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rota padrão ajustada para iniciar diretamente no nosso controller de Produtos.
// Certifique-se de que os espaços aqui são espaços normais.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produtos}/{action=Index}/{id?}");

app.Run();