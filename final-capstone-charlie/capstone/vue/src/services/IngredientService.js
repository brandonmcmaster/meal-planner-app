import axios from 'axios';

export default {
  add(ingredient) {
    return axios.post('/ingredient', ingredient)
  },
  listIngredients() {
    return axios.get('/ingredient')
  }
}