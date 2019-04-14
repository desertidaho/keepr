<template>
  <div class="dashboard">
    <navbar />
    <div class="row mt-4">
      <div class="col-5">
        <div class="col-12 mt-3">
          <button class="btn btn-lg btn-outline-dark shadow mb-4" data-toggle="modal" data-target="#create-keep">Create
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
              <p v-if="keep.isPrivate" class="card-text">Private</p>
              <p v-if="!keep.isPrivate" class="card-text">Public</p>
              <i class="far fa-eye"></i><span class="ml-3">{{keep.views}}</span><br>
              <i class="fas fa-share"></i><span class="num-shares">{{keep.shares}}</span><br>
              <i class="fas fa-file-download"></i><span class="num-keeps">{{keep.keeps}}</span><br>
              <a href="" @click="deleteKeep(keep.id)"><i class="fas fa-trash text-danger mt-2 trash"></i></a>
              <div class="text-center">
                <a href="#!" class="btn btn-sm btn-outline-secondary shadow add-to-vault ml-3">Add To Vault</a>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-7">
        <div class="col-12 mt-3">
          <button class="btn btn-lg btn-outline-dark shadow mb-4" data-toggle="modal" data-target="#create-vault">Create
            Vault</button>
        </div>
        <!-- vault cards -->
        <div class="col-12" v-for="vault in vaults">
          <div class="card d-flex flex-row mb-3 shadow">
            <div class="card-body text-left cbv">
              <h6 class="card-title">{{vault.name}}</h6>
              <p class="card-text">
                {{vault.description}}</p>
              <a href="" @click="deleteVault(vault.id)"><i class="fas fa-trash text-danger mt-2 trash"></i></a>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Create Keep modal -->
    <div class="modal fade" id="create-keep" tabindex="-1" role="dialog" aria-labelledby="exampleModal3Label"
      aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header mhk">
            <h5 class="modal-title text-light" id="exampleModal3Label">Make a Keep</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body mbk">
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
          <div class="modal-footer mfk">
            <button type="button" class="btn btn-outline-secondary shadow" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-outline-dark shadow" @click="newKeepPost"
              data-dismiss="modal">Submit</button>
          </div>
        </div>
      </div>
    </div>
    <!-- Create Vault modal -->
    <div class="modal fade" id="create-vault" tabindex="-1" role="dialog" aria-labelledby="exampleModal3Label"
      aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header mhv">
            <h5 class="modal-title text-light" id="exampleModal3Label">Make a Vault</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body mbv">
            <form>
              <div class="form-group">
                <input v-model="newVault.name" type="text" class="form-control" id="formGroupExampleInput"
                  placeholder="Name of new Vault...">
              </div>
              <div class="form-group">
                <input v-model="newVault.description" type="text" class="form-control" id="formGroupExampleInput2"
                  placeholder="Description...">
              </div>
            </form>
          </div>
          <div class="modal-footer mfv">
            <button type="button" class="btn btn-outline-secondary shadow" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-outline-dark shadow" @click="createNewVault"
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
      this.getMyVaults()
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
        },
        newVault: {
          name: "",
          description: "",
          userId: "",
        }
      };
    },
    computed: {
      activeUser() {
        return this.$store.state.user
      },
      keeps() {
        return this.$store.state.keeps
      },
      vaults() {
        return this.$store.state.vaults
      }
    },
    methods: {
      newKeepPost() {
        this.newKeep.userId = this.activeUser.id
        let newK = this.newKeep

        this.$store.dispatch('newKeep', newK)
      },
      getMyKeeps() {
        let userId = this.activeUser.id
        this.$store.dispatch('getMyKeeps', userId)
      },
      deleteKeep(id) {
        this.$store.dispatch('deleteKeep', id)
        this.$router.push({ name: 'dashboard' })
      },
      createNewVault() {
        this.newVault.userId = this.activeUser.id
        let newV = this.newVault
        this.$store.dispatch('newVault', newV)
      },
      getMyVaults() {
        let userId = this.activeUser.id
        this.$store.dispatch('getMyVaults', userId)
      },
      deleteVault(id) {
        this.$store.dispatch('deleteVault', id)
        this.$router.push({ name: 'dashboard' })
      }
    },
    components: {
      Navbar
    }
  };
</script>

<style scoped>
  .dashboard {
    min-height: 100vh;
  }

  .card,
  .modal-content {
    border: 4px solid white;
    border-bottom-left-radius: 20px;
    border-top-right-radius: 20px;
  }

  .card-body {
    background-image: linear-gradient(to right, rgb(235, 232, 232) 2%, white);
    border-top-right-radius: 6px;
    border-bottom-right-radius: 6px;
  }

  .cbv {
    border-bottom-left-radius: 18px;
  }

  .card-img-side {
    border-top-left-radius: 2px;
    border-bottom-left-radius: 18px;
    max-width: 60%;
    min-width: 60%;
    object-fit: cover;
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
    font-size: 0.7rem;
  }

  .mhk,
  .mfv {
    background-color: #ff7300;
  }

  .mbk {
    background-image: linear-gradient(#ff7300 30%, #ffc400);
  }

  .mfk,
  .mhv {
    background-color: #ffc400;
  }

  .mbv {
    background-image: linear-gradient(#ffc400 30%, #ff7300);
  }

  .modal-header {
    border-top-right-radius: 18px;
  }

  .modal-footer {
    border-bottom-left-radius: 18px;
  }
</style>