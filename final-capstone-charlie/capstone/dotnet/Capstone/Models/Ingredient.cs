namespace Capstone.Models
{
    public class Ingredient
    {

        public int IngredientId { get; set; }
        public string ItemName { get; set; }

        //created empty ingredient, not user defined.
        public Ingredient() {}

        //created ingredient with defined parameters
        public Ingredient(Ingredient ingredient)
        {
            ItemName = ingredient.ItemName;  
        }
    }

    
}
