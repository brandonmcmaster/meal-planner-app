import axios from 'axios';

export default {
  add(recipe) {
    return axios.post('/recipe', recipe)
  },
  listRecipes() {
    return axios.get('/recipe')  
  },
  listCurrentUsersRecipes() {
    return axios.get('/recipe/myrecipes'); 
  },
  update(recipe) {
    return axios.put(`/recipe/${recipe.recipeId}`, recipe);
  }
}