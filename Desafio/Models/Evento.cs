using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Models
{
    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do Evento obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Local do Evento obrigatório")]
        public string Locality { get; set; }

        [Required(ErrorMessage = "Data do Evento obrigatório")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Quantidade de Ticket do Evento obrigatório")]
        public long Tickets { get; set; }
    }
}
