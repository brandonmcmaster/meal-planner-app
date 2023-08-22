<template>
    <div class="modal-overlay">
        <div class="modal">
            <div class="modal-content">
                <h2>Add A Meal</h2>
                <form v-on:submit.prevent="addMealToStore">
                    <div>
                        <label for="date">Date: </label>
                        <input v-model="meal.mealDate" type="date" name="date" v-bind:min="mealPlan.startDate" v-bind:max="mealPlan.endDate" required />
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
                            <option v-for="recipe in myRecipes" :value="recipe.recipeId" v-bind:key="recipe.recipeId">
                                {{ recipe.recipeName }}
                            </option>
                        </select>   
                    </div>
                    <button type="submit" class="global-button">Add Meal</button>
                </form>
                <button type="button" v-on:click="close" class="global-button">Close</button>
            </div>
        </div>
    </div>
</template>
  
<script>
    export default {
        name: "AddMeal",
        props : ['mealPlanId', 'mealPlan'],
        data() {
            return {
                meal: {},
            }
        },
        methods: {
            addMealToStore() {
                // Check to see if other meals of the same date have the same meal type.
                if (this.mealTypeValidation()) {
                    // Add remaining properties to meal object.
                    this.nextMealId(this.meal);
                    this.meal.mealPlanId = this.mealPlanId;
                    this.meal.dayOfTheWeek = this.dayOfTheWeek(this.meal.mealDate);
                    // Push the meal object to the pendingMeals array in the store.
                    this.$store.state.pendingMeals.push(this.meal);
                    // Empty out meal object.
                    this.meal = {};
                    // Close the form.
                    this.close();
                }
                else {
                    window.alert("You can only have one of each meal type for a given day.");
                }
            },
            mealTypeValidation() {
                let meals = this.$store.state.pendingMeals;
                let mealsValidation = true;

                for (let i = 0; i < meals.length; i++) {
                    if (meals[i].mealDate == this.meal.mealDate && meals[i].mealType == this.meal.mealType) {
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
            nextMealId(meal) {
                let lastMealId = 0;
                let iterator = 0;

                this.$store.state.meals.forEach((m) => {
                    if (m.mealId > lastMealId) {
                        lastMealId = m.mealId;
                    }
                });
                
                iterator += this.$store.state.pendingMeals.length;
                meal.mealId = lastMealId + iterator;
            },
            close() {
                this.meal = {};
                this.$emit('close');
            },
        },
        computed: {
            myRecipes() {
                // Return a recipe array containing the recipes that have the same user ID as the currently logged in user.
                return this.$store.state.recipes.filter((recipe) => {
                    return recipe.userId == this.$store.state.user.userId;
                });
            },
        }
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