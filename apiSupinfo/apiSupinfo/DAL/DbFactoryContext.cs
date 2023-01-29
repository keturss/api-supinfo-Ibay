using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.Models.DTO;

namespace ProjetWebAPI.DAL
{
    public class DbFactoryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }


        public DbFactoryContext(DbContextOptions<DbFactoryContext> options)
            : base(options)
        {

        }
    }
}
