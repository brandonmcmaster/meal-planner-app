n<template>
  <div class="nav-bar">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>
      <router-link v-bind:to="{name: 'login'}">Login</router-link> 
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <router-link v-bind:to="{name: 'ingredient'}" v-if="$store.state.token != ''">Add Ingredient</router-link> 
      <router-link v-bind:to="{name: 'addrecipes'}" v-if="$store.state.token != ''">Add Recipe</router-link> 
      <router-link v-bind:to="{name: 'ViewRecipes'}">View All Recipes</router-link>
      <router-link v-bind:to="{name: 'viewMealPlans'}" v-if="$store.state.token != ''">View My Meal Plans</router-link> 
      <router-link v-bind:to="{name: 'ViewRecipesByUser'}" v-if="$store.state.token != ''">View My Recipes</router-link>
   
  </div>
</template>

<script>
  export default {
    computed: {
      isLoggedIn() {
        return this.$store.state.isLoggedIn;
      }
    }
  }
</script>

<style scoped>
  .nav-bar {
    width: 160px; 
    height: 100%;
    position: fixed; 
    top: 0;
    left: 0;
    background-color: #869e6b; 
    display: flex;
    flex-direction: column; 
    align-items: center;
    border-right: 2px solid black;
    border-left: 3px solid black;
  }

  .nav-bar a {
  box-sizing: border-box; 
    margin: 0; 
    text-decoration: none;
    color: white; 
    padding: 15px; 
    width: 100%;
    text-align: center;
    transition: background-color 0.3s; 
    border-bottom: 2px inset black;
  }

  .nav-bar a:hover {
    background-color: rgb(26, 77, 2);
  }
</style>    