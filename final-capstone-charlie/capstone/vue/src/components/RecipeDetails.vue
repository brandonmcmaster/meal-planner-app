<template>
  <div class="recipe-details">
    <h1 class="recipe-title">{{ recipe.recipeName }}</h1>
    <img class="recipe-image" :src="recipe.imageUrl" alt="Recipe Image" />
    <div class="recipe-content">
      <h2>Description</h2>
      <p class="recipe-description">{{ recipe.recipeDescription }}</p>
      
      <div class="recipe-ingredients">
        <h2> Recipe Ingredients</h2>
        <li v-for="recipeIngredient in displayedIngredients" v-bind:key="recipeIngredient.recipeIngredientId">
          {{recipeIngredient.itemName}} - {{recipeIngredient.amount}} {{recipeIngredient.unitOfMeasurement}}</li>
          </div>
      <h2>Instructions</h2>
      <p class="recipe-instructions">{{ recipe.instructions }}</p>
      
      
    </div>
  
  </div>
  
</template>


<script>


  export default {
    props: ['recipeId'],
    components: {},
    data() {
      return {
        displayedIngredient : {},
        
         
      }
    },
    computed: {
       
     
      displayedIngredients() {

            const newArray = this.recipeIngredients.map(obj => ({
                        ...obj,
                ['itemName']: this.ingredients.find(item => item.ingredientId === obj.ingredientId)?.itemName
                                  }));

                                return newArray;

    },

      recipe() {
        return this.$store.state.recipes.find((recipe) => {
          return recipe.recipeId == this.recipeId;
        });
      },
      recipeIngredients() {
        return this.$store.state.recipeIngredients.filter((recipeIngredient) => { 
            return recipeIngredient.recipeId == this.recipeId; 
        });
      },
      ingredients() {
        return this.$store.state.ingredients.filter((ingredient) => {
          return this.recipeIngredients.find((recipeIngredient) => {
            return recipeIngredient.ingredientId == ingredient.ingredientId;
          });
        });
      }
 
    },
    created() {
    
        this.$store.commit('LOAD_INGREDIENTS');
         this.$store.commit('LOAD_RECIPE_INGREDIENTS');
    }
  }
</script>

<style scoped>
.recipe-details {
  max-width: 600px; /* You can adjust this as needed */
  margin: 20px auto;
  padding: 20px;
  background-color: #f9f9f9;
  border: 2px solid var(--accent-color);
  text-align: center; /* Centering text */
}

.recipe-image {
  width: 250px;
  height: 1fr;
  object-fit: cover;
   display: block; /* To enable centering with margin */
  margin: 0 auto; /* Centering image horizontally */
}

.recipe-title {
  font-size: 28px; 
  text-align: center; 
  margin: 20px 0;
  font-weight: bold; 
  text-decoration: underline; 
  font-family: 'Georgia', serif; 
}

.recipe-content h2 {
  font-size: 20px;
  margin: 15px 0;
  text-align: center;
}

.recipe-content p {
  font-size: 16px;
  line-height: 1.5;
}

.recipe-description {
  font-size: 16px;
  line-height: 1.5;
  white-space: pre-line; /* This line handles the line breaks */
  text-align: center;
}

.recipe-instructions {
  font-size: 16px;
  line-height: 1.5;
  white-space: pre-line; /* This line handles the line breaks */
  text-align: center;
}

.recipe-ingredients{

  font-size: 16px;
  line-height: 1.5;  
  white-space: pre-line; /* This line handles the line breaks */
  text-align: center;
  list-style-type: none;
  text-transform:capitalize

}
</style>