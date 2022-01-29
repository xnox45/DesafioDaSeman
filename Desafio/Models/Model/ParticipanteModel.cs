using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Models.Model
{
    public class ParticipanteModel
    {
        [Required(ErrorMessage = "Nome obrigatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CPF obrigatorio")]
        public string TaxNumber { get; set; }

        [Required(ErrorMessage = "Informações de Data, Local e Nome do evento obrigatoria")]
        public EventoModel Evento { get; set; }
    }
}
