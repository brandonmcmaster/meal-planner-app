using Capstone.DAO;
using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private IRecipeDao recipeDao;
        private IUserDao userDao;

        public RecipeController(IRecipeDao recipeDao, IUserDao userDao)
        {
            this.recipeDao = recipeDao;
            this.userDao = userDao;
        }

        [Authorize]
        [HttpPost()]
        public IActionResult CreateRecipe(Recipe recipe)
        {
            string username = User.Identity.Name;
            int currentUserId = userDao.GetUserIdByUsername(username);
            Recipe newRecipe = recipeDao.CreateRecipe(recipe, currentUserId);

            if (newRecipe == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(newRecipe);
            }
        }

        [HttpGet()]
        public IActionResult ListRecipes()
        {
            List<Recipe> recipes = recipeDao.ListRecipes();

            if (recipes == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(recipes);
            }
        }

        [Authorize]
        [HttpGet("myrecipes")] 
        public ActionResult<List<Recipe>> GetRecipeByUser()
        {
            string username = User.Identity.Name;
            int currentUserId = userDao.GetUserIdByUsername(username); // the user id
            List<Recipe> userRecipes = recipeDao.GetUserRecipesByUserId(currentUserId); //gets user recipes by Id

            if (!User.Identity.IsAuthenticated || userRecipes == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(userRecipes);
            }
        }

        [Authorize]
        [HttpPut("{recipeId}")]
        public ActionResult<Recipe> UpdateRecipe(int recipeId, Recipe recipe)
        {
            Recipe updatedRecipe = recipeDao.UpdateRecipe(recipe);

            if (updatedRecipe == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(updatedRecipe);
            }
        }
    }
}
