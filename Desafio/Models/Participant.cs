using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Models
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public string Name { get; set; }

        public string TaxNumber { get; set; }
        
       
        
    }
}
