using Ecommerce_loja_de_Games.Model;
using Ecommerce_loja_de_Games.Model.Security;
using Ecommerce_loja_de_Games.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_loja_de_Games.Controller
{
    [Route("~/usuarios")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService; 
        private readonly IValidator<User> _userValidator;
        private readonly IAuthServicecs _authService;
        public UserController(IUserService UserService, IValidator<User> UserValidator, IAuthServicecs authService)
        {
            _userService = UserService;
            _userValidator = UserValidator;
            _authService = authService;
  
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {

            return Ok(await _userService.GetAll());

        }
        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetbyId(long id)
        {
            var Resposta = await _userService.GetById(id);

            if (Resposta == null)
                return NotFound();

            return Ok(Resposta);
        }

        [AllowAnonymous]
        [HttpPost("cadastrar")]
        public async Task<ActionResult> Create([FromBody] User user) 
        {
            var validarusers = await _userValidator.ValidateAsync(user); 
            if (!validarusers.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarusers); 
            }
            var Resposta = await _userService.Create(user);

            if (Resposta is null)
                return BadRequest("Usuario já cadastrado!");

            return CreatedAtAction(nameof(GetbyId), new { id = user.Id }, user);
            
        }



        [Authorize]
        [HttpPut("atualizar")] 
        public async Task<ActionResult> Update([FromBody] User user)
        {

            if (user.Id == 0) 
                return BadRequest("Id da postagem é invalido");

            var validaruser = await _userValidator.ValidateAsync(user);
            if (!validaruser.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validaruser);
            }

            var Userupadate = await _userService.GetByUsuario(user.Usuario);

            if (Userupadate is not null && Userupadate.Id != user.Id)
                return BadRequest("o Usuario(e-mail) ja esta em uso");

            var Resposta = await _userService.Update(user);

            if (Resposta is null)
                return NotFound("Postagem não Encontrada");

            return Ok(Resposta);

        }
       
        [AllowAnonymous]
        [HttpPost("logar")]
        public async Task<ActionResult> Autenticar([FromBody] UserLogin userlogin)
        {
            var Resposta = await _authService.Autenticar(userlogin);

            if (Resposta is null)
                return Unauthorized("Usuario e/ou Senha são invalidos");

            return Ok(Resposta);
        }
            

    }
}



