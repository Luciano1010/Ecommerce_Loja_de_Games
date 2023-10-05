using Ecommerce_loja_de_Games.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ecommerce_loja_de_Games.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("tb_produtos"); 
            modelBuilder.Entity<Categoria>().ToTable("tb_Categoria"); 
            modelBuilder.Entity<User>().ToTable("tb_Usuarios"); 


            _ = modelBuilder.Entity<Produto>() // metodo de relacionamento entre clases
                .HasOne(_ => _.Categoria) // tema lado 1
                .WithMany(t => t.Produto) // lado 2, tema ira se relacionar com postagens
                .HasForeignKey("CategoriaId")
                .OnDelete(DeleteBehavior.Cascade);
            
            

        }

        public DbSet<Produto> Produtos { get; set; } = null!; // registrar  Dbset - Obejto responsavel por manipular a Tabela
        public DbSet<Categoria> Categorias { get; set; } = null!; // objeto responsavel por manipular os temas, sendo usado na posatagem service no Getall.
        public DbSet<User> Users { get; set; } = null!;
        public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        {
            public DateOnlyConverter()
                : base(dateOnly =>
                        dateOnly.ToDateTime(TimeOnly.MinValue),
                    dateTime => DateOnly.FromDateTime(dateTime))
            { }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {

            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            base.ConfigureConventions(builder);

        }
    }
}
