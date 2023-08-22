<template>
  <div>
    <form v-on:submit.prevent="editRecipeIngredients">
      <label for="recipe"> Ingredient name: </label>

      <select name="recipe" id="default" v-model="newRecipeIngredient.ingredientId">
        <option
          v-for="ingredient in listIngredients"
          v-bind:value="ingredient.ingredientId"
          v-bind:key="ingredient.ingredientId"
        >
          {{ ingredient.itemName }}
        </option>
      </select>

      <label for="amount"> Amount: </label>

      <input
        v-model.number="newRecipeIngredient.amount"
        type="text"
        name="amount"
      />

      <label for="units"> Unit of Measurement: </label>
      <input
        v-model="newRecipeIngredient.unitOfMeasurement"
        type="text"
        name="units"
      />

      <br />
      <br />

      <button type="submit" class="global-button save-changes-button">
        Save Changes
      </button>
    </form>
  </div>
</template>

<script>
import RecipeIngredientsService from "../services/RecipeIngredientsService.js";

export default {
  name: "editRecipeDetails",
  data() {
    return {
      selectedIngredientId: 0,
      newRecipeIngredient: {},
      recipeIngredientId: 0,
    };
  },

  methods: {
    editRecipeIngredients() {
      if (this.doesRecipeContain.length < 1) {
  
        RecipeIngredientsService.update(this.newRecipeIngredient)

          .then(() => {
            window.alert("changes saved successfully");
            this.$store.commit("LOAD_RECIPE_INGREDIENTS");
            this.$store.commit("LOAD_INGREDIENTS");

            this.$router.push({
              name: "RecipeDetails",    
              params: { recipeId: this.newRecipeIngredient.recipeId },
            });
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
      } else {
        window.alert(
          "ingredient type already exists in the recipe. Cannot have more than one of the same type of ingredient"
        );
      }
    },

    recipeIngredient() {
      //user sets amount and units; computed RecipeIngredient
      return this.$store.state.recipeIngredients.find((recipeIngredient) => {
        return recipeIngredient.recipeIngredientId == this.recipeIngredientId;
      });
    },
  },

  computed: {


    doesRecipeContain() {
      // returns array containing the number of recipe ingredients that have the same recipe Id as the one beign edited
      return this.duplicatesExcludingCurrent.filter((index) => {
        return index.ingredientId == this.newRecipeIngredient.ingredientId;
      });
    },

    duplicatesExcludingCurrent() {
      return this.findDuplicates.filter((ingredient) => {
        return (
          ingredient.recipeIngredientId !=
          this.newRecipeIngredient.recipeIngredientId
        );
      });
    },

    findDuplicates() {
      // array for all recipe ingredients with same ingredient Id as teh current recipe ingredient to be edited

      return this.allrecipeIngredients.filter((ingredient) => {
        return this.newRecipeIngredient.recipeId == ingredient.recipeId;
      });
    },

    listIngredients() {
      return this.$store.state.ingredients;
    },


    allrecipeIngredients() {
      return this.$store.state.recipeIngredients;
    },
  },

  created() {
    this.$store.commit("LOAD_INGREDIENTS");
    this.$store.commit("LOAD_RECIPE_INGREDIENTS");
    this.$store.commit("LOAD_RECIPES");
    this.recipeIngredientId = this.$route.params.recipeIngredientId;
    console.log('recipeINgredientId', this.recipeIngredientId)
    this.newRecipeIngredient = this.recipeIngredient();
    console.log(this.newRecipeIngredient)
  },
};
</script>

<style scoped>
</style>