<template>
  <div class="recipes-container">
    <h1>Recipe Library</h1>
    <div class="cards">
      <a v-for="recipe in recipes" v-bind:key="recipe.recipeId" class="card" href="#">
        <img :src="recipe.imageUrl" alt="Recipe Image" class="card-image" />
        <div class="card-content">
          <h2>{{recipe.recipeName}}</h2>
          <p class="recipe-description">Description: {{recipe.recipeDescription}}</p>
          <br>
          <router-link class="global-button" v-bind:to="{name: 'RecipeDetails', params: {recipeId: recipe.recipeId}} " >Details</router-link>
         
        </div>
      </a>
    </div>
    <button v-on:click="pushToForm" class="global-button add-recipe-button">Add Recipe</button>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        
      };
    },
    methods: {
      pushToForm() {
        this.$router.push('/addrecipes')
      }
    },
    computed: {
      recipes() {
        return this.$store.state.recipes;
      }
    },
    created() {
      this.$store.commit("LOAD_RECIPES");
      this.$store.commit("LOAD_RECIPE_INGREDIENTS");
    }
  }
</script>

<style >
  .recipes-container {
    padding: 20px;
  }

  .cards {
    display: grid;
    grid-template-columns: repeat(3, 1fr); 
    gap: 20px; 
  }

  .card {
    display: flex;
    border: 2px solid #ccc;
    flex-direction: column; 
    border: 3px solid #ccc; 
    border-radius: 10px; 
    padding: 15px;
    background-color: #f9f9f9; 
    text-decoration: none;
    color: inherit;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1); 
    transition: all 0.3s; 
  }

  .card-content {
    flex-grow: 1;
    text-align: center; 
    margin: 2% 0;;
  }

.card-image {
  width: 150px; 
  height: 150px; 
  object-fit: cover;
  margin: 0 auto; 
}

  .card-content h2 {
    margin: 10px 0; 
  }

.card:hover {
  border-color: var(--accent-color);
  background-color: #f5f5f5; 
}

.global-button{
 padding: 1.5% 0;
    display: grid;
    margin: 0;
    text-align: center;
    text-decoration: none;
    color: white;
    background-color: var(--accent-color);
    transition: background-color 0.25s;
    cursor: pointer;

}
.global-button:hover {

  background-color: #724a16; 
}

.recipe-description {

 text-align: center;
}

.add-recipe-button{
  padding: 10px;
  margin: 10px;
}

</style>