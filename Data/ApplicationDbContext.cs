using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TpFinalJoaquinGaido.Models;

namespace TpFinalJoaquinGaido.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LaboratorioFinal;Trusted_Connection=True;MultipleActiveResultSets=True");
        }


        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Marcas> Marcas { get; set; }
        public DbSet<Categorias> Categorias { get; set; }

    }
}
