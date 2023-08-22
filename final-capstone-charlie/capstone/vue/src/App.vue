<template>
  <div id="app" class="grid-container">
     <Header class="header" />
    <Navbar class="navbar" />
    <div class="main-content">
      <router-view />
    </div>
     <Footer class="footer" />
  </div>
</template>

<script>
  import Header from './components/Header.vue';
  import Navbar from './components/Navbar.vue';
  import Footer from './components/Footer.vue';


  export default {
    components: {
      Header,
      Navbar,
      Footer
    },
    created(){
      this.$store.commit("LOAD_MEAL_PLANS");
      this.$store.commit("LOAD_MEALS");
      this.$store.commit("LOAD_RECIPES");
      this.$store.commit("LOAD_RECIPE_INGREDIENTS");
      this.$store.commit("LOAD_INGREDIENTS");
    }
  }
</script>

<style>

:root {
  --primary-bg: #FAF3E0;
  --secondary-bg: #869e6b;
  --accent-color: #FFA539;
  --text-color: #333333;
  --footer-color: #cccccc;
}

* {
  padding: 0;
  margin: 0;
}

body {
  background-color: var(--primary-bg);
  color: var(--text-color);
  font-family: 'Arial', sans-serif; /* Example font */
}

.grid-container {
  display: grid;
  grid-template-rows: auto 1fr auto;
  grid-template-columns: 200px 1fr;
  height: 100vh; 
}

.header {
  grid-row: 1;
  grid-column: 1 / span 2; 
}

.navbar {
  grid-column: 1; 
}

.main-content {
  grid-column: 2; 
}

.footer {
  height: 60px;
  grid-row: 3;
  grid-column: 1 / span 2; 
  margin: 0;
  padding: 0;
}

.global-button, .global-button button{
 padding: 20px 10px;
    display: grid;
    margin: 10px;
    text-align: center;
    text-decoration: none;
    color: white;
    background-color: var(--accent-color);
    transition: background-color 0.25s;
    border-radius: 2px; 
    cursor: pointer;
  border: none;

}
.global-button:hover {

  background-color: #724a16; 
}

</style>