using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio.Models.Entity
{
    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Nome do Evento obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Local do Evento obrigatório")]
        public string Locality { get; set; }

        [Required(ErrorMessage = "Data do Evento obrigatório")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Quantidade de Tickets do Evento obrigatório")]
        public long Tickets { get; set; }

        [NotMappedAttribute]
        public long TotalTickets { get; set; }
    }
}
