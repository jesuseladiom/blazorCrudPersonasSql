using BlazorCrudPersonasSql.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrudPersonasSql.Server
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               :base(options)
        {

        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Pais> Paises { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    var connectionString = configuration.GetConnectionString("DefaultConnection");
        //    optionsBuilder.UseSqlServer(connectionString);
        //}
    }

    //public class ApplicationDbContext2 : IdentityDbContext
    //{
    //    public ApplicationDbContext2(DbContextOptions<ApplicationDbContext2> options)
    //           : base(options)
    //    {

    //    }

    //    public DbSet<Persona> Personas { get; set; }

    //    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    //{
    //    //    var configuration = new ConfigurationBuilder()
    //    //        .SetBasePath(Directory.GetCurrentDirectory())
    //    //        .AddJsonFile("appsettings.json")
    //    //        .Build();

    //    //    var connectionString = configuration.GetConnectionString("DefaultConnection");
    //    //    optionsBuilder.UseSqlServer(connectionString);
    //    //}
    //}


}
