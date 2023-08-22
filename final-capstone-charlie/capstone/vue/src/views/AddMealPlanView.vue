<template>
    <div>
        <add-meal-plan/>
    </div>
</template>
  
<script>
import AddMealPlan from "../components/AddMealPlan.vue"

export default {
    name: "AddMealPlanView",
    components: {AddMealPlan},
    beforeRouteLeave(to, from, next) {
            this.$store.state.pendingMeals= [];
            next();
    }
}
</script>
  
<style>

</style>