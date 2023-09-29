using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_loja_de_Games.Model
{
    public class Produto 
    {

        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public long Id { get; set; } 
        // nome
        [Column(TypeName = "Varchar")] 
        [StringLength(255)] 
        public string Nome { get; set; } = string.Empty; 

        // Descrição
        [Column(TypeName = "Varchar")]
        [StringLength(1000)] 
        public string Descricao { get; set; } = string.Empty;

        // Console
        [Column(TypeName = "Varchar")]
        [StringLength(100)] 
        public string Console { get; set; } = string.Empty;

       

        // preco
       [Column(TypeName = "Decimal (6,2) ")]
       public decimal Preco { get; set; } 
        




        // Data de lançamento
        [Column(TypeName = "Date")]
        
        public DateOnly? DataLancamento { get; set; } 


        // Foto
        [Column(TypeName = "Varchar")]
        [StringLength(5000)]
        public string Foto { get; set; } = string.Empty;





        public virtual Categoria? Categoria { get; set; } // chave estrangeira vem da tabela categoria.

    }

}








