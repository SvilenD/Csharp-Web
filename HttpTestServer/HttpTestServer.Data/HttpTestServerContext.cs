namespace HttpTestServer.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;

    using HttpTestServer.Data.Models;

    public class HttpTestServerContext : DbContext
    {
        private const string ConnectionString = @"Server=.;Database=HttpTestServer;Integrated Security=True";

        public DbSet<Session> Sessions{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>()
                .Property(s => s.FirstLogin)
                .HasDefaultValue(DateTime.UtcNow);
            modelBuilder.Entity<Session>()
                .Property(s => s.LastLogin)
                .HasDefaultValue(DateTime.UtcNow);
        }
    }
}