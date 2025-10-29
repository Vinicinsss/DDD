using Microsoft.AspNetCore.Mvc;
using Produtos.Application.Interfaces;
using Produtos.Application.ViewModels;
using Produtos.Domain.Entities;
using System.Threading.Tasks;

namespace Produtos.Web.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        // A dependência é injetada aqui!
        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            // Mapeamento simples para ViewModel
            var viewModels = produtos.Select(p => new ProdutoViewModel 
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                Estoque = p.Estoque
            }).ToList();
            return View(viewModels);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Mapeamento de ViewModel para Entidade
                var produto = new Produto { Nome = viewModel.Nome, Preco = viewModel.Preco, Estoque = viewModel.Estoque };
                await _produtoRepository.AddAsync(produto);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // ... Implementar Edit, Delete e Details de forma similar ...
        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();

            var viewModel = new ProdutoViewModel { Id = produto.Id, Nome = produto.Nome, Preco = produto.Preco, Estoque = produto.Estoque };
            return View(viewModel);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProdutoViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var produto = new Produto { Id = viewModel.Id, Nome = viewModel.Nome, Preco = viewModel.Preco, Estoque = viewModel.Estoque };
                await _produtoRepository.UpdateAsync(produto);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();

            var viewModel = new ProdutoViewModel { Id = produto.Id, Nome = produto.Nome, Preco = produto.Preco, Estoque = produto.Estoque };
            return View(viewModel);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _produtoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}