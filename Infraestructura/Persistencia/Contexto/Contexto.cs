

using Dominio.Entidates;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Infraestructura.Persistencia.Contexto
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> db) : base(db) { }
        public DbSet<Reservas> Reservas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Inscripciones> Inscripciones { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inscripciones>()
                .ToTable(tb => tb.UseSqlOutputClause(false));

            modelBuilder.Entity<Reservas>()
                .Property(p => p.Estado)
                .ValueGeneratedOnAddOrUpdate()
                .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
            
        }

    }
} 

    


