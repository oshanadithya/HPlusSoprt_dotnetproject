using Microsoft.EntityFrameworkCore;

namespace HPlusSportAPI.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Active Wear" }
            );

            modelBuilder.Entity<Product>().HasData(
                 new Product { Id = 1, CategoryId = 1, Name = "Trainers", Sku = "Testing", Description = "Sample Description", Price = 100, IsAvailable = true }
            );
        }
    }
}
