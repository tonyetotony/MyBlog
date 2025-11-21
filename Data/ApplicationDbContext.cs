using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Entities;

namespace MyBlog.Data
{
    public class ApplicationDbContext : DbContext
    {
        private const string CONNECTION_STRING_NAME = "Default";
        private readonly string _connectionString;
        public DbSet<Post> Posts { get; set; }
        public ApplicationDbContext(IConfiguration configuration) 
        {
            string? connectionString = configuration.GetConnectionString(CONNECTION_STRING_NAME);
            
            if (string.IsNullOrEmpty(connectionString))
                throw new MissingFieldException($"Failed to get connection string with '{CONNECTION_STRING_NAME}'");
            
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
            base.OnConfiguring(optionsBuilder);

            if (optionsBuilder.IsConfigured)
                return;

            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>()
                        .Property(p => p.CreatedAt)
                        .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
