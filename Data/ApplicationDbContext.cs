namespace TestAPI.Data
{
    using Microsoft.EntityFrameworkCore;
    using TestAPI.Models;

    public class ApplicationDbContext : DbContext
    {
        // The DbSet property will tell EF Core tha we have a table that needs to be created
        public virtual DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // On model creating function will provide us with the ability to manage the tables properties
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}