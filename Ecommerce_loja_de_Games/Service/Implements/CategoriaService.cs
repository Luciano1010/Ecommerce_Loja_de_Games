using Ecommerce_loja_de_Games.Data;
using Ecommerce_loja_de_Games.Model;
using Microsoft.EntityFrameworkCore;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Ecommerce_loja_de_Games.Service.Implements
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context; 
        public CategoriaService(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await _context.Categorias.
                            Include(p => p.Produto).
                            ToListAsync();
        }
    

        public async Task<Categoria?> GetById(long id)
        {
            try 
            {
                var Categoria = await _context.Categorias
                    .Include(p => p.Produto)
                    .FirstAsync(c => c.Id == id); 
                return Categoria;
                 
            }
            catch
            {
                return null;
            }
        }
        public async Task<IEnumerable<Categoria>> GetByTipo(string Tipo)
        {
            var Categoria = await _context.Categorias 
                                  .Include(p => p.Produto)   
                                  .Where(c => c.Tipo.Contains(Tipo)) 
                                  .ToListAsync(); // ao achar as postagens que contem Titiulo organize em uma lista e que sera guardada na variavel postagem
                                
            return Categoria; // retorne a lista 

        }
        public async Task<Categoria?> Create(Categoria Categorias)  
        {

            await _context.Categorias.AddAsync(Categorias); 
            await _context.SaveChangesAsync(); 

            return Categorias;
        }

        public async Task<Categoria?> Update(Categoria Categorias)
        {
            var CategoriaUpdate = await _context.Categorias.FindAsync(Categorias.Id);

            if (CategoriaUpdate is null) 
                return null;

            _context.Entry(CategoriaUpdate).State = EntityState.Detached;
            _context.Entry(Categorias).State = EntityState.Modified; 
            await _context.SaveChangesAsync(); 

            return Categorias;
        }
        public async Task Delete(Categoria Categorias)
        {
            _context.Remove(Categorias);
            await _context.SaveChangesAsync();
        }
       
        
    }

}






      


        
