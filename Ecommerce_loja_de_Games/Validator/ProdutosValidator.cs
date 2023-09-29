using Ecommerce_loja_de_Games.Model;
using FluentValidation;

namespace Ecommerce_loja_de_Games.Validator
{
    public class ProdutosValidator : AbstractValidator<Produto>
    {
          public ProdutosValidator() 
        {
            RuleFor(n => n.Nome)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(30);

            RuleFor(d => d.Descricao)
                    .NotEmpty()
                    .MinimumLength(5)
                    .MaximumLength(255);

            RuleFor(c => c.Console)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(30);

            RuleFor(dl => dl.DataLancamento) // verificar
                    .NotEmpty();


            RuleFor(p => p.Preco)
                .NotNull()
                .GreaterThan(0)
                .PrecisionScale(20, 2, false); ;




            RuleFor(f => f.Foto)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(5000);



        }
    }
}
