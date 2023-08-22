<template>
    <div class="cards">

      <router-link v-bind:to="{name: 'mealPlanDetails', params: {mealPlanId: mealPlan.mealPlanId}}" v-for="mealPlan in mealPlans" v-bind:key="mealPlan.mealPlanId" class="card" href="#">
      
        
            <div class="card-content">
                <h2>{{ mealPlan.mealPlanName }}</h2>
            </div>
            <router-link v-bind:to="{name: 'editMealPlan', params: {mealPlanId: mealPlan.mealPlanId}}" class="global-button" >Edit</router-link>
        
        </router-link>
    </div>
</template>
  
<script>
  export default {
    name: "ListMealPlans",
    computed: {
        mealPlans() {
            return this.$store.state.mealPlans.filter((mealPlan) => {
              return mealPlan.userId == this.$store.state.user.userId;
            });
        }
    },
    created() {
        this.$store.commit("LOAD_MEAL_PLANS");
    }
  }
</script>
  
<style scoped>
.recipes-container {
  padding: 20px;
}

.cards {
   margin: 20px;
  display: grid;
  grid-template-columns: repeat( 1fr); 
  gap: 20px; 
}

@media (min-width: 600px) {
  .cards {
    grid-template-columns: repeat(2, 1fr);
    padding: 10px;
  }
}

@media (min-width: 900px) {
  .cards {
    grid-template-columns: repeat(3, 1fr);
  }
}

.card {
  display: flex;
  border: 2px solid #ccc;
  flex-direction: column; 
  border: 3px solid #ccc; /* Increased border width */
  border-radius: 10px; /* Rounded edges */
  padding: 15px;
  background-color: #f9f9f9; /* Background color */
  text-decoration: none;
  color: inherit;
  box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1); /* Subtle shadow */
  transition: all 0.3s; /* Smooth transition for hover effect */ 
}

.card-content {
  flex-grow: 1;
  text-align: center; /* Allow content to take up full width */
  margin: 2% 0;;
}

.card-content h2 {
  margin: 10px 0; /* Add spacing around the title */
}

.card:hover {
  border-color: var(--accent-color);
  background-color: #f5f5f5; /* Hover background color */
}
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
</style>