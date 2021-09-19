using JWTSpa_Autentication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Data
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculasActores> PeliculasActores { get; set; }
        public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }
        public DbSet<SalaDeCine> SalasDeCines { get; set; }
        public DbSet<PeliculasSalasDeCine> PeliculasSalasDeCines { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PeliculasActores>().HasKey(x => new { x.ActorId, x.PeliculaId });

            
            modelBuilder.Entity<PeliculasGeneros>().HasKey(x => new { x.GeneroId, x.PeliculaId });
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<PeliculasSalasDeCine>()
                .HasKey(x => new { x.PeliculaId, x.SalaDeCineId });

                
        }
    }
}
