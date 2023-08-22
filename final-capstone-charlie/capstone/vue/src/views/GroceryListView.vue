<template>
  <div>
    <groceryList :mealPlanId="mealPlanId" />
  </div>
</template>

<script>
import GroceryList from '../components/GroceryList.vue';

export default {
  components: {GroceryList},
  props: {
    mealPlanId: {
      type: Number,
      required: true,
    },
  },
};
</script>
