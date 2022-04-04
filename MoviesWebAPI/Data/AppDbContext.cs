using Microsoft.EntityFrameworkCore;
using MoviesWebAPI.Data.Models;
using System.Diagnostics.CodeAnalysis;

namespace MoviesWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
    }
}
