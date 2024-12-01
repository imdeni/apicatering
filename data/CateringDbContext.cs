using Microsoft.EntityFrameworkCore;
using ApiCatering.Models; // Replace with your actual namespace

namespace ApiCatering.Data
{
    public class CateringDbContext : DbContext
    {
        public CateringDbContext(DbContextOptions<CateringDbContext> options)
            : base(options)
        { }

        public DbSet<FoodItem> FoodItems { get; set; }
    }
}
