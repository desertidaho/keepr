<template>
  <div class="row mx-0 px-0">
    <nav class="navbar navbar-light">
      <div class="col-3 d-flex justify-content-start">
        <a class="navbar-brand" href="#!">
          <router-link class="" to="/home"><img src="@/assets/k.jpg" alt="K logo" class="logo">
          </router-link>
        </a>
      </div>
      <div class="col-6 d-flex justify-content-center" v-if="!atLogin">
        <form class="form-inline" @submit.prevent="search(searchQuery)">
          <input class="form-control mr-sm-2" type="text" placeholder="Search" v-model="searchQuery">
          <button class="btn btn-outline-warning my-2 my-sm-0 search" type="submit">Search</button>
        </form>
        <button v-if="searched.length > 0" class="btn btn-outline-warning my-2 my-sm-0 search ml-2"
          @click="clearSearch">Clear
          Search</button>
      </div>
      <div class="col-3 d-flex justify-content-end" v-if="!activeUser.active && !atLogin">
        <button class="btn btn-outline-warning my-2 my-sm-0 in" type="submit" v-if="" @click="signIn">Sign In</button>
        <button class="btn btn-outline-warning my-2 my-sm-0 ml-2 in" type="submit" v-if=""
          @click="signIn">Register</button>
      </div>
      <div class="col-3 d-flex justify-content-end" v-if="!activeUser.active && !atHome">
        <button class="btn btn-outline-warning my-2 my-sm-0 browse" @click="browse">Browse as
          guest</button>
      </div>
      <div class="col-3 d-flex justify-content-end" v-if="activeUser.active">
        <button v-if="atHome" class="btn btn-outline-warning my-2 my-sm-0 out" @click="dashboard">Dashboard</button>
        <button v-if="atDashboard" class="btn btn-outline-warning my-2 my-sm-0 out" @click="home">Home</button>
        <button class="btn btn-outline-warning my-2 my-sm-0 out ml-2" @click="logout">Log out</button>
      </div>
    </nav>
  </div>
</template>

<script>
  export default {
    name: "Navbar",
    mounted() {
      if (this.$route.name == 'home') {
        this.atHome = true
      }
      if (this.$route.name == 'login') {
        this.atLogin = true
      }
      if (this.$route.name == 'dashboard') {
        this.atDashboard = true
      }
    },
    props: [],
    data() {
      return {
        atHome: false,
        atLogin: false,
        atDashboard: false,
        searchQuery: ''
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
    methods: {
      logout() {
        this.atHome = false
        this.$store.dispatch('logout')
      },
      signIn() {
        this.atHome = false
        this.$router.push({ name: 'login' })
      },
      browse() {
        this.$router.push({ name: 'home' })
      },
      dashboard() {
        this.$router.push({ name: 'dashboard' })
      },
      home() {
        this.$router.push({ name: 'home' })
      },
      search(searchQuery) {
        let keeps = this.keeps
        let searchArr = []
        let query = searchQuery.toLowerCase()
        for (let i = 0; i < keeps.length; i++) {
          let keep = keeps[i]
          for (let key in keep) {
            if (keep.hasOwnProperty(key)) {
              if (keep[key].toString().toLowerCase().includes(query)) {
                if (!searchArr.includes(keep)) {
                  searchArr.push(keep)
                }
              }
            }
          }
        }
        this.$store.dispatch('searchFor', searchArr)
        this.$router.push({ name: 'home' })
      },
      clearSearch() {
        this.searchQuery = ''
        this.$store.dispatch("clearSearch")
      }

    },
    components: {}
  };
</script>

<style scoped>
  nav {
    width: 100vw;
  }

  .navbar {
    background-color: black;
  }

  .logo {
    height: 3rem;
    width: 3rem;
  }

  .form-control {
    width: 20vw;
  }

  input[type="text"].form-control::-webkit-input-placeholder {
    color: #ff7300;
    font-weight: 500;
  }

  .browse,
  .in,
  .out,
  .search {
    color: #ff7300;
  }
</style>