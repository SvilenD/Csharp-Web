using DemoApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Data
{
    public class ApplicationDbContext :DbContext
    {
        private const string ConnectionString = @"Server=.;Database=DemoApp;Integrated Security=True";

        public DbSet<Tweet> Tweets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}