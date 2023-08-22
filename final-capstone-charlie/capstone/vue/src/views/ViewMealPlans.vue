<template>
    <div>
        <h1>My Meal Plans</h1>
        <list-meal-plans/>
        <router-link class="add-meal-plan-button" v-bind:to="{name: 'addMealPlan'}">
            <button class="global-button"> Add Meal Plan</button>
            </router-link>
    </div>
</template>
  
<script>
import ListMealPlans from "../components/ListMealPlans.vue"

export default {
    name: "ViewMealPlans",
    components: {ListMealPlans},
}
</script>
  
<style scoped>
 .add-meal-plan-button {
    text-decoration: none;
     padding: 10px
            
  }

  .add-meal-plan-button button{
      border: none; 
      padding: 5px;
  }
</style>