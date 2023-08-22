using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IMealPlanDao
    {
        List<MealPlan> ListMealPlans();
        MealPlan CreateMealPlan(MealPlan mealPlan, List<Meal> meals, int userId, int mealPlanId);
        MealPlan UpdateMealPlan(MealPlan mealPlan);
    }
}
