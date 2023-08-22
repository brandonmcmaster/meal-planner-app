using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IIngredientDao
    {
        Ingredient GetIngredientById(int ingredientId);
        Ingredient CreateIngredient(Ingredient ingredient);
        List<Ingredient> ListIngredients();
    }
}
