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
    public class MealPlanController : ControllerBase
    {
        private IMealPlanDao mealPlanDao;
        private IUserDao userDao;

        public MealPlanController(IMealPlanDao mealPlanDao, IUserDao userDao)
        {
            this.mealPlanDao = mealPlanDao;
            this.userDao = userDao;
        }

        [Authorize]
        [HttpPost()]
        public IActionResult CreateMealPlan(MealPlanMeals mealPlanMeals)
        {
            string username = User.Identity.Name;
            int currentUserId = userDao.GetUserIdByUsername(username);
            MealPlan createdMealPlan = mealPlanDao.CreateMealPlan(mealPlanMeals.MealPlan, mealPlanMeals.Meals, currentUserId, mealPlanMeals.MealPlanId);

            if (createdMealPlan == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(createdMealPlan);
            }
        }

        [HttpGet]
        public ActionResult<List<MealPlan>> ListMealPlans()
        {
            List<MealPlan> mealPlans = mealPlanDao.ListMealPlans();

            if (mealPlans == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mealPlans);
            }
        }

        [Authorize]
        [HttpPut("{mealPlanId}")]
        public ActionResult<MealPlan> UpdateMealPlan(int mealPlanId, MealPlan mealPlan)
        {
            mealPlan.MealPlanId = mealPlanId;
            MealPlan updatedMealPlan = mealPlanDao.UpdateMealPlan(mealPlan);

            if (updatedMealPlan == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(updatedMealPlan);
            }
        }
    }
}
