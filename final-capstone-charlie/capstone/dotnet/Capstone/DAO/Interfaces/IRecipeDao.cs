using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IRecipeDao
    {
        Recipe GetRecipeById(int recipeId);
        List<Recipe> ListRecipes();
        List<Recipe> GetUserRecipesByUserId(int currentUserId);
        Recipe CreateRecipe(Recipe recipe, int userId);
        Recipe UpdateRecipe(Recipe recipe);
    }
}
