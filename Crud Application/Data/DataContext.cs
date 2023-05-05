using Crud_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud_Application.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Posts)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserID)
                .HasPrincipalKey(e => e.ID);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Tags)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.PostID)
                .HasPrincipalKey(e => e.ID);
        }
    }
}
