using Ecommerce_loja_de_Games.Data;
using Ecommerce_loja_de_Games.Model;
using Microsoft.EntityFrameworkCore;
using System;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Ecommerce_loja_de_Games.Service.Implements
{
    public class ProdutosService : IProdutoService
    {
        private readonly AppDbContext _context; // campo usado usado para interagir com o banco de dados somente leitura
        public ProdutosService(AppDbContext context)// context injeção de dependencia 

        {
            _context = context; // isso permite que a classe postagemservice use o contexto banco de dados para realizar operações relacioanados as Produtos
        }
        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos
               .Include(p => p.Categoria)
               .ToListAsync();
        }
        public async Task<Produto?> Create(Produto produto)
        {
            if (produto.Categoria is not null)
            {
                var BuscaCategoria = await _context.Categorias.FindAsync(produto.Categoria.Id);

                if (BuscaCategoria is null)
                    return null;
            }

            produto.Categoria = produto.Categoria is not null ? _context.Categorias.FirstOrDefault(p => p.Id == produto.Categoria.Id) : null;

            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

            return produto;
        }
        public async Task<Produto?> GetById(long id)
        {
            try // depois tratamento de erro caso nao ache nenhuma postagem
            {
                var Produto = await _context.Produtos
                    .Include(p => p.Categoria)
                    .FirstAsync(p => p.Id == id); // primeiro definir o modo de busca
                return Produto;

            }
            catch
            {
                return null;
            }
        }
        public async Task<IEnumerable<Produto>> GetByNome(string nome)

        {
            var Produtos = await _context.Produtos// (local) _context pega todos os livros de postagens e guarda as informações em Postagem
                             .Include(p => p.Categoria)
                             .Where(n => n.Nome.Contains(nome)) // pro end point temas/descricao/atualização - colocar a palavra q identifica atualização
                             .ToListAsync(); // ao achar as postagens que contem Titiulo organize em uma lista e que sera guardada na variavel postagem
                           
            return Produtos; 
        }

        public async Task<IEnumerable<Produto>> GetByPreco(decimal preco)
        {
            var Produto = await _context.Produtos
                 .Include(p => p.Categoria)
                 .Where(p => p.Preco == preco)
                 .ToListAsync();
            return Produto;
        }


        public async Task<IEnumerable<Produto>> GetByIntervaloPre(decimal preco1, decimal preco2)
        {
            var Produto = await _context.Produtos
                 .Include(p => p.Categoria)
                 .Where(p => p.Preco >= preco1 && p.Preco <= preco2)
                 .ToListAsync();
                  return Produto;
            
        }

        // metodo de busca por nome ou console
        public async Task<IEnumerable<Produto>> GetByConsole(string console, string nome)
        {
            var Produto = await _context.Produtos.
                      Include(p => p.Categoria).
                      Where(p => p.Nome.Contains(nome) || p.Console.Contains(console)).
                      ToListAsync();

            return Produto;
        }

        public async  Task<Produto?> Update(Produto Produtos)
        {
            var ProdutoUpdate = await _context.Produtos.FindAsync(Produtos.Id);

            if (ProdutoUpdate is null) // verficando se a informação digitida existe
                return null;

            if(Produtos.Categoria is not null) 
            {
                var Buscaproduto = await _context.Produtos.FindAsync(Produtos.Categoria.Id);
                if (Buscaproduto is null)
                    return null;
            
            }

            Produtos.Categoria = Produtos.Categoria is not null ? _context.Categorias.FirstOrDefault(t => t.Id == Produtos.Categoria.Id) : null; // verificação
         
            _context.Entry(ProdutoUpdate).State = EntityState.Detached; // eu nao quero a informação digitada pra fazer a procura persista
            _context.Entry(Produtos).State = EntityState.Modified; // e a informaçao q quero quer persista
            await _context.SaveChangesAsync(); // salvando a alteração

            return Produtos; // retorno da atualização
        }
        public async Task Delete(Produto Produtos)
        {
            _context.Remove(Produtos);
            await _context.SaveChangesAsync();
        }

        
    }

       
    
}
        



            



                



            










            
            
             
            
