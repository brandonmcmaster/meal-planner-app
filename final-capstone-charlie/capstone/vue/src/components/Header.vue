<template>
  <header class="header">
    <login-component/>
  </header>
</template>

<script>
import LoginComponent from './LoginComponent.vue'
  export default {
  components: { LoginComponent },

  }
</script>

<style scoped>
.header {
  background-image: url('https://www.cannolikitchen.com/wp-content/uploads/2022/04/fresh_ingredients_cannoli_kitchen-scaled.jpg');
  background-size: cover;
  background-position: center;
  height: 100px;
  border-bottom: solid 2px black;
}
</style>