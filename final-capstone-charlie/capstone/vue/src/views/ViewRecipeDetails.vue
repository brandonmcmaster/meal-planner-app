<template>
  
  <div>
    
    <recipe-details-vue v-bind:recipeId="this.recipeId" />
  </div>
</template>

<script>
import RecipeDetailsVue from '../components/RecipeDetails.vue'

export default {
    name: 'ViewRecipeDetails',
  components: {RecipeDetailsVue},

data()
  {
      return {
          recipeId : this.$route.params.recipeId
      }
  }, 
}
</script>

<style>




</style>