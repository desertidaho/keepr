<template>
  <div id="app">
    <router-view />
  </div>
</template>

<script>
  import Navbar from '@/components/Navbar.vue'

  export default {
    name: 'App',
    data() {
      return {
      }
    },
    mounted() {
    },
    methods: {
    },
    components: {
      Navbar
    }
  }
</script>

<style>
  #app {
    /* font-family: "Avenir", Helvetica, Arial, sans-serif; */
    font-family: 'Noto Serif', serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    overflow-x: hidden;
    text-align: center;
    color: #2c3e50;
  }

  #nav {
    padding: 30px;
  }

  #nav a {
    font-weight: bold;
    color: #2c3e50;
  }

  #nav a.router-link-exact-active {
    color: #42b983;
  }
</style>