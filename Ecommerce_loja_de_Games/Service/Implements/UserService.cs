using Ecommerce_loja_de_Games.Data;
using Ecommerce_loja_de_Games.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_loja_de_Games.Service.Implements
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)

        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
               .ToListAsync();
              
        }
        public async Task<User?> GetById(long id)
        {
            try
            {
                var Usuario = await _context.Users
                    .FirstAsync(i => i.Id == id);
                   

                Usuario.Senha = ""; // no fronte pra atualizar a senha
                return Usuario;

            }
            catch
            {
                return null;
            }
        }

        public async Task<User?> GetByUsuario(string usuario)
        {
            try
            {
                var Buscausuario = await _context.Users
                    .Where(u => u.Usuario == usuario)
                    .FirstOrDefaultAsync();
                  

                return Buscausuario;
            }
            catch
            {
                return null;

            }
        }
        public async Task<User?> Create(User usuario)
            {
              var Buscausuario = await GetByUsuario(usuario.Usuario);

            if (Buscausuario is not null)
            {
                return null;
            }
            if (usuario.Foto is null || usuario.Foto == "")
                usuario.Foto = "https://i.imgur.com/I8MfmC8.png";

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha, workFactor: 10); // metodo de criptografia

            await _context.Users.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }
        public async Task<User?> Update(User usuario)
            {
            var UsuarioUpdate = await _context.Users.FindAsync(usuario.Id);  // verificação 1

            if (UsuarioUpdate is null) // caso a 1 verificaçao for nula retonar null
                return null;

            if (usuario.Foto is null || usuario.Foto == "") 
                usuario.Foto = "https://i.imgur.com/I8MfmC8.png";

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha, workFactor: 10);

            _context.Entry(UsuarioUpdate).State = EntityState.Detached;
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return usuario;
        }
        

    }

}







