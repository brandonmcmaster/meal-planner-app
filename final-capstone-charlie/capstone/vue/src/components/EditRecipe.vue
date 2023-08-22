<template>
  <div>
    <section class="edit-recipe-and-ingredients-container">
      <form v-on:submit.prevent="editRecipe">
        <h2>Edit Recipe</h2>

        <label>
          <p>Name:</p>
          <input
            type="text"
            name="name"
            id="name"
            v-model="recipe.recipeName"
            required
          />
        </label>
        <label>
          <p>Description:</p>
          <textarea
            v-model="recipe.recipeDescription"
            type="textarea"
            name="recipe"
            required
          />
        </label>
        <label>
          <p>Instructions:</p>
          <textarea
            v-model="recipe.instructions"
            type="textarea"
            name="recipe"
            required
          />
        </label>
        <label>
          <p>Image URL:</p>
          <input
            type="text"
            name="imageUrl"
            id="imageUrl"
            v-model="recipe.imageUrl"
            required
          />
        </label>

        <button type="submit" class="global-button save-changes-button">Save Changes</button>
      </form>

      <list-Recipe-Ingredients-Vue v-bind:recipeId="recipeId" />
    </section>
  </div>
</template>
  
<script>
import recipeService from "../services/RecipeService.js";

import ListRecipeIngredientsVue from "./ListRecipeIngredients.vue";

export default {
  name: "edit-recipe",
  components: { ListRecipeIngredientsVue },
  data() {
    return {
      recipeId: this.$route.params.recipeId,
    };
  },
  methods: {
    editRecipe() {
      recipeService
        .update(this.recipe)
        .then(() => {
          this.$store.commit("LOAD_RECIPES");
          // this.$router.push({ name: "ViewRecipesByUser" });
        })
        .catch((error) => {
          if (error.response) {
            // error.response exists
            // Request was made, but response has error status (4xx or 5xx)
            console.log("Error updating recipe: ", error.response.status);
          } else if (error.request) {
            // There is no error.response, but error.request exists
            // Request was made, but no response was received
            console.log(
              "Error updating recipe: unable to communicate to server"
            );
          } else {
            // Neither error.response and error.request exist
            // Request was *not* made
            console.log("Error updating recipe: error making request");
          }
        });
    },
  },

  computed: {
    recipe() {
      return this.$store.state.recipes.find((recipe) => {
        return recipe.recipeId == this.recipeId;
      });
    },
  },

  created() {
    this.$store.commit("LOAD_RECIPE_INGREDIENTS");
    this.$store.commit("LOAD_INGREDIENTS");
  },
};
</script>
  
<style scoped>
label {
  display: flex;
  flex-direction: row;
  justify-content: left;
  width: 500px;
  line-height: 26px;
  margin: 10px 0;
}

input {
  box-sizing: border-box;
  padding: 0 0.5vh;
  flex: 0 0 200px;
  font-family: Arial;
  font-size: 13.3px;
}

p {
  flex: 0 0 100px;
  text-align: left;
}

#imageUrl {
  flex: 0 0 400px;
}

textarea {
  box-sizing: border-box;
  padding: 0.5vh;
  flex: 0 0 400px;
  resize: none;
  height: 150px;
  font-family: Arial;
  font-size: 13.3px;
}
.edit-recipe-and-ingredients-container {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 20px;
  padding: 20px;
  height: 100vh;
}

.save-changes-button{
    padding: 10px;
}

</style>