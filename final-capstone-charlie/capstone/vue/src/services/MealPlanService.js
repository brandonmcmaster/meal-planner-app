import axios from 'axios';

export default {
    list() {
        return axios.get('/mealplan');
    },
    add(mealPlan, meals) {
        return axios.post('/mealplan', mealPlan, meals);
    },
    update(mealPlan) {
        return axios.put(`/mealplan/${mealPlan.mealPlanId}`, mealPlan);
    }
}