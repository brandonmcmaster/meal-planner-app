using Capstone.DAO.Interfaces;
using Capstone.Models;
using System.Data.SqlClient;
using System;
using Capstone.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.DAO
{
    public class MealSqlDao : IMealDao
    {
        private readonly string connectionString;

        public MealSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Meal> ListMeals()
        {
            List<Meal> meals = new List<Meal>();

            string sql = @"SELECT meals.meal_id, meals.meal_plan_id, meals.recipe_id, day_of_the_week, meal_date, meal_type
                           FROM meals;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Meal meal = new Meal();
                                meal = MapRowToMeal(reader);
                                meals.Add(meal);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return meals;
        }

        public Meal GetMealById(int mealId)
        {
            Meal meal = null;
            string sql = @"SELECT meals.meal_id, meals.meal_plan_id, meals.recipe_id, day_of_the_week, meal_date, meal_type
                           FROM meals 
                           WHERE meals.meal_id = @meal_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@meal_id", mealId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                meal = MapRowToMeal(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return meal;
        }

        public Meal CreateMeal(Meal meal)
        {
            Meal createdMeal = null;

            string sql = @"INSERT INTO meals (meals.meal_plan_id, meals.recipe_id, day_of_the_week, meal_date, meal_type)
                           OUTPUT INSERTED.meal_id
                           VALUES (@meal_plan_id, @recipe_id, @day_of_the_week, @meal_date, @meal_type)";

            int newMealId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@meal_plan_id", meal.MealPlanId);
                        cmd.Parameters.AddWithValue("@recipe_id", meal.RecipeId);
                        cmd.Parameters.AddWithValue("@day_of_the_week", meal.DayOfTheWeek);
                        cmd.Parameters.AddWithValue("@meal_date", meal.MealDate);
                        cmd.Parameters.AddWithValue("@meal_type", meal.MealType);

                        newMealId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                createdMeal = GetMealById(newMealId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return createdMeal;
        }

        public List<Meal> CreateMeals(List<Meal> meals, int mealPlanId)
        {
            List<Meal> createdMeals = new List<Meal>();
            List<int> createdMealsIds = new List<int>();

            string sql = @"INSERT INTO meals (meals.meal_plan_id, meals.recipe_id, day_of_the_week, meal_date, meal_type)
                           OUTPUT INSERTED.meal_id
                           VALUES (@meal_plan_id, @recipe_id, @day_of_the_week, @meal_date, @meal_type)";

            bool requestValidationCheck = false;
            for (int i = 0; i < meals.Count; i++)
            {
                if (i + 1 < meals.Count &&
                    meals[i].MealDate == meals[i + 1].MealDate &&
                    meals[i].MealType == meals[i + 1].MealType)
                {
                    requestValidationCheck = true;
                }
            }

            bool serverValidationCheck = ListMeals().Any(meal => meals.Any(passedMeal =>
            {
                return meal.MealDate == passedMeal.MealDate &&
                       meal.MealType == passedMeal.MealType;
            }));

            if (requestValidationCheck || serverValidationCheck)
            {
                throw new DaoException("SQL exception occurred");
            }
            else
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
                            {
                                for (int i = 0; i < meals.Count; i++)
                                {
                                    cmd.Parameters.Clear();

                                    cmd.Parameters.AddWithValue("@meal_plan_id", mealPlanId);
                                    cmd.Parameters.AddWithValue("@recipe_id", meals[i].RecipeId);
                                    cmd.Parameters.AddWithValue("@day_of_the_week", meals[i].DayOfTheWeek);
                                    cmd.Parameters.AddWithValue("@meal_date", meals[i].MealDate);
                                    cmd.Parameters.AddWithValue("@meal_type", meals[i].MealType);

                                    createdMealsIds.Add(Convert.ToInt32(cmd.ExecuteScalar()));

                                }
                                trans.Commit();
                            }
                        }
                        catch (SqlException ex)
                        {
                            throw new DaoException("SQL exception occurred", ex);
                        }
                    }
                }
            }
            createdMealsIds.ForEach(id => createdMeals.Add(GetMealById(id)));

            return createdMeals;
        }

        public Meal UpdateMeal(Meal meal)
        {
            string sql = @"UPDATE meals SET meals.meal_plan_id=@meal_plan_id, meals.recipe_id=@recipe_id, 
                                  day_of_the_week=@day_of_the_week, meal_date=@meal_date, meal_type=@meal_type
                           WHERE meals.meal_id = @meal_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@meal_id", meal.MealId);
                    cmd.Parameters.AddWithValue("@meal_plan_id", meal.MealPlanId);
                    cmd.Parameters.AddWithValue("@recipe_id", meal.RecipeId);
                    cmd.Parameters.AddWithValue("@day_of_the_week", meal.DayOfTheWeek);
                    cmd.Parameters.AddWithValue("@meal_date", meal.MealDate);
                    cmd.Parameters.AddWithValue("@meal_type", meal.MealType);

                    int count = cmd.ExecuteNonQuery();
                    if (count == 1)
                    {
                        return meal;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }



        private Meal MapRowToMeal(SqlDataReader reader)
        {
            Meal meal = new Meal
            {
                MealId = Convert.ToInt32(reader["meal_id"]),
                MealPlanId = Convert.ToInt32(reader["meal_plan_id"]),
                RecipeId = Convert.ToInt32(reader["recipe_id"]),
                DayOfTheWeek = Convert.ToString(reader["day_of_the_week"]),
                MealDate = Convert.ToDateTime(reader["meal_date"]),
                MealType = Convert.ToString(reader["meal_type"])
            };

            return meal;
        }
    }
}
