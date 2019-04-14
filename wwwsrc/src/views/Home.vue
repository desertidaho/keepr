<template>
  <div class="home">
    <navbar />
    <div class="row mt-4">
      <!-- keep cards -->
      <div class="col-12" v-for="keep in keeps">
        <div class="card d-flex flex-row mb-3 shadow">
          <img class="card-img-side" :src="keep.img" alt="Card image cap">
          <div class="card-body text-left">
            <h6 class="card-title">{{keep.name}}</h6>
            <p class="card-text">
              {{keep.description}}
            </p>
            <p class="card-text">
              {{keep.isPrivate}}
            </p>
            <a href="#!" class="btn btn-sm btn-dark shadow">Add To Vault</a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import Navbar from '@/components/Navbar.vue'

  export default {
    name: "home",
    mounted() {
      this.$store.dispatch('getKeeps')
    },
    data() {
      return {

      };
    },
    computed: {
      activeUser() {
        return this.$store.state.user
      },
      keeps() {
        return this.$store.state.keeps
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