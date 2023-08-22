import axios from 'axios';

export default {
    list() {
        return axios.get('/meal');
    },
    add(meal) {
        return axios.post('/meal', meal);
    },
    update(meal) {
        return axios.put(`/meal/${meal.mealId}`, meal);
    }
}