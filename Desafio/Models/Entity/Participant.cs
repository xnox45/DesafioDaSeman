using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio.Models.Entity
{
    public class Participant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int EventId { get; set; }

        [Required(ErrorMessage = "Nome obrigatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CPF obrigatorio")]
        public string TaxNumber { get; set; }
        
    }
}
