using SmallBusinessSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SmallBusinessSystem.Data
{
    public class CandyDbContext : IdentityDbContext
    {
        public CandyDbContext(DbContextOptions<CandyDbContext> options) : base(options) 
        { 

        }

        public DbSet<Candy> Candies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Candy>().HasData(
                new Candy
                {
                    CandyId =1,
                    CandyName = "Gummy Worms", 
                    Description = "Sweet chewy worm candy",
                    CandyPrice = 5.99m, 
                    ImgUrl = "",
                    CandyQty = 100
                },
                new Candy
                {
                    CandyId = 2,
                    CandyName = "Homestyle Milk Chocolate Bar",
                    Description = "Store exclusive homestyle chocolate bar.",
                    CandyPrice = 2.99m,
                    ImgUrl = "",
                    CandyQty = 140
                },
                new Candy
                {
                    CandyId = 3,
                    CandyName = "Sour Gummy Worms",
                    Description = "Sweet chewy worm candy with sour sugar coating on top",
                    CandyPrice = 6.99m,
                    ImgUrl = "",
                    CandyQty = 130
                },
                new Candy
                {
                    CandyId = 4,
                    CandyName = "Homestyle Peanut Butter Cups",
                    Description = "Peanut butter center with chocolate cover on the outside.",
                    CandyPrice = 6.99m,
                    ImgUrl = "",
                    CandyQty = 100
                },
                new Candy
                {
                    CandyId = 5,
                    CandyName = "Chocolate Covered Almonds",
                    Description = "Salted almonds covered in our hometyle milk chocolate.",
                    CandyPrice = 3.99m,
                    ImgUrl = "", 
                    CandyQty = 200
                }
                );
        }
    }
}
