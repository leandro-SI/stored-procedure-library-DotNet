using ConsultaSP.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaSP.API.Context
{
    public partial class TesteContext : DbContext
    {
        public TesteContext()
        {

        }

        public TesteContext(DbContextOptions<TesteContext> opt) : base(opt)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Pessoa>().HasData(
                new Pessoa { Id = 1, Nome = "Leandro Cesar", Idade = 32},
                new Pessoa { Id = 2, Nome = "Luciana dos Santos", Idade = 49 }
            );
        }

    }
}
