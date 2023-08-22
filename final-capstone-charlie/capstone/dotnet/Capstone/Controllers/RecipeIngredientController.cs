using Capstone.DAO;
using Capstone.DAO.Interfaces;
using Capstone.Models;
using Capstone.Models.Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecipeIngredientController : ControllerBase
    {
        private IRecipeIngredientDao recipeIngredientDao;
     
        public RecipeIngredientController(IRecipeIngredientDao recipeIngredientDao)
        {
            this.recipeIngredientDao = recipeIngredientDao;
           
        }

        [HttpPost()]
        //creates an array of recipe ingredients and adds it to the database
        public IActionResult CreateRecipeIngredient(RecipeIngredient[] recipeIngredients)
        {
            RecipeIngredient[] newRecipeIngredients = recipeIngredientDao.CreateRecipeIngredient(recipeIngredients);

            if (newRecipeIngredients == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(newRecipeIngredients);
            }
        }

        //get recipe ingredients by recipe_id

        [HttpGet("{recipeid}")]
        //gets all recipe ingredients
        public ActionResult<List<RecipeIngredient>> GetRecipeIngredientsByRecipeId(int recipeId)
        {

            List<RecipeIngredient> recipeIngredients = recipeIngredientDao.GetRecipeIngredientsByRecipeId(recipeId);

            
            if (recipeIngredients == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(recipeIngredients);
            }


        }
        [HttpGet()]
        //gets all recipe ingredients
        public ActionResult<List<RecipeIngredient>> GetRecipeIngredients()
        {

            List<RecipeIngredient> recipeIngredients = recipeIngredientDao.GetRecipeIngredients();


            if (recipeIngredients == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(recipeIngredients);
            }


        }


        [HttpGet("mealplan/{mealPlanId}")]
        public ActionResult<List<GroceryListItem>> GetIngredientsForMealPlan(int mealPlanId)
        {
            List<GroceryListItem> ingredients = recipeIngredientDao.GetIngredientsForMealPlan(mealPlanId);

            if (ingredients == null)
            {
                return BadRequest();
            }
            else
            {

                return Ok(ingredients);
            }
        }

        [Authorize]
        [HttpPut("{recipeIngredientId}")]
        public ActionResult<Recipe> UpdateRecipeIngredients(RecipeIngredient recipeIngredient)
        {
            RecipeIngredient updatedRecipeIngredient = recipeIngredientDao.UpdateRecipeIngredient(recipeIngredient);

            if (updatedRecipeIngredient == null)

            {
                return BadRequest();
            }
            else { 

                return Ok(updatedRecipeIngredient);
            }
        }

    }
}
