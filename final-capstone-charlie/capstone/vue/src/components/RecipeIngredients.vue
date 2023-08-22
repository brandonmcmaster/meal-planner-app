<template>
  <div class="container">
    <h2>Add Ingredients</h2>

    <form ref="RecipeIngredientForm" v-on:submit.prevent="addRecipeIngredients">
      <button class="global-button" type="submit">Add Ingredient</button>

      <div>
        <label for="recipe"> Ingredient name: </label>

        <select  name="ingredient" v-model="ingredient" required>
          <option
            v-for="ingredient in listIngredients"
            v-bind:value="ingredient"
            v-bind:key="ingredient.ingredientId"
            v-bind:ingredient="ingredient"
          >
            {{ ingredient.itemName }}
          </option>
        </select>
        <br />
        <label for="recipe"> Amount: </label>

        <input
          v-model.number="recipeIngredient.amount"
          type="text"
          name="recipe"
          required
        />
        <br />
        <label for="recipe"> Unit of Measurement: </label>
        <input
          v-model="recipeIngredient.unitOfMeasurement"
          type="text"
          name="recipe"
          required
        />

        <br />
        <br />

        <button v-on:click="submitRecipeIngredients" class="global-button">
          Finish
        </button>
      </div>
    </form>
  </div>
</template>

<script>
import RecipeIngredientsService from "../services/RecipeIngredientsService";

export default {
  components: {},
  props: ["recipeId"],

  data() {
    return {
      recipeIngredient: {},
      recipeIngredients: [],
      ingredient: {},
    };
  },

  computed: {
    listIngredients() {
      return this.$store.state.ingredients;
    },

    checkForExistingIngredient() {
      //checks recipe inngredients array in store to see if ingredient Id exists
      return this.$store.state.pendingRecipeIngredients.includes(
        this.ingredient.ingredientId
      );
    },
  },

  methods: {
    //maps ingredientId and RecipeId to recipeIngredient object. The user sets the unitOfMeasurement and amount in the v-model
    mapRecipeIngredient(ingredientId, recipeId) {
      this.recipeIngredient.recipeId = recipeId;
      this.recipeIngredient.ingredientId = ingredientId;
      this.recipeIngredients.push(this.recipeIngredient);
    },

    submitRecipeIngredients() {
      this.$store.state.pendingRecipeIngredients = [];
      this.$router.push("/recipe");
    },

    resetForm() {
      this.recipeIngredient = {};
    },

    addRecipeIngredients() {
      if (!this.checkForExistingIngredient) {
        // if an ingredient has not been used, then will allow this method to execute
        this.mapRecipeIngredient(this.ingredient.ingredientId, this.recipeId);
        RecipeIngredientsService.addRecipeIngredients(this.recipeIngredients)
          .then(() => {
            this.recipeIngredients = [];
            this.$store.state.pendingRecipeIngredients.push(
              this.ingredient.ingredientId
            );
            window.alert("ingredient added successfully"); //notifies user of successful add
            this.resetForm(); //resets ingredient submission fields
          })

          .catch((error) => {
            if (error.response) {
              // error.response exists
              // Request was made, but response has error status (4xx or 5xx)
              console.log(
                "Error adding recipe ingredient: ",
                error.response.status
              );
            } else if (error.request) {
              // There is no error.response, but error.request exists
              // Request was made, but no response was received
              console.log(
                "Error adding recipe ingredient: unable to communicate to server"
              );
            } else {
              // Neither error.response and error.request exist
              // Request was *not* made
              console.log(
                "Error adding recipe ingredient: error making request"
              );
            }
          });
      } //let user know that ingredient already exists.
      else {
        window.alert(
          "ingredient already exists in the recipe. Edit the recipe to make changes"
        );
      }
    },
  },

  created() {
    this.$store.commit("LOAD_INGREDIENTS");
    this.$store.commit("LOAD_RECIPE_INGREDIENTS");
  },
};
</script>

<style scoped>
.container {
  background-color: rgba(255, 255, 255, 0.9);
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  padding: 20px;
  border-radius: 10px;
}

label {
  display: inline-block;
  width: 200px;
}

select,
input {
  width: 200px;
  padding: 5px;
  margin-bottom: 10px;
}

.global-button {
  padding: 10px;
}
</style>
