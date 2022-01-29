using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Models
{
    public class GetEvento
    {
        [Key]
        [Required(ErrorMessage = "Nome do Evento obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Local do Evento obrigatório")]
        public string Locality { get; set; }

        [Required(ErrorMessage = "Data do Evento obrigatório")]
        public DateTime? Date { get; set; }
    }
}
