using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Models.Model
{
    public class EventoModel
    {
        [Required(ErrorMessage = "Nome do Evento obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Local do Evento obrigatório")]
        public string Locality { get; set; }

        [Required(ErrorMessage = "Data do Evento obrigatório")]
        public DateTime? Date { get; set; }
    }
}
