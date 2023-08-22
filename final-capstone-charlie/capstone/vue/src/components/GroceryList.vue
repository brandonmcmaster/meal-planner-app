<template>
  <div class="grocery-list-container">
    <h1>Grocery List</h1>
    <form>
      <ul>
        <li v-for="ingredient in groceryList" :key="ingredient.ingredient_id">
         <span class="item-name">{{ ingredient.itemName }}</span> - {{ ingredient.amount }}  
          {{ ingredient.unitOfMeasurement }} 
        </li>
      </ul>
      
    </form>
  </div>
</template>

<script>
import recipeIngredientsService from '../services/RecipeIngredientsService.js';

export default {
  name: "GroceryList",
  data() {
    return {
      groceryList: [],
    };
  },
  props: {
    mealPlanId: {
      type: Number,
      required: true,
    },
  },
  created() {
    this.getGroceryList(this.mealPlanId);
  },
  methods: {
  getGroceryList() {
    recipeIngredientsService.fetchGroceryList(this.mealPlanId)
      .then((response) => {
          console.log("Response data:", response.data)
        this.groceryList = response.data;
      })
      .catch((error) => {
        console.error("An error occurred:", error);
      });
  },
},
};
</script>


<style scoped>
.grocery-list-container {
  padding: 20px;
  border: 1px solid var(--secondary-bg);
  background-color: var(--primary-bg);
  border: 2px solid #333333;
   width: fit-content;
    margin: 10px auto;
    padding: 20px;
}

.grocery-list-container h1 {
  text-decoration: underline;
}

ul {
  list-style: none;
}

.grocery-list-container .item-name {
  font-weight: bold; 
}

li {
  position: relative;
  padding-left: 20px;
}

li::before {
  content: '•';
  position: absolute;
  left: 0;
  color: var(--accent-color);
}

</style>

