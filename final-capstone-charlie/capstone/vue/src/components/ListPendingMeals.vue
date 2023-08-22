<template>
    <div>
         <div v-for="date in dates" v-bind:key="date.dateId" class="list-pending-meals-dates">
            <h2>{{ date.dayOfTheWeek }}, {{ date.mealDate }}</h2>
            <div class="list-pending-meals-meal-container">
                <div v-for="meal in meals(date)" v-bind:key="meal.mealId" class="list-pending-meals-meals">
                    <div class="list-pending-meals-meal-card">
                        <div class="list-pending-meals-image-container">
                            <img v-bind:src="getRecipeImageUrl(meal.recipeId)" v-bind:alt="getRecipeName(meal.recipeId)">
                        </div>
                        <h3>{{ meal.mealType }}</h3>
                        <p>{{ getRecipeName(meal.recipeId) }}</p>
                        <div>
                            <button v-on:click="isViewable=!isViewable" type="button" class="list-pending-meals-button">Edit</button>
                            <edit-pending-meal v-show="isViewable" v-on:close="isViewable=false" v-bind:mealId="meal.mealId" v-bind:mealPlan="passedMealPlan"/>
                            <button v-on:click="removeMeal(meal.mealId)" type="button" class="list-pending-meals-button">Remove</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
  
<script>
import EditPendingMeal from "../components/EditPendingMeal.vue";

export default {
    name: "ListPendingMeals",
    components: { EditPendingMeal },
    props: ['mealPlanId', 'mealPlan'],
    data() {
        return {
            passedMealPlan: this.mealPlan,
            isViewable: false,
        }
    },
    methods: {
        nextMealId() {
            let result = 0;
            this.$store.state.meal.forEach((meal) => {
                if (meal.mealId > result) {
                    result = meal.mealId;
                }
            });
            return result + 1;
        },
        getMealRecipeByRecipeId(recipeId) {
            let result = this.$store.state.recipes.find((recipe) => {
                return recipe.recipeId == recipeId;
            });
            return result
        },
        getRecipeName(recipeId) {
            const recipe = this.getMealRecipeByRecipeId(recipeId);
            return recipe.recipeName;
        },
        getRecipeImageUrl(recipeId) {
            const recipe = this.getMealRecipeByRecipeId(recipeId);
            return recipe.imageUrl;
        },
        meals(date) {
            return this.$store.state.pendingMeals.filter((meal) => {
                return meal.mealDate == date.mealDate;
            });
        },
        removeMeal(mealId) {
            this.$store.state.pendingMeals = this.$store.state.pendingMeals.filter((meal) => {
                return meal.mealId != mealId;
            });
        }
    },
    computed: {
        dates() {
            let meals = this.$store.state.pendingMeals;
            let isValid = true;
            let dates = [];

            meals.forEach((meal) => {
                let dateObject = {
                    dayOfTheWeek: meal.dayOfTheWeek,
                    mealDate: meal.mealDate
                };

                for (let i = 0; i < dates.length; i++) {
                    if (dates[i].mealDate == dateObject.mealDate) {
                        isValid = false;
                    }
                }
                if (isValid) {
                    dates.push(dateObject);
                }
            });

            return dates.sort((d1, d2) => new Date(d1.mealDate) - new Date(d2.mealDate));
        }
    },
    created() {
        this.$store.commit("LOAD_MEALS");
    },
}
</script>
  
<style scoped>
.list-pending-meals-dates {
    display: flex;
    flex-direction: column;
    align-items:center;
}
.list-pending-meals-dates h2 {
    margin: 15px 0 0 0;
    flex: 1;
}
.list-pending-meals-meal-container {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: stretch;
}
.list-pending-meals-meals {
    display:flex;
    justify-content: space-between;
    align-items: center;
}
.list-pending-meals-meal-card {
    display: flex;
    width: 250px;
    height: 300px;
    margin: 20px 10px;
    padding: 5px;
    justify-content: space-around;
    align-items: center;
    flex-direction: column;
    border: 2px solid black;
    background-color: white;
    box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
    border-radius: 15px;
    text-align: center;
}
.list-pending-meals-image-container {
    height: 200px;
    width: 225px;
    margin: 5px 0 0 0;
    border-radius: 15px;
    overflow: hidden;
    position: relative;
    background-color: #282A36;
}
.list-pending-meals-image-container img {
    width: 100%;
    height: 100%;
    object-fit: cover;
}
.list-pending-meals-meal-card p {
    flex: 1;
}
.list-pending-meals-meal-card h3 {
    margin: 5px 10px 0;
}
.list-pending-meals-meal-card button {
    height: 30px;
    width: 75px;
    margin: 5px;
    background-color: var(--accent-color);
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.2s;
}

.list-pending-meals-meal-card button:hover {
  background-color: #724a16; 
}
</style>