using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public Owner Owner { get; set; }

        public string Name { get; set; }

        public string Locality { get; set; }

        public DateTime Date { get; set; }

        public long Tickets { get; set; }
    }
}
