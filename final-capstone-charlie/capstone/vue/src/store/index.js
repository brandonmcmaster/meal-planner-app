import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

// import { push } from 'core-js/core/array'
import recipeService from '../services/RecipeService.js'
import ingredientService from '../services/IngredientService.js'
import RecipeIngredientsService from '../services/RecipeIngredientsService.js'
import mealPlanService from '../services/MealPlanService.js'
import mealService from '../services/MealService.js'
Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token')
const currentUser = JSON.parse(localStorage.getItem('user'));

if(currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
    
    recipes: [],
    ingredients: [],
    recipeIngredients: [],
    ingredientIds : [],
    pendingRecipeIngredients: [], // recipe ingred before being added to recipe
 
    mealPlans: [],
    meals: [],
    pendingMeals: [],
     
    
  },
  mutations: {
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user',JSON.stringify(user));
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    },
    ADD_RECIPE(state, payload) {
        state.recipes.push(payload);
    },
    

    LOAD_RECIPE_INGREDIENTS(state) {
      RecipeIngredientsService.listRecipeIngredients()
      .then((response) => {
        state.recipeIngredients = response.data
      
      })
      .catch((error) => {
        if (error.response) {
          // error.response exists
          // Request was made, but response has error status (4xx or 5xx)
          console.log("Error viewing all recipe ingredients: ", error.response.status);
        } else if (error.request) {
          // There is no error.response, but error.request exists
          // Request was made, but no response was received
          console.log(
            "Error viewing all recipe ingredients: unable to communicate to server"
          );
        } else {
          // Neither error.response and error.request exist
          // Request was *not* made
          console.log("Error viewing all recipe ingredients: error making request");
        }
      });
    },
    LOAD_INGREDIENTS(state)
    {
      ingredientService.listIngredients()
      .then((response) => {
       state.ingredients= response.data
      })
      .catch((error) => {
        if (error.response) {
          // error.response exists
          // Request was made, but response has error status (4xx or 5xx)
          console.log("Error viewing all ingredients: ", error.response.status);
        } else if (error.request) {
          // There is no error.response, but error.request exists
          // Request was made, but no response was received
          console.log(
            "Error viewing all ingredients: unable to communicate to server"
          );
        } else {
          // Neither error.response and error.request exist
          // Request was *not* made
          console.log("Error viewing all ingredients: error making request");
        }
      });
    },
    LOAD_RECIPES(state) {
      recipeService.listRecipes()
      .then((response) => {
       state.recipes = response.data
      
      })
      .catch((error) => {
        if (error.response) {
          // error.response exists
          // Request was made, but response has error status (4xx or 5xx)
          console.log("Error viewing all recipes: ", error.response.status);
        } else if (error.request) {
          // There is no error.response, but error.request exists
          // Request was made, but no response was received
          console.log(
            "Error viewing all recipes: unable to communicate to server"
          );
        } else {
          // Neither error.response and error.request exist
          // Request was *not* made
          console.log("Error viewing all recipes: error making request");
        }
      });
    },
    LOAD_MEAL_PLANS(state) {
      mealPlanService.list()
      .then((response) => {
        state.mealPlans = response.data;
      })
      .catch((error) => {
        if (error.response) {
          // error.response exists
          // Request was made, but response has error status (4xx or 5xx)
          console.log("Error viewing all meal plans: ", error.response.status);
        } else if (error.request) {
          // There is no error.response, but error.request exists
          // Request was made, but no response was received
          console.log(
            "Error viewing all meal plans: unable to communicate to server"
          );
        } else {
          // Neither error.response and error.request exist
          // Request was *not* made
          console.log("Error viewing all meal plans: error making request");
        }
      });
    },
    LOAD_MEALS(state) {
      mealService.list()
      .then((response) => {
        state.meals = response.data;
      })
      .catch((error) => {
        if (error.response) {
          // error.response exists
          // Request was made, but response has error status (4xx or 5xx)
          console.log("Error viewing all meals: ", error.response.status);
        } else if (error.request) {
          // There is no error.response, but error.request exists
          // Request was made, but no response was received
          console.log(
            "Error viewing all meals: unable to communicate to server"
          );
        } else {
          // Neither error.response and error.request exist
          // Request was *not* made
          console.log("Error viewing all meals: error making request");
        }
      });
    }
  }
})
