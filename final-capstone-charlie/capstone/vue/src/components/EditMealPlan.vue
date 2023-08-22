<template>
    <div>
        <section>
            <form v-on:submit.prevent="editMealPlan">
                <label>
                    <p>Name:</p> <input type="text" name="name" id="name" v-model="mealPlan.mealPlanName" required />
                </label>
                <label>
                    <p>Start Date:</p> <input type="date" name="name" id="name" v-model="startDate" required />
                </label>
                <label>
                    <p>End Date:</p> <input type="date" name="name" id="name" v-model="endDate" required />
                </label>
                <button type="submit" class="global-button">Save Changes</button>
            </form>
        </section>
    </div>
</template>
  
<script>
    import mealPlanService from "../services/MealPlanService.js";

    export default {
        name: "EditMealPlan",
        data() {
            return {
                mealPlanId: this.$route.params.mealPlanId
            };
        },
        methods: {
            editMealPlan() {
                mealPlanService
                .update(this.mealPlan)
                .then(() => {
                    this.$router.push({ name: "viewMealPlans" });
                })
                .catch((error) => {
                    if (error.response) {
                        // error.response exists
                        // Request was made, but response has error status (4xx or 5xx)
                        console.log("Error updating meal plan: ", error.response.status);
                    } else if (error.request) {
                        // There is no error.response, but error.request exists
                        // Request was made, but no response was received
                        console.log("Error updating meal plan: unable to communicate to server");
                    } else {
                        // Neither error.response and error.request exist
                        // Request was *not* made
                        console.log("Error updating meal plan: error making request");
                    }
                });
            },
        },
        computed: {
            mealPlan() {
                return this.$store.state.mealPlans.find((mealPlan) => {
                    return mealPlan.mealPlanId == this.mealPlanId;
                });
            },
            startDate: {
                get() {
                    return this.mealPlan.startDate.split('T')[0];
                },
                set(newDate) {
                    const time = this.mealPlan.startDate.split('T')[1];
                    const newDateTime = `${newDate}T${time}`;
                    this.mealPlan.startDate = newDateTime;
                }
            },
            endDate: {
                get() {
                    return this.mealPlan.endDate.split('T')[0];
                },
                set(newDate) {
                    const time = this.mealPlan.endDate.split('T')[1];
                    const newDateTime = `${newDate}T${time}`;
                    this.mealPlan.endDate = newDateTime;
                }
            }
        },
        created() {
            this.$store.commit("LOAD_MEAL_PLANS");
        },
    }
</script>
  
<style scoped>
    label {
        display: flex;
        flex-direction: row;
        justify-content: left;
        width: 500px;
        line-height: 26px;
        margin: 10px 0;
    }

    input {
        box-sizing: border-box;
        padding: 0 0.5vh;
        flex: 0 0 200px;
        font-family: Arial;
        font-size: 13.3px;
    }

    p {
        flex: 0 0 100px;
        text-align: left;
    }

    .global-button{
        padding: 10px;
    }
</style>