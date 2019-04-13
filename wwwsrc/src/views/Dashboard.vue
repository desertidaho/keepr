<template>
  <div class="dashboard">
    <navbar />
    <h1>Welcome Dashboard</h1>
  </div>
</template>

<script>
  import Navbar from '@/components/Navbar.vue'

  export default {
    name: "dashboard",
    mounted() {
      //blocks users not logged in
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }
    },
    data() {
      return {

      };
    },
    methods: {

    },
    components: {
      Navbar
    }
  };
</script>

<style>
  .dashboard {
    min-height: 100vh;
    background-image: linear-gradient(to right, #ff7300 30%, #ffc400);
  }
</style>