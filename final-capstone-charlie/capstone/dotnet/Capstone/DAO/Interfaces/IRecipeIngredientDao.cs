using Capstone.Models;
using Capstone.Models.Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IRecipeIngredientDao
    {
        RecipeIngredient[] CreateRecipeIngredient(RecipeIngredient[] recipeIngredient);
        RecipeIngredient GetRecipeIngredientById(int recipeIngredientId);
        List<RecipeIngredient> GetRecipeIngredientsByRecipeId(int recipeId);

        List<RecipeIngredient> GetRecipeIngredients();

        RecipeIngredient UpdateRecipeIngredient(RecipeIngredient recipeIngredient);
        List<GroceryListItem> GetIngredientsForMealPlan(int mealPlanId);

    }
}
