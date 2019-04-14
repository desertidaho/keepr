<template>
  <div class="home">
    <navbar />
    <div class="container-fluid">
      <div class="row mt-4">
        <!-- keep cards -->
        <div v-if="searched.length == 0" class="col-2" v-for="keep in keeps">
          <div class="card mb-3 shadow">
            <img class="card-img-top" :src="keep.img" alt="Card image cap">
            <div class="card-body text-left">
              <h6 class="card-title">{{keep.name}}</h6>
              <p class="card-text">
                {{keep.description}}
              </p>
              <i class="far fa-eye"></i><span class="ml-3">{{keep.views}}</span><br>
              <i class="fas fa-share"></i><span class="num-shares">{{keep.shares}}</span><br>
              <i class="fas fa-file-download"></i><span class="num-keeps">{{keep.keeps}}</span><br>
              <div class="text-center">
                <button v-if="activeUser.id" class="btn btn-sm btn-outline-secondary shadow add-to-vault ml-3">Add To
                  Vault</button>
              </div>
            </div>
          </div>
        </div>
        <!-- search results -->
        <div v-if="searched.length > 0" class="col-2" v-for="search in searched">
          <div class="card mb-3 shadow">
            <img class="card-img-top" :src="search.img" alt="Card image cap">
            <div class="card-body text-left">
              <h6 class="card-title">{{search.name}}</h6>
              <p class="card-text">
                {{search.description}}
              </p>
              <i class="far fa-eye"></i><span class="ml-3">{{search.views}}</span><br>
              <i class="fas fa-share"></i><span class="num-shares">{{search.shares}}</span><br>
              <i class="fas fa-file-download"></i><span class="num-keeps">{{search.keeps}}</span><br>
              <div class="text-center">
                <button v-if="activeUser.id" class="btn btn-sm btn-outline-secondary shadow add-to-vault ml-3">Add To
                  Vault</button>
              </div>
            </div>
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
      },
      searched() {
        return this.$store.state.search
      }
    },
    components: {
      Navbar
    }
  };
</script>

<style scoped>
  .home {
    min-height: 100vh;
  }

  .card {
    border: 4px solid white;
    border-bottom-left-radius: 20px;
    border-top-right-radius: 20px;
  }

  .card-img-top {
    border-top-left-radius: 2px;
    border-top-right-radius: 18px;
    max-height: 9rem;
    min-height: 9rem;
    object-fit: cover;
  }

  .card-body {
    background-image: linear-gradient(to bottom, rgb(235, 232, 232) 2%, white);
    border-top-left-radius: 2px;
    border-bottom-right-radius: 2px;
    border-bottom-left-radius: 20px;
  }

  .card-text {
    font-size: 0.7rem;
  }

  .fa-eye {
    color: rgb(65, 32, 32);
  }

  .fa-share {
    color: blue;
  }

  .fa-file-download {
    color: green;
    margin-left: 0.15rem;
  }

  .num-shares {
    margin-left: 1.1rem;
  }

  .num-keeps {
    margin-left: 1.21rem;
  }

  .trash {
    margin-left: 0.1rem;
  }

  .add-to-vault {
    position: absolute;
    bottom: 10px;
    font-size: 0.6rem;
  }
</style>