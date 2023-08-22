using System;

namespace Capstone.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public int MealPlanId { get; set; }
        public int RecipeId { get; set; }
        public string DayOfTheWeek { get; set; }
        public DateTime MealDate { get; set; }
        public string MealType { get; set; }

        public Meal() { }


        public Meal(Meal meal)
        {
            MealPlanId = meal.MealPlanId;
            RecipeId = meal.RecipeId;
            DayOfTheWeek = meal.DayOfTheWeek;
            MealDate = meal.MealDate;
            MealType = meal.MealType;
        }
    }
}
