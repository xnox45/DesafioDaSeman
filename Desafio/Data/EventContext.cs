using Microsoft.EntityFrameworkCore;
using Desafio.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pomelo.EntityFrameworkCore.MySql;

namespace Desafio.Data
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {
        }

        public DbSet<Evento> Events { get; set; }

        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>().Property(p => p.Name).HasMaxLength(80);

            modelBuilder.Entity<Evento>().HasData(
                new Evento { Id = 1, Name = "GeekHunter", Locality = "Fortaleza", Date = DateTime.Now.Date, Tickets = 90 }
                );
        }
    }
}
