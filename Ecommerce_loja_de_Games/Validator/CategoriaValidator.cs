using Ecommerce_loja_de_Games.Model;
using FluentValidation;

namespace Ecommerce_loja_de_Games.Validator
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(c => c.Tipo)
                    .NotEmpty() 
                    .MinimumLength(2) 
                    .MaximumLength(20); 

          
        }
    }
}
