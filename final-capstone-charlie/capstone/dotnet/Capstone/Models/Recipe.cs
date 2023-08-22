using System.Collections.Generic;

namespace Capstone.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public string Instructions { get; set; }
        public string ImageUrl { get; set; }

        public Recipe() {}

        public Recipe(Recipe recipe)
        {
            RecipeName = recipe.RecipeName;
            RecipeDescription = recipe.RecipeDescription;
            Instructions = recipe.Instructions;
            ImageUrl = recipe.ImageUrl;
        }

    }
}