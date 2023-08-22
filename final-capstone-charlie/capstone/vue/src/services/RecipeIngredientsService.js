import axios from 'axios';

export default {
    addRecipeIngredients(recipeIngredients) {
        return axios.post("/recipeIngredient", recipeIngredients)
    },
    listRecipeIngredients() {
        return axios.get("/recipeIngredient")
    },
    listRecipeIngredientsByRecipeId(recipe) {
        return axios.get(`/recipeIngredient/${recipe.recipeId}`, recipe)
    },
    fetchGroceryList(mealplanId) {
        return axios.get(`/recipeIngredient/mealPlan/${mealplanId}`);
    },

    update(recipeIngredient) {
        return axios.put(`/recipeIngredient/${recipeIngredient.recipeIngredientId}`, recipeIngredient);
    },
};

