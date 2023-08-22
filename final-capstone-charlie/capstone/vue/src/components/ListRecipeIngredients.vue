<template>
  <div >
  
      <div class="modify-recipe-ingredients-container" v-show="isToggled">
           <h2> Recipe Ingredients</h2>
          
            <li class="recipe-ingredients" v-for="recipeIngredient in displayedIngredients" v-bind:key="recipeIngredient.recipeIngredientId">
          {{recipeIngredient.itemName}} - {{recipeIngredient.amount}} {{recipeIngredient.unitOfMeasurement}} 
              <router-link v-bind:to="{name: 'EditRecipeIngredients', params: {recipeIngredientId: recipeIngredient.recipeIngredientId}}"> edit </router-link >
                    

          </li>
           <button class="global-button" v-on:click="toggleForm">Add New Ingredient to Recipe</button> 
      </div>
      
    
        <recipe-ingredients-vue v-bind:recipeId="recipe.recipeId" v-show="!isToggled"/>
  </div>
</template>

<script>
import RecipeIngredientsVue from '../components/RecipeIngredients.vue';



export default {
    props : ['recipeId'],
    components : {RecipeIngredientsVue},
    name:  "listRecipeIngredients",
      data() {
      return {
        displayedIngredient : {}, 
        isToggled : true

      }
    },

    methods :
    {
     toggleForm(){
        
        if(this.isToggled)
        {
          this.isToggled = false;
        }

        else 
        {
          this.isToggled = true;  
        }

        return this.isToggled
      },
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
.recipe-ingredients {
  padding: 20px; /* Add padding to align with EditRecipe.vue */
  font-family: Arial; /* Match the font from EditRecipe.vue */
  font-size: 13.3px; /* Match the font size from EditRecipe.vue */
}

li {
  margin: 10px 0; /* Add space between the list items */
}


 .modify-recipe-ingredients-container{

   list-style: none;
   
 }


</style>