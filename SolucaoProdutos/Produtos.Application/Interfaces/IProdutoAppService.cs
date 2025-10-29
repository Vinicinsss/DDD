using Produtos.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Produtos.Application.Interfaces
{
    // Contrato para o repositório de dados de Produto
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task AddAsync(Produto produto);
        Task UpdateAsync(Produto produto);
        Task DeleteAsync(int id);
    }
}