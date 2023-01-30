using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.Models.DTO;

namespace ProjetWebAPI.DAL
{
    public class DbFactoryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        
        protected readonly IConfiguration Configuration;

        public DbFactoryContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("Production");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
