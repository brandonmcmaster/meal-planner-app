<template>
  <section>
    <h2>Add Ingredient</h2>
    <form v-on:submit.prevent="addIngredient">
      <br>
      <div>
        <label for="ingredient">Ingredient Name: </label>
        <input v-model="ingredient.ItemName" type="text" name="ingredient" required />
      </div>   
      <button type="submit" class="global-button ingredient-button"> Add Ingredient </button>
    </form>
  </section>
</template>

<script>
  import IngredientService from '../services/IngredientService.js';

  export default {
    name: 'ingredient',
    data() {
      return {
        ingredient: {},
      }
    },
    methods: {
      

      addIngredient() {
        IngredientService.add(this.ingredient)
        .then(() =>{ 
          window.alert("ingredient added successfully");
          this.$router.push({name: 'home'})
        })
        .catch((error) => {
          if (error.response) {
            // error.response exists
            // Request was made, but response has error status (4xx or 5xx)
            console.log("Error adding ingredient: ", error.response.status);
          } else if (error.request) {
            // There is no error.response, but error.request exists
            // Request was made, but no response was received
            console.log("Error adding ingredient: unable to communicate to server");
          } else {
            // Neither error.response and error.request exist
            // Request was *not* made
            console.log("Error adding ingredient: error making request");
          }
        });
      }
    }
  }
</script>

<style scoped>
.ingredient-button {
  padding: 10px;
  margin: 10px; /* Add more padding as needed */
}

</style>