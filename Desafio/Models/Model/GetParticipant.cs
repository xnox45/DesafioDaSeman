using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Models.Model
{
    public class GetParticipant
    {
        public string Name { get; set; }

        [Required(ErrorMessage = "CPF Obrigatorio")]
        public string Taxnumber { get; set; }

        public EventoModel Evento { get; set; }
    }
}
