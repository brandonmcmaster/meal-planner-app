<template>
    <div class="add-meal-plan-form-container">
        <form v-on:submit.prevent="addMealPlan">
            <h1>Add A Meal Plan</h1>
            <div>
                <label for="name" class="add-meal-plan-label">Meal Plan Name: </label>
                <input v-model="mealPlan.mealPlanName" type="text" name="name" class="short-input" required />
            </div>
            <div>
                <label for="start-date" class="add-meal-plan-label">Start Date: </label>
                <input v-model="mealPlan.startDate" v-bind:min="today()" v-bind:max="mealPlan.endDate" type="date" name="start-date" class="short-input" required />
            </div>
            <div>
                <label for="end-date" class="add-meal-plan-label">End Date: </label>
                <input v-model="mealPlan.endDate" v-bind:min="endDateMin()" type="date" name="end-date" class="short-input" required />
            </div>

            <list-pending-meals v-bind:mealPlanId="nextMealPlanId()" v-bind:mealPlan="this.mealPlan" />

            <button type="button" v-on:click="checkValidity" class="submit-button add-meal-plan-label-button" title="Add a meal to your meal plan!">Add Meal</button>
            <add-meal v-show="isVisible" v-on:close="isVisible=false" v-bind:mealPlan="mealPlan" v-bind:mealPlanId="nextMealPlanId()" />
            <div class="add-meal-plan-button-container">
                <button type="submit" class="submit-button add-meal-plan-button">Add Meal Plan</button>
                <button type="button" v-on:click="lastPage" class="submit-button add-meal-plan-button">Cancel</button>
            </div>
        </form>
    </div>
</template>
  
<script>
    import mealPlanService from "../services/MealPlanService.js";
    import AddMeal from "../components/AddMeal.vue";
    import ListPendingMeals from "../components/ListPendingMeals.vue";

    export default {
        name: "AddMealPlan",
        components: { AddMeal, ListPendingMeals },
        data() {
            return {
                mealPlan: {},
                isVisible: false,
            }
        },
        methods: {
            addMealPlan() {
                const mealPlanMeal = {
                    mealPlan: this.mealPlan,
                    meals: this.$store.state.pendingMeals,
                    mealPlanId: this.nextMealPlanId()
                };

                mealPlanService.add(mealPlanMeal)
                .then(() => {
                    this.$router.push({ name: "viewMealPlans" });
                })
                .catch((error) => {
                    window.alert("Meal plan creation unsuccessful.");
                    if (error.response) {
                    // error.response exists
                    // Request was made, but response has error status (4xx or 5xx)
                    console.log("Error adding meal plan: ", error.response.status);
                    } else if (error.request) {
                    // There is no error.response, but error.request exists
                    // Request was made, but no response was received
                    console.log(
                        "Error adding meal plan: unable to communicate to server"
                    );
                    } else {
                    // Neither error.response and error.request exist
                    // Request was *not* made
                    console.log("Error adding meal plan: error making request");
                    }
                });
            },
            nextMealPlanId() {
                let result = 0;
                this.$store.state.mealPlans.forEach((mealPlan) => {
                if (mealPlan.mealPlanId > result) {
                    result = mealPlan.mealPlanId;
                }
                });
                return result + 1;
            },
            checkValidity(event) {
                if (this.mealPlan.startDate && this.mealPlan.endDate) {
                    this.isVisible = !this.isVisible;
                }
                else {
                    window.alert("Please fill out the Start Date and End Date before adding a meal.");
                    event.preventDefault();
                }
            },
            lastPage() {
                this.$router.go(-1);
            },
            today() {
                const today = new Date();
                const year = today.getFullYear();
                const month = today.getMonth() + 1;
                const day = today.getDate();
                
                if (month < 10) {
                    return `${year}-0${month}-${day}`;
                }
                else {
                    return `${year}-${month}-${day}`;
                }
            },
            endDateMin() {
                if (this.mealPlan.startDate == null || this.mealPlan.startDate == "") {
                    return this.today();
                }
                else {
                    return this.mealPlan.startDate;
                }
            }
        },
        computed: {
            /* meals() {
                return this.$store.state.meals.filter((meal) => {
                    return meal.mealId = this.meal.mealId;
                });
            }, */
        },
    }
</script>
  
<style scope>
    .add-meal-plan-form-container {
        text-align: center;
        max-width: 600px;
        margin: 50px auto; 
        padding: 20px;
        background-color: #f9f9f9; 
    }

    .add-meal-plan-label {
        display: block;
        width: 100%;
    }
    .add-meal-plan-label-button {
        display: block;
        margin: 20px auto;
    }
    .add-meal-plan-button {
        width: 140px;
        margin: 20px 0 10px 10px;
    }
</style>