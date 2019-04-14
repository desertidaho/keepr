<template>
  <div class="dashboard">
    <navbar />
    <div class="row mt-4">
      <div class="col-4">
        <div class="col-12">
          <h4 class="text-light">Keeps</h4>
        </div>
        <div class="col-12">
          <button class="btn btn-outline-light shadow mb-4" data-toggle="modal" data-target="#create-keep">Create
            Keep</button>
        </div>
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

      <div class="col-8">
        <div class="col-12">
          <h4 class="text-light">Vaults</h4>
        </div>
      </div>
    </div>
    <!-- Create Keep modal -->
    <div class="modal fade" id="create-keep" tabindex="-1" role="dialog" aria-labelledby="exampleModal3Label"
      aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title text-light" id="exampleModal3Label">Make a Keep</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body">
            <form>
              <div class="form-group">
                <input v-model="newKeep.name" type="text" class="form-control" id="formGroupExampleInput"
                  placeholder="Name of new Keep...">
              </div>
              <div class="form-group">
                <input v-model="newKeep.description" type="text" class="form-control" id="formGroupExampleInput2"
                  placeholder="Description...">
              </div>
              <div class="form-group">
                <input v-model="newKeep.img" type="text" class="form-control" id="formGroupExampleInput"
                  placeholder="Image UrL...">
              </div>
              <div class="form-group d-flex justify-content-start ml-4">
                <input v-model="newKeep.isPrivate" class="form-check-input" type="checkbox" value="" id="isPrivate">
                <label class="form-check-label" for="isPrivate">
                  Private (If checked only you can see.)
                </label>
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-outline-secondary shadow" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-outline-dark shadow" @click="newKeepPost"
              data-dismiss="modal">Submit</button>
          </div>
        </div>
      </div>
    </div>

  </div>
</template>

<script>
  import Navbar from '@/components/Navbar.vue'

  export default {
    name: "dashboard",
    beforeMount() {
      this.getMyKeeps()
    },
    mounted() {
      //blocks users not logged in
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }

    },
    data() {
      return {
        newKeep: {
          name: "",
          description: "",
          img: "",
          isPrivate: false,
          userId: "",
          keeps: 0,
          shares: 0,
          views: 0
        }

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
    methods: {
      newKeepPost() {
        this.newKeep.userId = this.activeUser.id
        let newK = this.newKeep

        this.$store.dispatch('newKeep', newK)
        // this.newKeep.name = ''
        // this.newKeep.description = ''
        // this.newKeep.img = ''
        // this.newKeep.isPrivate = false
      },
      getMyKeeps() {
        let userId = this.activeUser.id
        this.$store.dispatch('getMyKeeps', userId)
      }
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

  .card,
  .modal-content {
    border: 5px solid white;
    border-radius: 10px;
  }

  .card-body {
    background-image: linear-gradient(to right, rgb(224, 223, 223) 30%, white);
    border-top-right-radius: 6px;
    border-bottom-right-radius: 6px;
  }

  .card-img-side {
    border-top-left-radius: 6px;
    border-bottom-left-radius: 6px;
    max-width: 50%;
    object-fit: cover;
  }

  .card-text {
    font-size: 0.7rem;
  }

  .modal-header {
    background-color: #ff7300;
  }

  .modal-body {
    background-image: linear-gradient(#ff7300 30%, #ffc400);
  }

  .modal-footer {
    background-color: #ffc400;
  }
</style>