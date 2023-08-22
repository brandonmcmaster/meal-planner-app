using Capstone.DAO.Interfaces;
using Capstone.Exceptions;
using Capstone.Models;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using Capstone.Models.Capstone.Models;

namespace Capstone.DAO
{
    public class RecipeIngredientSqlDao : IRecipeIngredientDao 
    {
        private readonly string connectionString;

        public RecipeIngredientSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<RecipeIngredient> GetRecipeIngredients()
        {
            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();

            string sql = @"SELECT recipe_ingredient_id, recipes_ingredients.recipe_id, ingredient_id, amount, unit_of_measurement
                           FROM recipes_ingredients;";

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
                                RecipeIngredient recipeIngredient = new RecipeIngredient();
                                recipeIngredient = MapRowToRecipeIngredients(reader);
                                recipeIngredients.Add(recipeIngredient);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return recipeIngredients;



        }

        //return list of recipe ingredients by recipe_id
        public List<RecipeIngredient> GetRecipeIngredientsByRecipeId(int recipeId)
        {
            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();

            string sql = @"SELECT recipe_ingredient_id, recipes_ingredients.recipe_id, ingredient_id, amount, unit_of_measurement
                           FROM recipes_ingredients 
                           WHERE recipes_ingredients.recipe_id = @recipe_id;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@recipe_id", recipeId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RecipeIngredient recipeIngredient = new RecipeIngredient();
                                recipeIngredient = MapRowToRecipeIngredients(reader);
                                recipeIngredients.Add(recipeIngredient);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return recipeIngredients;



        }

        public RecipeIngredient GetRecipeIngredientById(int recipeIngredientId)
        {
            RecipeIngredient recipeIngredient = null;
            string sql = @"SELECT recipe_ingredient_id, recipe_id, ingredient_id, amount, unit_of_measurement
                           FROM recipes_ingredients 
                           WHERE recipe_ingredient_id = @recipe_ingredient_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@recipe_ingredient_id", recipeIngredientId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                recipeIngredient = MapRowToRecipeIngredients(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return recipeIngredient;
        }
        public RecipeIngredient[] CreateRecipeIngredient(RecipeIngredient[] recipeIngredients)
        {
            RecipeIngredient[] newRecipeIngredient = new RecipeIngredient[recipeIngredients.Length];
            int newRecipeIngredientId = 0;
            string sql = @"INSERT INTO recipes_ingredients (recipe_id, ingredient_id, amount, unit_of_measurement)
                           OUTPUT INSERTED.recipe_ingredient_id
                           VALUES (@recipe_id, @ingredient_id, @amount, @unit_of_measurement)";

            try
            {
                for (int i = 0; i < recipeIngredients.Length; i++)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                                cmd.Parameters.AddWithValue("@recipe_id", recipeIngredients[i].RecipeId);
                                cmd.Parameters.AddWithValue("@ingredient_id", recipeIngredients[i].IngredientId);
                                cmd.Parameters.AddWithValue("@amount", recipeIngredients[i].Amount);
                                cmd.Parameters.AddWithValue("@unit_of_measurement", recipeIngredients[i].UnitOfMeasurement);  

                                newRecipeIngredientId = Convert.ToInt32(cmd.ExecuteScalar()); 
                        
                        }
                    }
                    newRecipeIngredient[i] = GetRecipeIngredientById(newRecipeIngredientId);
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return newRecipeIngredient;
        }

        public List<GroceryListItem> GetIngredientsForMealPlan(int mealPlanId)
        {
            List<GroceryListItem> ingredients = new List<GroceryListItem>();

            string sql = @"SELECT i.item_name, SUM(ri.amount) AS amount, ri.unit_of_measurement
                   FROM recipes_ingredients AS ri
                   JOIN ingredients AS i ON ri.ingredient_id = i.ingredient_id
                   JOIN meals AS m ON ri.recipe_id = m.recipe_id
                   WHERE m.meal_plan_id = @mealPlanId
                   GROUP BY i.item_name, ri.unit_of_measurement";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mealPlanId", mealPlanId);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    GroceryListItem ingredient = new GroceryListItem
                    {
                        ItemName = Convert.ToString(reader["item_name"]),
                        Amount = Convert.ToInt32(reader["amount"]),
                        UnitOfMeasurement = Convert.ToString(reader["unit_of_measurement"])
                    };
                    ingredients.Add(ingredient);
                }
            }

            return ingredients;
        }

        public RecipeIngredient UpdateRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            string sql = @"UPDATE recipes_ingredients SET recipes_ingredients.recipe_id=@recipe_id, recipes_ingredients.ingredient_id=@ingredient_id, 
                                  amount=@amount, unit_of_measurement=@unit_of_measurement 
                           WHERE recipe_ingredient_id=@recipe_ingredient_id;";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@recipe_ingredient_id", recipeIngredient.RecipeIngredientId);
                    cmd.Parameters.AddWithValue("@recipe_id", recipeIngredient.RecipeId);
                    cmd.Parameters.AddWithValue("@ingredient_id", recipeIngredient.IngredientId);
                    cmd.Parameters.AddWithValue("@amount", recipeIngredient.Amount);
                    cmd.Parameters.AddWithValue("@unit_of_measurement", recipeIngredient.UnitOfMeasurement);
                

                    int count = cmd.ExecuteNonQuery();
                    if (count == 1)
                    {
                        return recipeIngredient;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private RecipeIngredient MapRowToRecipeIngredients(SqlDataReader reader)
        {
            RecipeIngredient recipeIngredient = new RecipeIngredient();

            recipeIngredient.RecipeIngredientId = Convert.ToInt32(reader["recipe_ingredient_id"]);
            recipeIngredient.RecipeId = Convert.ToInt32(reader["recipe_id"]);
            recipeIngredient.IngredientId = Convert.ToInt32(reader["ingredient_id"]);
            recipeIngredient.Amount = Convert.ToInt32(reader["amount"]);
            recipeIngredient.UnitOfMeasurement = Convert.ToString(reader["unit_of_measurement"]);

            return recipeIngredient;
        }
    }
}
