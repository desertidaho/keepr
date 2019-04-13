<template>
  <div class="dashboard">
    <navbar />
    <h1>Welcome Dashboard</h1>
  </div>
</template>

<script>

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

    }
  };
</script>