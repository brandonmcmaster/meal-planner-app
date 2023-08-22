using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security.Models;
using System.Data.SqlClient;
using System;
using Capstone.DAO.Interfaces;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public class IngredientSqlDao : IIngredientDao
    {
        private readonly string connectionString;

        public IngredientSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Ingredient> ListIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            string sql = @"SELECT ingredients.ingredient_id, ingredients.item_name 
                           FROM ingredients;";

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
                                Ingredient ingredient = new Ingredient();
                                ingredient = MapRowToingredient(reader);
                                ingredients.Add(ingredient);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return ingredients;

        }
        public Ingredient GetIngredientById(int ingredientId)
        {
            Ingredient ingredient = null;
            string sql = @"SELECT ingredient_id, item_name 
                           FROM ingredients 
                           WHERE ingredient_id = @ingredient_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ingredient_id", ingredientId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ingredient = MapRowToingredient(reader);
                            }
                        }
                    }            
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return ingredient;
        }

        public Ingredient CreateIngredient(Ingredient ingredient)
        {
            Ingredient newIngredient = null;
            int newIngredientId = 0;
            string sql = @"INSERT INTO ingredients (item_name)
                           OUTPUT INSERTED.ingredient_id
                           VALUES (@item_name)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@item_name", ingredient.ItemName);

                        newIngredientId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                newIngredient = GetIngredientById(newIngredientId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return newIngredient;
        }

  

        private Ingredient MapRowToingredient(SqlDataReader reader)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.IngredientId = Convert.ToInt32(reader["ingredient_id"]);
            ingredient.ItemName = Convert.ToString(reader["item_name"]);

            return ingredient;
        }

    }
}
