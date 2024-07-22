namespace Menu.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<DishIngredient> DishIngredients { get; set; }
    }
}
