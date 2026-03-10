using ECartBulkyWebBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace ECartBulkyWebBooks.Data
{

    public class ApplicationDbContext : DbContext
        {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Action", DisplayOrder = 1 },
                new Category { CategoryId = 2, CategoryName = "Scifi", DisplayOrder = 2 },
                new Category { CategoryId = 3, CategoryName = "History", DisplayOrder = 3 }

);
        }

    }
}
