<template>
  <div class="home">
    <navbar />
    <div class="container-fluid">
      <div class="row mt-4">
        <!-- keep cards -->
        <div v-if="searched.length == 0" class="col-12 col-md-2" v-for="keep in keeps">
          <div class="card mb-3 shadow">
            <a @click="setViewKeep(keep.id)"><img data-toggle="modal" data-target="#view-keep" class="card-img-top"
                :src="keep.img" alt="Card image cap"></a>
            <div class="card-body text-left pl-3 pr-2">
              <a @click="setViewKeep(keep.id)">
                <h6 class="card-title" data-toggle="modal" data-target="#view-keep">{{keep.name}}</h6>
                <p class="card-text mb-2" data-toggle="modal" data-target="#view-keep">
                  {{keep.description}}
                </p>
              </a>
              <i class="far fa-eye"></i><span class="ml-3">{{keep.views}}</span><a
                href="https://www.facebook.com/sharer/sharer.php?u=brettkeepr.herokuapp.com" target="_blank"><i
                  @click="increaseShares(keep)" class="fab fa-facebook facebook"></i></a> <a
                href="http://www.twitter.com/share?u=brettkeepr.herokuapp.com" target="_blank"><i
                  @click="increaseShares(keep)" class="fab fa-twitter twitter ml-3"></i></a>
              <br>
              <i class=" fas fa-share mt-2"></i><span class="num-shares">{{keep.shares}}</span> <br>
              <i class="fas fa-file-download mt-2"></i><span class="num-keeps">{{keep.keeps}}</span><br>
              <div class="text-center">
                <div v-if="activeUser.id" class="dropdown mt-2">
                  <button class="btn btn-sm btn-outline-secondary dropdown-toggle add-to-vault shadow ml-1"
                    type="button" data-toggle="dropdown">Add
                    To Vault
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
                <button v-if="activeUser.id" @click="addToVault(viewKeep.id)"
                  class="btn btn-sm btn-outline-secondary shadow add-to-vault ml-3">Add To
                  Vault</button>
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
    </div>
  </div>
</template>

<script>
  import Navbar from '@/components/Navbar.vue'
  export default {
    name: "home",
    beforeMount() {
      if (this.activeUser.id) {
        this.getMyVaults()
      }
    },
    mounted() {
      this.$store.dispatch('getKeeps')
      this.$store.dispatch('clearKeepsInAVault')
    },
    data() {
      return {
        showModal: false,
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
      searched() {
        return this.$store.state.search
      },
      vaults() {
        return this.$store.state.vaults
      },
      vaultkeeps() {
        return this.$store.state.vaultkeeps
      }
    },
    methods: {
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
        } else {
          this.viewKeep.keeps = data.keeps++
          this.updateViews(data)
        }
      },
      updateViews(keep) {
        this.$store.dispatch('updateViews', keep)
      },
      addKeepToVault(keep, vault) {
        let payload = {}
        payload.vaultId = vault.id
        payload.keepId = keep.id
        payload.userId = vault.userId
        this.$store.dispatch('addKeepToVault', payload)
        this.setViewKeep(keep)
      },
      getMyVaults() {
        let userId = this.activeUser.id
        this.$store.dispatch('getMyVaults', userId)
      },
      increaseShares(data) {
        this.viewKeep.shares = data.shares++
        this.updateViews(data)
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
    cursor: pointer;
  }

  .card-body {
    background-image: linear-gradient(to bottom, rgb(235, 232, 232) 2%, white);
    border-top-left-radius: 2px;
    border-bottom-right-radius: 2px;
    border-bottom-left-radius: 20px;
  }

  .card-title {
    cursor: pointer;
  }

  .card-text {
    font-size: 0.7rem;
    cursor: pointer;
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

  .facebook {
    margin-left: 4.5vw;
  }

  .add-to-vault {
    position: absolute;
    bottom: 10px;
    font-size: 0.6rem;
  }

  .modal-content {
    border-bottom-left-radius: 18px;
    border-top-right-radius: 18px;
    border: 4px solid white;
  }

  .modal-header {
    background-color: #ff7300;
    border-top-right-radius: 18px;
    color: black;
  }

  .view-keep-desc {
    font-size: 0.9rem;
  }

  .view-keep-img {
    max-height: 15rem;
    min-height: 15rem;
    object-fit: contain;
  }

  .modal-shares,
  .modal-views,
  .modal-keeps {
    margin-left: -4.5rem;
  }

  .modal-footer {
    background-color: #ffc400;
    border-bottom-left-radius: 18px;
  }

  .modal-dropdown {
    font-size: 0.7rem;
  }

  @media (max-width:768px) {

    .facebook,
    .twitter {
      margin-left: 38vw;
    }

    .card-img-top {
      max-height: 14rem;
      min-height: 14rem;
    }
  }
</style>