using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using Capstone.DAO.Interfaces;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private IIngredientDao ingredientDao;

        public IngredientController(IIngredientDao ingredientDao)
        {
            this.ingredientDao = ingredientDao;
        }

        [HttpPost()]
        public IActionResult CreateIngredient(Ingredient ingredient)
        {
            Ingredient newIngredient = ingredientDao.CreateIngredient(ingredient);

            if (newIngredient == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(newIngredient);
            }
        }


        [HttpGet()]
        public ActionResult<List<Ingredient>> ListIngredients()
        {
            List<Ingredient> ingredients = ingredientDao.ListIngredients();

            if (ingredients == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(ingredients);
            }
        }
    }
}
