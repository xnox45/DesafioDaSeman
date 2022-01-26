using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string TaxNumber { get; set; }

        public string Mail { get; set; }

        public string Phone { get; set; }

        public List<Event> Events { get; set; }
    }
}
