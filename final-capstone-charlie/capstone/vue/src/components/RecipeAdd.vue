<template>
  <div class="recipe-form-container">
    <section>
      <h2 v-show="!showIngredientForm">Add A New Recipe</h2>
     
      <form v-on:submit.prevent="addRecipe" v-show="showForm">
        <div>
          <label for="recipe">Recipe Name: </label>
          <input v-model="recipe.recipeName" type="text" name="recipe" class="short-input" required />
        </div>
        <div>
          <label for="recipe">Descriptions: </label>
          <textarea
            v-model="recipe.recipeDescription"
            type="textarea"
            name="recipe"
            class="long-input"
          required />
        </div>
        <div>
          <label for="recipe">Instructions: </label>
          <textarea v-model="recipe.instructions" type="textarea" name="recipe" class="long-input" required />
        </div>
        <div>
          <label for="recipe">Image URL: </label>
          <input v-model="recipe.imageUrl" type="text" name="recipe" class="short-input" required />
        </div>
        <br>
        <button type="submit" class="global-button add-recipe-button">Add Recipe</button>
      </form>
      <recipe-ingredients-vue v-show="showIngredientForm" v-bind:recipeId="recipe.recipeId"/>
    </section>
  </div>
</template>

<script>
  import RecipeService from "../services/RecipeService.js";
  import RecipeIngredientsVue from "../components/RecipeIngredients.vue";

  export default {
    name: "recipe-add",
    components: { RecipeIngredientsVue },
    data() {
      return { 
        showForm: true, 
        recipe: {}, 
        showIngredientForm: false, 
      }
    },
    methods: {
      addRecipe() {
        RecipeService.add(this.recipe)
          .then(() => {
            if (this.recipe.recipeName) {
              this.recipe.recipeId = this.nextRecipeId();
              this.toggleForm();
            }
          })
          .catch((error) => {
            if (error.response) {
              // error.response exists
              // Request was made, but response has error status (4xx or 5xx)
              console.log("Error adding recipe: ", error.response.status);
            } else if (error.request) {
              // There is no error.response, but error.request exists
              // Request was made, but no response was received
              console.log(
                "Error adding recipe: unable to communicate to server"
              );
            } else {
              // Neither error.response and error.request exist
              // Request was *not* made
              console.log("Error adding recipe: error making request");
            }
          });
      },
      nextRecipeId() {
        let result = 0;
        this.$store.state.recipes.forEach((item) => {
          if (item.recipeId > result) {
            result = item.recipeId;
          }
        });
        return result + 1;
      },
      toggleForm() {
        if (this.showForm) {
          this.showForm = false;
          this.showIngredientForm = true;
        } else {
          this.showForm = true;
          this.showIngredientForm = false;
        }
      },
    },
  }
</script>

<style scoped>
.recipe-form-container {
   display: flex;
   flex-direction: column;
   justify-content: center; 
  max-width: 600px;
  align-items: center; 
  background-color: #f9f9f9; /* Background color for the form container */
}

.short-input {
  width: 100%;
  padding: 8px;
  border: 1px solid #ccc;
}

.long-input {
  width: 100%;
  padding: 20px;
  border: 1px solid #ccc;
  height: 100px; 
  resize: vertical; 
}

.add-recipe-button{
  padding: 10px;
  margin:10px;
}
</style>
