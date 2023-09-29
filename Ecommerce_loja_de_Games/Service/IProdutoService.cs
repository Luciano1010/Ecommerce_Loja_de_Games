using Ecommerce_loja_de_Games.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ecommerce_loja_de_Games.Service
{
    public interface IProdutoService
    {

        Task<IEnumerable<Produto>> GetAll(); // taks trabalha com metodos assincronos, Inumerable trabalha com abstração/ traz todas as postagens

        Task<Produto?> GetById(long id); // metodo consulta por Id a Produtos

        Task<IEnumerable<Produto>> GetByPreco(decimal preco);

        Task<IEnumerable<Produto>> GetByIntervaloPre(decimal preco1, decimal preco2);

        Task<IEnumerable<Produto>> GetByConsole(string nome, string console);

        Task<Produto?> Create(Produto produto); // metodo criar nova Produtos
        Task<Produto?> Update(Produto Produtos); // metodo de atuaizar
      
        Task Delete(Produto Produtos); // metodo deletar






    }
}
