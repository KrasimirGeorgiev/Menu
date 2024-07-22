using Menu.Models;
using Microsoft.EntityFrameworkCore;

namespace Menu.Data
{
    public class MenuContext : DbContext
    {
        public MenuContext(DbContextOptions<MenuContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Connection many to many
            //_____
            modelBuilder.Entity<DishIngredient>().HasKey(di => new
            {
                di.DishId,
                di.IngredientId
            });

            modelBuilder.Entity<DishIngredient>().HasOne(d => d.Dish).WithMany(di => di.DishIngredients).HasForeignKey(d => d.DishId);
            modelBuilder.Entity<DishIngredient>().HasOne(d => d.Ingredient).WithMany(di => di.DishIngredients).HasForeignKey(d => d.IngredientId);
            //_____

            // Add some data to the database
            //_____
            modelBuilder.Entity<Dish>().HasData
            (
                new Dish { Id = 1, Name = "Margaritta", Price = 7.50m, ImgUrl = "https://www.thekitchn.com/easy-recipe-classic-margherita-pizza-recipes-from-the-kitchn-174103" }
            );

            modelBuilder.Entity<Ingredient>().HasData
            (
                new Ingredient { Id = 1, Name = "Tomato Sauce"},
                new Ingredient { Id = 2, Name = "Mozzarella" }
            );

            modelBuilder.Entity<DishIngredient>().HasData
            (
                new DishIngredient { DishId = 1, IngredientId = 1 },
                new DishIngredient { DishId = 1, IngredientId = 2 }
            );
            //_____

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients {  get; set; }
        public DbSet <DishIngredient> DishIngredients { get; set; }
    }
}
