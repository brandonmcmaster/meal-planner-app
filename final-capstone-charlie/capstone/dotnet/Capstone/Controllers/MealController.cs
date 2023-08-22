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
    public class MealController : ControllerBase
    {
        private IMealDao mealDao;

        public MealController(IMealDao mealDao)
        {
            this.mealDao = mealDao;
        }

        [Authorize]
        [HttpPost()]
        public IActionResult CreateMeal(Meal meal)
        {
            Meal createdMeal = mealDao.CreateMeal(meal);

            if (createdMeal == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(createdMeal);
            }
        }

        [HttpGet]
        public ActionResult<List<Meal>> ListMeals()
        {
            List<Meal> meals = mealDao.ListMeals();

            if (meals == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(meals);
            }
        }

        [Authorize]
        [HttpPut("{mealId}")]
        public ActionResult<Meal> UpdateRecipe(int mealId, Meal meal)
        {
            meal.MealId = mealId;
            Meal updatedMeal = mealDao.UpdateMeal(meal);

            if (updatedMeal == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(updatedMeal);
            }
        }
    }
}
