<template>
    <div class="modal-overlay">
        <div class="modal">
            <div class="modal-content">
                <h2>Edit Your Meal</h2>
                <form v-on:submit.prevent="editMealInStore">
                    <div>
                        <label for="date">Date: </label>
                        <input v-model="meal.mealDate" type="date" name="date" v-bind:min="this.mealPlan.startDate" v-bind:max="this.mealPlan.endDate" required />
                    </div>
                    <div>
                        <label for="type">Meal Type: </label>
                        <select name="type" v-model="meal.mealType" required >
                            <option :value="'Breakfast'">Breakfast</option>
                            <option :value="'Lunch'">Lunch</option>
                            <option :value="'Dinner'">Dinner</option>
                        </select>
                    </div>
                    <div>
                        <label for="recipe">Recipe: </label>
                        <select name="recipe" v-model="meal.recipeId" required >
                            <option v-for="recipe in myRecipes" v-bind:value="recipe.recipeId" v-bind:key="recipe.recipeId">
                                {{ recipe.recipeName }}
                            </option>
                        </select>
                    </div>
                    <button type="submit" class="add-meal-button">Confirm Changes</button>
                </form>
                <button type="button" v-on:click="close" class="add-meal-close-button">Close</button>
            </div>
        </div>
    </div>
</template>
  
<script>
    export default {
        name: "EditPendingMeal",
        props : ['mealId', 'mealPlan'],
        data() {
            return {
                meal: this.setMeal(),
                breakfast: false,
                lunch: false,
                dinner: false
            }
        },
        methods: {
            editMealInStore() {
                // Check to see if other meals of the same date have the same meal type.
                if (this.mealTypeValidation()) {
                    this.pendingMeal.recipeId = this.meal.recipeId;
                    this.pendingMeal.dayOfTheWeek = this.dayOfTheWeek(this.meal.mealDate);
                    this.pendingMeal.mealDate = this.meal.mealDate;
                    this.pendingMeal.mealType = this.meal.mealType;
                    // Close the form.
                    this.close();
                }
                else {
                    window.alert("You can only have one of each meal type for a given day.");
                }
            },
            setMeal() {
                let pendingMeal = this.$store.state.pendingMeals.find((meal) => meal.mealId == this.mealId);
                let meal = {
                    mealId: pendingMeal.mealId,
                    mealPlanId: pendingMeal.mealPlanId,
                    recipeId: pendingMeal.recipeId,
                    dayOfTheWeek: pendingMeal.dayOfTheWeek,
                    mealDate: pendingMeal.mealDate,
                    mealType: pendingMeal.mealType
                };
                return meal;
            },
            mealTypeValidation() {
                let meals = this.$store.state.pendingMeals;
                let mealsValidation = true;

                for (let i = 0; i < meals.length; i++) {
                    if (meals[i].mealDate == this.meal.mealDate && (meals[i].mealType == this.meal.mealType && meals[i].mealId != this.meal.mealId)) {
                        mealsValidation = false;
                    }
                }

                return mealsValidation;
            },
            dayOfTheWeek(date) {
                const newDate = new Date(date.replace(/-/g, "/"));
                const weekday = ["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"];
                return weekday[newDate.getDay()];
            },
            close() {
                this.$emit('close');
            },
        },
        computed: {
            pendingMeal() {
                return this.$store.state.pendingMeals.find((meal) => meal.mealId == this.mealId);
            },
            myRecipes() {
                // Return a recipe array containing the recipes that have the same user ID as the currently logged in user.
                return this.$store.state.recipes.filter((recipe) => {
                    return recipe.userId == this.$store.state.user.userId;
                });
            },
        },
        created() {
            this.$store.commit("LOAD_MEALS");
            this.$store.commit("LOAD_MEAL_PLANS");
        },
    }
</script>
  
<style scoped>
    .modal-overlay {
        position: fixed; /* Removes the modal from the normal flow, allowing it to overlap other elements in the view. */
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.5);
        display: flex;
        align-items: center;
        justify-content: center;
        z-index: 9999; /* Makes sure to maintain the highest position, or stack level, on the page. */
    }
    .modal {
        background-color: #fff;
        padding: 20px;
        border-radius: 5px;
        box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.3);
    }
    .modal-content {
        width: 270px;
        height: 300px;
        text-align: center;
    }
    .modal-content label {
        display: block;
        width:100%;
    }
    .add-meal-button {
        margin: 10px auto;
    }
</style>