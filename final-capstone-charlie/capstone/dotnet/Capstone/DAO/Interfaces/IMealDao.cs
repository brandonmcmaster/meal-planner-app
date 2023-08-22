using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IMealDao
    {
        List<Meal> ListMeals();
        Meal CreateMeal(Meal meal);
        Meal UpdateMeal(Meal meal);
        List<Meal> CreateMeals(List<Meal> meals, int mealPlanId);
    }
}
