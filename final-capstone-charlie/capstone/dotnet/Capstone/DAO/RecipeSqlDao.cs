using Capstone.Exceptions;
using Capstone.Models;
using System.Data.SqlClient;
using System;
using Capstone.DAO.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.DAO
{
    public class RecipeSqlDao : IRecipeDao
    {
        private readonly string connectionString;

        public RecipeSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Recipe>  ListRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();

            string sql = @"SELECT recipe_id, recipes.user_id, recipe_name, recipe_description, instructions, image_url 
                           FROM recipes;";

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
                                Recipe recipe = new Recipe();
                                recipe = MapRowTorecipe(reader);
                                recipes.Add(recipe);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return recipes;
        }

        public Recipe GetRecipeById(int recipeId)
        {
            Recipe recipe = null;
            string sql = @"SELECT recipe_id, recipes.user_id, recipe_name, recipe_description, instructions, image_url
                           FROM recipes 
                           WHERE recipe_id = @recipe_id";

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
                            if (reader.Read())
                            {
                                recipe = MapRowTorecipe(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return recipe;
        }

        public List<Recipe> GetUserRecipesByUserId(int currentUserId)
        {
            List<Recipe> recipes = new List<Recipe>();

            string sql = @"SELECT recipe_id, recipes.user_id, recipe_name, recipe_description, instructions, image_url
                           FROM recipes 
                           WHERE recipes.user_id = @userId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", currentUserId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Recipe recipe = new Recipe();
                                recipe = MapRowTorecipe(reader);
                                recipes.Add(recipe);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return recipes;
        }

        public Recipe CreateRecipe(Recipe recipe, int userId)
        {
            Recipe newRecipe = null;
            string sql = @"INSERT INTO recipes (recipes.user_id, recipe_name, recipe_description, instructions, image_url)
                           OUTPUT INSERTED.recipe_id
                           VALUES (@user_id, @recipe_name, @recipe_description, @instructions, @image_url)";

            int newRecipeId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {           
                        cmd.Parameters.AddWithValue("@user_id", userId);
                        cmd.Parameters.AddWithValue("@recipe_name", recipe.RecipeName);
                        cmd.Parameters.AddWithValue("@recipe_description", recipe.RecipeDescription);
                        cmd.Parameters.AddWithValue("@instructions", recipe.Instructions);
                        cmd.Parameters.AddWithValue("@image_url", recipe.ImageUrl);

                        newRecipeId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                newRecipe = GetRecipeById(newRecipeId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return newRecipe;
        }

        public Recipe UpdateRecipe(Recipe recipe)
        {
            string sql = @"UPDATE recipes SET recipes.user_id=@user_id, recipe_name=@recipe_name, 
                                  recipe_description=@recipe_description, instructions=@instructions, image_url=@image_url 
                           WHERE recipe_id = @recipe_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@recipe_id", recipe.RecipeId);
                    cmd.Parameters.AddWithValue("@user_id", recipe.UserId);
                    cmd.Parameters.AddWithValue("@recipe_name", recipe.RecipeName);
                    cmd.Parameters.AddWithValue("@recipe_description", recipe.RecipeDescription);
                    cmd.Parameters.AddWithValue("@instructions", recipe.Instructions);
                    cmd.Parameters.AddWithValue("@image_url", recipe.ImageUrl);

                    int count = cmd.ExecuteNonQuery();
                    if (count == 1)
                    {
                        return recipe;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private Recipe MapRowTorecipe(SqlDataReader reader)
        { 
            Recipe recipe = new Recipe();
            recipe.RecipeId = Convert.ToInt32(reader["recipe_id"]);
            recipe.UserId = Convert.ToInt32(reader["user_id"]);
            recipe.RecipeName = Convert.ToString(reader["recipe_name"]);
            recipe.RecipeDescription = Convert.ToString(reader["recipe_description"]);
            recipe.Instructions = Convert.ToString(reader["instructions"]);
            recipe.ImageUrl = Convert.ToString(reader["image_url"]);

            return recipe;
        }
    }
}
