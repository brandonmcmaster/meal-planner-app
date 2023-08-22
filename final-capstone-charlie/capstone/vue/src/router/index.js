import Vue from 'vue'
import Router from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Logout from '../views/Logout.vue'
import Register from '../views/Register.vue'
import store from '../store/index'
import AddIngredient from '../views/AddIngredient.vue'
import Recipe from '../views/Recipe.vue'
import UsersRecipes from '../views/CurrentUsersRecipes.vue'
import AddRecipe from '../views/AddRecipe.vue'
import EditRecipes from '../views/EditRecipeView.vue'
import ViewRecipeDetails from '../views/ViewRecipeDetails.vue'
import ViewMealPlans from '../views/ViewMealPlans.vue'
import AddMealPlan from '../views/AddMealPlanView.vue'
import EditMealPlan from '../views/EditMealPlanView.vue'
import MealPlanDetails from '../views/MealPlanDetailsView.vue'
import GroceryListView from '../views/GroceryListView.vue';
import EditRecipeIngredients from '../views/EditRecipeIngredientsView.vue'
// import { component } from 'vue/types/umd'

Vue.use(Router)

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/logout",
      name: "logout",
      component: Logout,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/register",
      name: "register",
      component: Register,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/ingredient",
      name: "ingredient",
      component: AddIngredient,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/recipe", // this references add recipes 
      name: "recipe",
      component: Recipe,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/viewrecipes", // adds path for view all recipes 
      name: "ViewRecipes",
      component: Recipe,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/myrecipes", // path to view current users recipes 
      name: "ViewRecipesByUser",
      component: UsersRecipes,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/addrecipes", // path to view current users recipes 
      name: "addrecipes",
      component: AddRecipe,
      meta: {
        requiresAuth: true
      },
    },
    {
      path: "/editrecipe/:recipeId",
      name: "editRecipe",
      component: EditRecipes,
      meta: {
        requireAuth: true
      }
    },
    {
      path: "/recipedetails/:recipeId", //path to view recipe details
      name: "RecipeDetails",
      component: ViewRecipeDetails,
      meta: {
        requireAuth: false
      }
    },

    {
      path: "/EditRecipeIngredients/:recipeIngredientId", 
      name: "EditRecipeIngredients",
      component: EditRecipeIngredients,
      meta: {
        requireAuth: true
      }
    }, 
    
    {
      path: "/mealplans/",
      name: "viewMealPlans",
      component: ViewMealPlans,
      meta: {
        requireAuth: true
      }
    },
    {
      path: "/addmealplan",
      name: "addMealPlan",
      component: AddMealPlan,
      meta: {
        requireAuth: true
      }
    },
    {
      path: "/editmealplan/:mealPlanId",
      name: "editMealPlan",
      component: EditMealPlan,
      meta: {
        requireAuth: true
      }
    },
    {
      path: "/mealplandetails/:mealPlanId",
      name: "mealPlanDetails",
      component: MealPlanDetails,
      meta: {
        requireAuth: false
      }
    },
    {
      path: "/meal/:mealId",
      name: "editMeal",
      component: MealPlanDetails,
      meta: {
        requireAuth: true
      }
    },
    {
      path: '/grocery-list/:mealPlanId',
      name: "GroceryListView",
      component: GroceryListView,
      props: route => ({ mealPlanId: Number(route.params.mealPlanId) })
    },
    
  ]
})

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '') {
    next("/login");
  } else {
    // Else let them go to their next destination
    next();
  }
});

export default router;
