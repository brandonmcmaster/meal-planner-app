namespace Capstone.Models
{
    public class RecipeIngredient
    {
            public int RecipeIngredientId { get; set; }
            public int RecipeId { get; set; }
            public int IngredientId { get; set; }
            public int Amount { get; set; }
            public string UnitOfMeasurement { get; set; }

            public RecipeIngredient() {}

            public RecipeIngredient(RecipeIngredient recipeIngredient)
            {
                RecipeIngredientId = recipeIngredient.RecipeIngredientId;
                RecipeId = recipeIngredient.RecipeId;
                IngredientId = recipeIngredient.IngredientId;
                Amount = recipeIngredient.Amount;
                UnitOfMeasurement = recipeIngredient.UnitOfMeasurement;

            }

       
    }
}
