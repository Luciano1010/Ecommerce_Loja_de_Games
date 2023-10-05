using Ecommerce_loja_de_Games.Model;

namespace Ecommerce_loja_de_Games.Service
{
    public interface IUserService
    {

        Task<IEnumerable<User>> GetAll(); 

        Task<User?> GetById(long id); 

        Task<User?> GetByUsuario(string usuario); 

        Task<User?> Create(User usuario); 

        Task<User?> Update(User usuario); 

    }
}
