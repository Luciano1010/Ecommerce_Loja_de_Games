namespace Ecommerce_loja_de_Games.Model.Security
{
    public interface IAuthServicecs
    {
        Task<UserLogin?> Autenticar(UserLogin userLogin);
    }
}
