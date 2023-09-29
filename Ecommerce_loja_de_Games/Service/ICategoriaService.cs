using Ecommerce_loja_de_Games.Model;

namespace Ecommerce_loja_de_Games.Service
{
    public interface ICategoriaService
    {

        Task<IEnumerable<Categoria>> GetAll(); // taks trabalha com metodos assincronos, Inumerable trabalha com abstração/ traz todas as postagens

        Task<Categoria?> GetById(long id); // metodo consulta por Id a Categorias

        Task<IEnumerable<Categoria>> GetByTipo(string Tipo); // metodo de busca pelo tipo da Categorias

        Task<Categoria?> Create(Categoria Categorias); // metodo criar nova Categorias

        Task<Categoria?> Update(Categoria Categorias); // metodo de atuaizar

        Task Delete(Categoria Categorias); // metodo deletar
       
     



    }
}
