using Ecommerce_loja_de_Games.Model;
using Ecommerce_loja_de_Games.Service;
using Ecommerce_loja_de_Games.Service.Implements;
using Ecommerce_loja_de_Games.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_loja_de_Games.Controller
{


    [Route("~/produtos")]

    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly IProdutoService _produtoService; // aqui a interface vai manipular os dados sem necessidade de mexer nos dados da classe
        private readonly IValidator<Produto> _produtoValidator; // metodo q avalia se a postagem pode ser feita

        public ProdutosController(IProdutoService produtoService, IValidator<Produto> produtoValidator)

        {
            _produtoService = produtoService;
            _produtoValidator = produtoValidator;
        }
        [HttpGet]

        public async Task<ActionResult> GetAll()
        {

            return Ok(await _produtoService.GetAll());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetbyId(long id)
        {
            var Resposta = await _produtoService.GetById(id);

            if (Resposta == null)
                return NotFound();

            return Ok(Resposta);
        }



        [HttpGet("preco/{preco}")]
        public async Task<ActionResult> GetByPreco(decimal preco)

        {
            return Ok(await _produtoService.GetByPreco(preco));
        }

        [HttpGet("preco1/{preco1}/preco2/{preco2}")]
        public async Task<ActionResult> GetByIntervaloPre(decimal preco1, decimal preco2)

        {
            return Ok(await _produtoService.GetByIntervaloPre( preco1, preco2));
        }

        [HttpGet("nome/{nome}/console/{console}")]
        public async Task<ActionResult> GetByConsole(string nome, string console)
        {
            return Ok(await _produtoService.GetByConsole(nome, console));
        }



        [HttpPost] // criar

        public async Task<ActionResult> Create([FromBody] Produto produtos) // frombody o objeto postagem no corpo da requisição
        {
            var validarprodutos = await _produtoValidator.ValidateAsync(produtos); // usando o postagem validator vai verificar se o objeto esta dentro dos parametros e sera guardado no validarPostagem         

            if (!validarprodutos.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarprodutos); // que indica que a requisição (ou a validação) não foi bem-sucedida, e ainda fornece o q foi escrito no validarPostagem para fornecer mais detalhes
            }
            var Resposta = await _produtoService.Create(produtos);

            if (Resposta is null)
                return BadRequest("Tema não encontrado");

            return CreatedAtAction(nameof(GetbyId), new { id = produtos.Id }, produtos);
            // return volta com um valor, createdAtaction manda o codigo de status 200, nameof é pegar um id novo, new cria o objeto junto com o seu id, postagem é o objeto criado com sucesso. 
        }


        [HttpPut] // atualizar
        public async Task<ActionResult> Update([FromBody] Produto produtos)
        {
            if (produtos.Id == 0) // nao tem id 0 ou numeros negativos
                return BadRequest("Id da postagem é invalido");

            var validarprodutos = await _produtoValidator.ValidateAsync(produtos);
            
            if (!validarprodutos.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarprodutos);
            }

            var Resposta = await _produtoService.Update(produtos); // aqui atualiza se encontrar o id 

            if (Resposta is null)
                return NotFound("Postagem não Encontrada");

            return Ok(Resposta);

        }
        [HttpDelete("{id}")] // delete 

        public async Task<IActionResult> Delete(long Id)
        {
            var BuscaPostagem = await _produtoService.GetById(Id);


            if (BuscaPostagem is null)
                return NotFound("Postagem não encontrada");

            await _produtoService.Delete(BuscaPostagem);
            return NoContent();


        }

    }
}
