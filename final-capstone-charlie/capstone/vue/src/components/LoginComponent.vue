<template>
  <div class="login-container">
    <div class="login-box" v-if="!isLoggedIn">
      <form @submit.prevent="login">
        <input type="text" v-model="user.username" placeholder="Username" required />
        <input type="password" v-model="user.password" placeholder="Password" required />
        <button type="submit">Login</button>
        <router-link to="/register" class="register-link">Register</router-link>
      </form>
    </div>
    <div v-else>
      <img class="avatar" :src="userAvatar" alt="User Avatar" />
       <button @click="logout" class="global-button">Logout</button>
    </div>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  computed: {
    isLoggedIn() {
      return !!this.$store.state.token; 
    },
    userAvatar() {
      return "https://cdn.vectorstock.com/i/preview-lt/99/94/default-avatar-placeholder-profile-icon-male-vector-23889994.webp"; // Assuming avatar URL is stored in Vuex
    }
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch(error => {
          const response = error.response;
          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
    logout() {
      this.$store.commit("LOGOUT");   
      this.$router.push("/"); 
    }
  }
};
</script>

<style scoped>

.header {
  position: relative;
}

.login-container {
  position: absolute;
  top: 3px;
  right: 10px;
  max-width: 150px;
}

.login-box {
  background-color: #FAF3E0; 
  padding: 10px; 
  border-radius: 2px;
  border: 2px solid #FFA539; 
  box-sizing: border-box;
  overflow: hidden;
  width: 120px;
}
input[type="text"],
input[type="password"],
button {
  display: block;
  margin: 2px 0;
  width: 100%;
  font-size: 8px; 
  padding: 2px; 
}

.register-link {
  display: block;
  margin: 2px 0;
  font-size: 8px; 
}

.avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
}
</style>
