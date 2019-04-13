<template>
  <div class="home">
    <navbar />
    <h1>Welcome Homey</h1>
  </div>
</template>

<script>
  import Navbar from '@/components/Navbar.vue'

  export default {
    name: "home",
    mounted() {
    },
    data() {
      return {

      };
    },
    computed: {
      activeUser() {
        return this.$store.state.user
      }
    },
    components: {
      Navbar
    }
  };
</script>

<style>
  .home {
    min-height: 100vh;
    background-image: linear-gradient(to right, #ff7300 30%, #ffc400);
  }
</style>