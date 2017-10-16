using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldWebServer.Models
{
    public class WorldDbContext : DbContext
    {
        public WorldDbContext(DbContextOptions<WorldDbContext> options)
        : base(options) { }

        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }

    }
    public class WorldDbContextFactory
    {
        public static WorldDbContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WorldDbContext>();
            optionsBuilder.UseMySQL(connectionString);
            var dbContext = new WorldDbContext(optionsBuilder.Options);
            return dbContext;
        }

    }
}