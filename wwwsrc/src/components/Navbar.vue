<template>
  <div class="row mx-0 px-0">
    <nav class="navbar navbar-light">
      <div class="col-3 d-flex justify-content-start">
        <a class="navbar-brand" href="#!">
          <img src="@/assets/k.jpg" alt="K logo" class="logo">
        </a>
      </div>
      <div class="col-6 d-flex justify-content-center">
        <form class="form-inline">
          <input class="form-control mr-sm-2" type="text" placeholder="Search">
          <button class="btn btn-outline-warning my-2 my-sm-0 search" type="submit">Search</button>
        </form>
      </div>
      <div class="col-3 d-flex justify-content-end">
        <button class="btn btn-outline-warning my-2 my-sm-0 sr" type="submit">Sign In</button>
        <button class="btn btn-outline-warning my-2 my-sm-0 ml-4 sr" type="submit">Register</button>
      </div>
    </nav>
  </div>
</template>

<script>
export default {
  name: "Navbar",
  props: [],
  data() {
    return {};
  },
  computed: {},
  methods: {},
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
  width: 30vw;
}

input[type="text"].form-control::-webkit-input-placeholder {
  color: #ff7300;
  font-weight: 500;
}

.sr,
.search {
  color: #ff7300;
}
</style>
