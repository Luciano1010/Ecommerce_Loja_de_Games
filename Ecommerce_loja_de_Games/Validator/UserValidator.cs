using Ecommerce_loja_de_Games.Model;
using FluentValidation;

namespace Ecommerce_loja_de_Games.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Nome)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(30);
            
            
            RuleFor(u => u.Senha)
                .NotEmpty()
                .MinimumLength(8); 


            RuleFor(u => u.Foto)
                .MaximumLength(5000);


            RuleFor(u => u.Usuario)
             .NotEmpty()
             .MaximumLength(255)
             .EmailAddress();

           









        }
        
    }
        





}
         




       

                



