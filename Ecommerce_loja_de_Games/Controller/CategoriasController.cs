using Ecommerce_loja_de_Games.Model;
using Ecommerce_loja_de_Games.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_loja_de_Games.Controller
{
    [Authorize]
    [Route("~/categorias")]

    [ApiController]
    public class CategoriasController : ControllerBase
    {


            private readonly ICategoriaService _categoriaservice; // aqui a interface vai manipular os dados sem necessidade de mexer nos dados da classe
            private readonly IValidator<Categoria> _categoriaValidator; // metodo q avalia se a postagem pode ser feita

            public CategoriasController(ICategoriaService categoriaservice, IValidator<Categoria> categoriaValidator)

            {
                _categoriaservice = categoriaservice;
                _categoriaValidator = categoriaValidator;
            }

        [HttpGet]

        public async Task<ActionResult> GetAll()
        {

            return Ok(await _categoriaservice.GetAll());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetbyId(long id)
        {
            var Resposta = await _categoriaservice.GetById(id);

            if (Resposta == null)
                return NotFound();

            return Ok(Resposta);
        }

        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult> GetbyTipo(string tipo)
        {
            var Resposta = await _categoriaservice.GetByTipo(tipo);

            if (Resposta == null)
                return NotFound();

            return Ok(Resposta);
        }




        [HttpPost] // criar

        public async Task<ActionResult> Create([FromBody] Categoria categorias) // frombody o objeto postagem no corpo da requisição
        {
            var validarcategorias = await _categoriaValidator.ValidateAsync(categorias); // usando o postagem validator vai verificar se o objeto esta dentro dos parametros e sera guardado no validarPostagem         
            
            if (!validarcategorias.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarcategorias); // que indica que a requisição (ou a validação) não foi bem-sucedida, e ainda fornece o q foi escrito no validarPostagem para fornecer mais detalhes
            }
            await _categoriaservice.Create(categorias);

            return CreatedAtAction(nameof(GetbyId), new { id = categorias.Id }, categorias);
            // return volta com um valor, createdAtaction manda o codigo de status 200, nameof é pegar um id novo, new cria o objeto junto com o seu id, postagem é o objeto criado com sucesso. 
        }


        [HttpPut] // atualizar
        public async Task<ActionResult> Update([FromBody] Categoria categorias)
        {
            if (categorias.Id == 0) // nao tem id 0 ou numeros negativos
                return BadRequest("Id da postagem é invalido");

            var validarcategorias = await _categoriaValidator.ValidateAsync(categorias);
            if (!validarcategorias.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarcategorias);
            }

            var Resposta = await _categoriaservice.Update(categorias); // aqui atualiza se encontrar o id 

            if (Resposta is null)
                return NotFound("Postagem não Encontrada");

            return Ok(Resposta);



        }
        [HttpDelete("{id}")] // delete 

        public async Task<IActionResult> Delete(long Id)
        {
            var BuscaPostagem = await _categoriaservice.GetById(Id);


            if (BuscaPostagem is null)
                return NotFound("Postagem não encontrada");

            await _categoriaservice.Delete(BuscaPostagem);
            return NoContent();


        }
      


    }
}
      
        

