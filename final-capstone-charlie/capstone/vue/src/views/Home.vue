<template>
  <div class="home">
    
    <section class="hero">
      <div class="hero-content">
        <h1>Welcome to Meal Planner</h1>
        <p>Plan, cook, and enjoy delicious meals with ease.</p>
        <button v-on:click="getStarted" class="global-button">Get Started</button>
      </div>
      <div class="hero-image">
        <img src="https://www.smithsfoodanddrug.com/content/v2/binary/image/featured/meal-planning-by-diet-plant-based/imageset_hero_05-01--4910004_p4w2_2023_p4w2_dhw_specialty_diet_refresh_10_5daysrecipes_subpage_dsk_hero_plantbase_948x312.jpg" alt="Meal Planning">
      </div>
    </section>

    
  </div>
</template>

<script>
export default {
  name: "home",

  methods: 

  {
    getStarted()
    {
      if(this.$store.state.user == undefined)
      {
          this.$router.push('/register')
      }

      else if (this.$store.state.token == '')
      {
           this.$router.push('/login')
      }

      else 
      {
           this.$router.push('/addmealplan')

      }

    }   

  }
};
</script>

<style scoped>

.hero {
  display: flex; 
  align-items: center; 
  padding: 50px 0;
   position: relative;
}

.hero-image::before {
  content: "";
  position: absolute; 
  top: 0;
  right: 0;
  bottom: 0;
  left: 25%; 
  background: radial-gradient(circle, rgba(255, 255, 255, 0) 100%, #FAF3E0);
  pointer-events: none; 
}

.hero-content {
  flex: 1; 
  padding-right: 50px;
  text-align: left;
}

.hero-image {
  flex: 2;
  position: relative;
}

.hero-image img {
  max-width: 100%; 
  height: auto;
  display: block;
}

.global-button {
  background-color: var(--accent-color);
  color: white;
  padding: 0.75vh;
  margin: 0.5vh 0;
}


</style>

