<template>
    <meal-plan-details v-bind:mealPlan="this.mealPlan"/>
</template>
  
<script>
import MealPlanDetails from "../components/MealPlanDetails.vue"

export default {
    name: "MealPlanDetailsView",
    components: {MealPlanDetails},
    data()
    {

        return {

            mealPlanId: this.$route.params.mealPlanId

        }

    },

    computed: {

        mealPlan(){

            return this.$store.state.mealPlans.find((mealPlan) =>
            {
               return mealPlan.mealPlanId == this.mealPlanId

            })
        }

    }
}
</script>
  
<style>

</style>