using System;
using System.Collections.Generic;

namespace Capstone.Models
{
    public class MealPlan
    {
        public int MealPlanId { get; set; }
        public int UserId { get; set; }
        public string MealPlanName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public MealPlan() { }


        public MealPlan(MealPlan mealPlan)
        {
            UserId = mealPlan.UserId;
            MealPlanName = mealPlan.MealPlanName;
            StartDate = mealPlan.StartDate;
            EndDate = mealPlan.EndDate;
        }
    }

    public class MealPlanMeals
    {
        public MealPlan MealPlan { get; set; }
        public List<Meal> Meals { get;  set; }
        public int MealPlanId { get; set; }

        public MealPlanMeals() { }


        public MealPlanMeals(MealPlanMeals mealPlanMeals)
        {
            MealPlan = mealPlanMeals.MealPlan;
            Meals = mealPlanMeals.Meals;
            MealPlanId = mealPlanMeals.MealPlanId;
        }
    }
}
