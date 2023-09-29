using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_loja_de_Games.Model
{
    public class Categoria
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public long Id { get; set; }

        // tipo console/pc
        [Column(TypeName = "Varchar")] 
        [StringLength(100)] 
        public string Tipo { get; set; } = string.Empty;

  


        [InverseProperty("Categoria")]
        public virtual ICollection<Produto>? Produto { get; set; } // Icollection(generica) traz todos objetos de postagem, porque posategm podem ter varios temas

    }
}
