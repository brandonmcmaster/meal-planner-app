<template>
  <div class="meal-plan-details-container">
    <h1>Meal Plan Details</h1>
    <table class="meal-plan-details">
      <tr>
        <th>Meal Plan Name:</th>
        <td>{{mealPlan.mealPlanName}}</td>
      </tr>
      <tr>
        <th>Meal Plan Start Date:</th>
        <td>{{convertedStartDate}}</td>
      </tr>
      <tr>
        <th>Meal Plan End Date:</th>
        <td>{{convertedEndDate}}</td>
      </tr>
    </table>
    <div class="button-container">
    <router-link v-bind:to="{name: 'GroceryListView', params: {mealPlanId: mealPlan.mealPlanId}}" class="global-button">Print Grocery List</router-link>
  </div>
  </div>
</template>

  
<script>
export default {

    props: ['mealPlan'],
  name: "MealPlanDetails",

  data() {

      return {
    
            convertedStartDate : this.convertStartDate(),
            convertedEndDate : this.convertEndDate()       
          
      }
  },

    methods: {

            convertStartDate(){

                   let date = this.mealPlan.startDate 
                 
                 
                    return  date.substring(0,10)
 
            },

             convertEndDate(){

                   let date = this.mealPlan.endDate 
                 
                 
                    return  date.substring(0,10)
 
            }

    }

};



</script>
  
<style scoped>
.meal-plan-details-container {
  text-align: center; 
}

.meal-plan-details {
  width: 80%;
  margin: 20px auto;
  border-collapse: collapse; 
}
.meal-plan-details th,
.meal-plan-details td {
  border: 3px solid var(--text-color); 
  padding: 10px;
  text-align: left;
}

.button-container {
  width: 80%;
  margin: 20px auto;
  text-align: center;
}

.global-button {
  display: inline-block;
  width: 98%; 
  padding: 10px;
  text-align: center;
  text-decoration: none;
  border-radius: 5px;
}

</style>