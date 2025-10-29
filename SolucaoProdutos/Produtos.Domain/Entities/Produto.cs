namespace Produtos.Domain.Entities
{
    // POCO - Plain Old CLR Object
    // Representa a nossa entidade de domínio principal.
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }

        // Poderíamos ter regras de negócio aqui, como:
        // public void DarBaixaEstoque(int quantidade) { ... }
    }
}