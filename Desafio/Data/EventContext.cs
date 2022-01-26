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
        public DbSet<Event> Events { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Participant> Participants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ServerVersion serverVersion = null;
            optionsBuilder.UseMySql("server=localhost;user=root;password=Undethome45;port=3306;database=Desafio; persistsecurityinfo = True;",
            new MySqlServerVersion(new Version()));

          // var a =  "server = localhost; userid = root; password = rajeesh123; database = WebAppMySql; persistsecurityinfo = True />";
        }
    }
}
