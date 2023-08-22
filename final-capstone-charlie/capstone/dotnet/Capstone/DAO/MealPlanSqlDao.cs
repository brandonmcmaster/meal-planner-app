using Capstone.DAO.Interfaces;
using Capstone.Models;
using System.Data.SqlClient;
using System;
using Capstone.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.DAO
{
    public class MealPlanSqlDao : IMealPlanDao
    {
        private readonly string connectionString;
        private readonly IMealDao mealDao;

        public MealPlanSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
            mealDao = new MealSqlDao(connectionString);
        }

        public List<MealPlan> ListMealPlans()
        {
            List<MealPlan> mealPlans = new List<MealPlan>();

            string sql = @"SELECT meal_plans.meal_plan_id, meal_plans.user_id, meal_plan_name, start_date, end_date
                           FROM meal_plans;";

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
                                MealPlan mealPlan = new MealPlan();
                                mealPlan = MapRowToMealPlan(reader);
                                mealPlans.Add(mealPlan);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return mealPlans;
        }

        public MealPlan GetMealPlanById(int mealPlanId)
        {
            MealPlan mealPlan = null;
            string sql = @"SELECT meal_plans.meal_plan_id, meal_plans.user_id, meal_plan_name, start_date, end_date
                           FROM meal_plans 
                           WHERE meal_plans.meal_plan_id = @meal_plan_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@meal_plan_id", mealPlanId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                mealPlan = MapRowToMealPlan(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return mealPlan;
        }

        public MealPlan CreateMealPlan(MealPlan mealPlan, List<Meal> meals, int userId, int mealPlanId)
        {
            MealPlan createdMealPlan = null;

            string sql = @"INSERT INTO meal_plans (meal_plans.user_id, meal_plan_name, start_date, end_date)
                           OUTPUT INSERTED.meal_plan_id
                           VALUES (@user_id, @meal_plan_name, @start_date, @end_date)";

            int newMealPlanId = 0;

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

            bool serverValidationCheck = mealDao.ListMeals().Any(meal => meals.Any(passedMeal =>
            {
                return meal.MealDate == passedMeal.MealDate &&
                       meal.MealType == passedMeal.MealType &&
                       meal.MealPlanId != passedMeal.MealPlanId;
            }));

            if (requestValidationCheck || serverValidationCheck)
            {
                throw new DaoException("SQL exception occurred");
            }
            else {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@user_id", userId);
                            cmd.Parameters.AddWithValue("@meal_plan_name", mealPlan.MealPlanName);
                            cmd.Parameters.AddWithValue("@start_date", mealPlan.StartDate);
                            cmd.Parameters.AddWithValue("@end_date", mealPlan.EndDate);

                            newMealPlanId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                    createdMealPlan = GetMealPlanById(newMealPlanId);
                }
                catch (SqlException ex)
                {
                    throw new DaoException("SQL exception occurred", ex);
                }
            }
            List<Meal> mealPlanMeals = mealDao.CreateMeals(meals, mealPlanId);

            return createdMealPlan;
        }

        public MealPlan UpdateMealPlan(MealPlan mealPlan)
        {
            string sql = @"UPDATE meal_plans SET meal_plans.user_id=@user_id, meal_plan_name=@meal_plan_name, 
                                  start_date=@start_date, end_date=@end_date
                           WHERE meal_plan_id = @meal_plan_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@meal_plan_id", mealPlan.MealPlanId);
                    cmd.Parameters.AddWithValue("@user_id", mealPlan.UserId);
                    cmd.Parameters.AddWithValue("@meal_plan_name", mealPlan.MealPlanName);
                    cmd.Parameters.AddWithValue("@start_date", mealPlan.StartDate);
                    cmd.Parameters.AddWithValue("@end_date", mealPlan.EndDate);

                    int count = cmd.ExecuteNonQuery();
                    if (count == 1)
                    {
                        return mealPlan;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private MealPlan MapRowToMealPlan(SqlDataReader reader)
        {
            MealPlan mealPlan = new MealPlan
            {
                MealPlanId = Convert.ToInt32(reader["meal_plan_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                MealPlanName = Convert.ToString(reader["meal_plan_name"]),
                StartDate = Convert.ToDateTime(reader["start_date"]),
                EndDate = Convert.ToDateTime(reader["end_date"]),
            };

            return mealPlan;
        }
    }
}
