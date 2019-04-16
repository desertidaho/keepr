<template>
  <div class="dashboard">
    <navbar />
    <div class="row">
      <div class="col-12 col-md-5">
        <div class="col-12 mt-5">
          <button class="btn btn-lg btn-outline-warning shadow-lg mb-4" data-toggle="modal"
            data-target="#create-keep">Create
            Keep</button>
        </div>
        <!-- keep cards -->
        <div class="col-12 ml-md-2" v-for="keep in keeps">
          <div class="card d-flex flex-row mb-3 shadow">
            <img class="card-img-side" :src="keep.img" alt="Card image cap" @click="setViewKeep(keep.id)"
              data-toggle="modal" data-target="#view-keep">
            <div class="card-body text-left px-2 pl-md-4">
              <h6 class="card-title card-keep-title">{{keep.name}} <a href="" @click="deleteKeep(keep.id)"><i
                    class="fas fa-trash text-dark trash-k shadow"></i></a> </h6>
              <p class="card-text card-keep-description">
                {{keep.description}}
              </p>
              <p v-if="keep.isPrivate" class="card-text card-keep-isPrivate">Private</p>
              <p v-if="!keep.isPrivate" class="card-text card-keep-isPrivate">Public</p>
              <i class="far fa-eye card-keep-icon"></i><span class="ml-1 card-keep-icon-nums">{{keep.views}}</span>
              <i class="fas fa-share ml-3 ml-md-4 card-keep-icon"></i><span
                class="num-shares ml-1 card-keep-icon-nums">{{keep.shares}}</span>
              <i class="fas fa-file-download ml-3 ml-md-4 mb-3 card-keep-icon"></i><span
                class="num-keeps ml-1 card-keep-icon-nums">{{keep.keeps}}</span>
              <div class="text-start">
                <a href="https://www.facebook.com/sharer/sharer.php?u=brettkeepr.herokuapp.com" target="_blank"><i
                    @click="increaseShares(keep)" class="fab fa-facebook facebook mt-2"></i></a> <a
                  href="http://www.twitter.com/share?u=brettkeepr.herokuapp.com" target="_blank"><i
                    @click="increaseShares(keep)" class="fab fa-twitter twitter ml-3"></i></a><br>
                <button class="btn btn-sm btn-outline-secondary dropdown-toggle add-to-vault shadow" type="button"
                  data-toggle="dropdown">Add To Vault
                  <span class="caret"></span></button>
                <ul class="dropdown-menu">
                  <li v-for="vault in vaults"><a href="" @click="addKeepToVault(keep, vault)"
                      class="text-dark ml-2">{{vault.name}}</a></li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-12 col-md-7">
        <div class="col-12 mt-5">
          <button class="btn btn-lg btn-outline-warning shadow-lg mb-4" data-toggle="modal"
            data-target="#create-vault">Create
            Vault</button>
        </div>
        <!-- vault cards -->
        <div class="col-12" v-for="vault in vaults">
          <div class="card mb-3 shadow">
            <div class="card-body text-left cbv" id="card-vaults">
              <a href="" @click="deleteVault(vault.id)"><i class="fas fa-trash text-dark mt-2 trash-v shadow"></i></a>
              <h6 class="card-title text-light">{{vault.name}}</h6>
              <p class="card-text text-light">
                {{vault.description}}</p>
              <div class="col-12 d-flex flex-row" id="vault-cards">
                <!-- keep cards in vaults -->
                <div v-for="keep in keepsinavault[vault.id]">
                  <div class="card ml-md-2 card-keep-vault shadow">
                    <img class="card-img-top img-in-vault" :src="keep.img" alt="Card image cap">
                    <div class="" id="keep-vault-body">
                      <h6 class="card-title ml-2 mt-2">{{keep.name}} <a href=""
                          @click="deleteKeepFromVault(vault, keep)"><i
                            class="fas fa-trash text-dark mr-2 trash-v shadow"></i></a> </h6>
                      <p class="card-text keep-vault-description mx-2">
                        {{keep.description}}
                      </p>
                      <div class="keep-vault-v-s-k ml-4 mb-2">
                        <i class="far fa-eye"></i><span class="ml-1">{{keep.views}}</span>
                        <i class="fas fa-share ml-3"></i><span class="num-shares ml-1">{{keep.shares}}</span>
                        <i class="fas fa-file-download ml-3"></i><span class="num-keeps ml-1">{{keep.keeps}}</span>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
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
              <div class="form-group text-left">
                <input v-model="newKeep.isPrivate" class="form-check-input mt-2 text-left" type="checkbox" value=""
                  id="isPrivate">
                <label class="form-check-label ml-4" for="isPrivate">
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
    <!-- view keep modal -->
    <div class="modal fade" id="view-keep" tabindex="-1" role="dialog" aria-labelledby="" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h4 class="card-title">{{viewKeep.name}}</h4>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body mbv">
            <p class="card-text ml-3 view-keep-desc">
              {{viewKeep.description}}
            </p>
            <img class="card-img-top view-keep-img mb-2" :src="viewKeep.img" alt="Card image cap">
          </div>
          <div class="modal-footer mfv d-flex justify-content-around">
            <i class="far fa-eye"></i><span class="modal-views">{{viewKeep.views + 1}}</span>
            <i class="fas fa-share"></i><span class="modal-shares">{{viewKeep.shares}}</span>
            <i class="fas fa-file-download ml-4"></i><span class="modal-keeps">{{viewKeep.keeps}}</span>
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
      if (this.$store.state.user.id) {
        this.getMyKeeps()
        this.getMyVaults()
        this.getMyVaultKeeps()
      }
    },
    mounted() {
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }
      let vaults = this.vaults
      for (let i = 0; i < vaults.length; i++) {
        let vault = vaults[i]
        let vaultId = vault.id
        this.getKeepsInAVault(vaultId)
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
        },
        viewKeep: {
          name: '',
          description: '',
          img: '',
          views: '',
          shares: '',
          keeps: ''
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
      },
      vaultkeeps() {
        return this.$store.state.vaultkeeps
      },
      keepsinavault() {
        return this.$store.state.keepsInVaults
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
      },
      addKeepToVault(keep, vault) {
        let payload = {}
        payload.vaultId = vault.id
        payload.keepId = keep.id
        payload.userId = vault.userId
        this.$store.dispatch('addKeepToVault', payload)
        keep.keeps++
        this.$store.dispatch('updateViews', keep)
      },
      getMyVaultKeeps() {
        let userId = this.activeUser.id
        this.$store.dispatch('getMyVaultKeeps', userId)
        this.$router.push({ name: 'dashboard' })
      },
      getKeepsInAVault(vaultId) {
        this.$store.dispatch('getKeepsInVault', vaultId)
        this.$router.push({ name: 'dashboard' })
      },
      deleteKeepFromVault(vault, keep) {
        let vaultId = vault.id
        let keepId = keep.id
        let vaultKeeps = this.vaultkeeps
        for (let i = 0; i < vaultKeeps.length; i++) {
          let vk = vaultKeeps[i]
          if (vk.vaultId == vaultId && vk.keepId == keepId) {
            this.$store.dispatch('deleteKeepFromVault', vk)
            break;
          }
        }
      },
      increaseShares(data) {
        data.shares++
        this.updateViews(data)
      },
      updateViews(keep) {
        this.$store.dispatch('updateViews', keep)
      },
      setViewKeep(data) {
        if (data > 0) {
          let keep = this.keeps.find(k => k.id == data);
          this.viewKeep.name = keep.name
          this.viewKeep.description = keep.description
          this.viewKeep.img = keep.img
          this.viewKeep.views = keep.views++
          this.viewKeep.shares = keep.shares
          this.viewKeep.keeps = keep.keeps
          this.updateViews(keep)
        }
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

  .btn-outline-warning {
    color: #ff7300;
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

  #card-vaults {
    background-image: linear-gradient(to right, #ff7300 30%, #ffc400);
    border-top-right-radius: 18px;
  }

  .cbv {
    border-bottom-left-radius: 18px;
  }

  .card-img-side {
    border-top-left-radius: 2px;
    border-bottom-left-radius: 18px;
    max-width: 60%;
    min-width: 60%;
    min-height: 50vh;
    object-fit: cover;
    cursor: pointer;
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

  .trash-k {
    float: right;
  }

  .trash-v {
    float: right;
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

  #isPrivate {
    margin-left: -6.9vw;
  }

  .card-keep-vault {
    max-width: 11vw;
  }

  #keep-vault-body {
    background-image: linear-gradient(to bottom, rgb(235, 232, 232) 2%, white);
    border-bottom-left-radius: 19px;
  }

  .keep-vault-description {
    font-size: 0.64rem;
  }

  .img-in-vault {
    max-height: 19vh;
    min-height: 19vh;
    object-fit: fill;
    border-top-right-radius: 18px;
  }

  .keep-vault-v-s-k {
    font-size: 0.7rem;
  }

  @media (max-width:768px) {
    .card-img-side {
      max-width: 50%;
      min-width: 50%;
      min-height: 30vh;
      object-fit: cover;
    }

    .card-keep-title {
      font-size: 0.8rem;
    }

    .card-keep-description {
      font-size: 0.6rem;
    }

    .card-keep-isPrivate {
      font-size: 0.6rem;
    }

    .card-keep-icon-nums {
      font-size: 0.7rem;
    }

    .facebook,
    .twitter {
      margin-left: 1vw;
      margin-bottom: 2rem;
    }

    .add-to-vault {
      font-size: 0.7rem;
    }

    #vault-cards {
      display: flex;
      flex-direction: column !important;
    }

    .card-keep-vault {
      min-width: 70vw;
      margin-bottom: 3vh;
    }

    .img-in-vault {
      max-height: 30vh;
      min-height: 30vh;
    }
  }
</style>