<template>
  <div class="recipes-container">
    <h1>Recipe Library</h1>
    <div class="cards">
       
     
      <router-link v-bind:to="{name: 'RecipeDetails', params: {recipeId: recipe.recipeId}}" v-for="recipe in currentUserRecipes" v-bind:key="recipe.recipeId" class="card" href="#" >  
        <img :src="recipe.imageUrl" alt="Recipe Image" class="card-image" />
        <div class="card-content">
          <h2>{{recipe.recipeName}}</h2>
          <p class="recipe-description">Description: {{recipe.recipeDescription}}</p>
        </div>
        <router-link v-bind:to="{name: 'editRecipe', params: {recipeId: recipe.recipeId}}" class="edit-button" >Edit</router-link>

        </router-link>
      
  
    </div>
    <button v-on:click="pushToForm" class="global-button add-recipe-button">Add Recipe</button>
  </div>
</template>

<script>
  export default {
    methods: {
      pushToForm() {
        this.$router.push('/addrecipes')
      },

      routeToMyRecipes()
      {

           this.$router.push('/myrecipes')
      }
    },
    computed: {
      currentUserRecipes() {
        return this.$store.state.recipes.filter((recipe) => {
          return recipe.userId == this.$store.state.user.userId;
        });
      },
    },
    created() {
        this.$store.commit("LOAD_RECIPES");
    }
  };
</script>

<style scoped>
  .edit-button {
    padding: 1.5% 0;
    margin: 0;
    text-align: center;
    text-decoration: none;
    color: white;
    background-color: var(--accent-color);
    transition: background-color 0.25s;
    cursor: pointer;
  }
  .edit-button:hover {
    background-color: #724a16; 
  }

  .add-recipe-button{
    padding: 10px;
  }

  .cards {
  display: grid;
  grid-template-columns: 1fr; /* Single column layout for small screens */
  gap: 20px;
  margin: 20px; /* Margin around the container */
}

/* Two columns for medium screens */
@media (min-width: 600px) {
  .cards {
    grid-template-columns: repeat(2, 1fr);
  }
}

/* Three columns for large screens */
@media (min-width: 900px) {
  .cards {
    grid-template-columns: repeat(3, 1fr);
  }
}

</style>