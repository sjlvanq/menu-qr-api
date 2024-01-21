using Microsoft.EntityFrameworkCore;
using MenuApi.Models;

namespace MenuApi.Data
{
    public class MenuDb : DbContext
    {
        public MenuDb(DbContextOptions<MenuDb> options)
        : base(options) {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supply> Supplies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
