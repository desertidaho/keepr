<template>
  <div class="home">
    <navbar />
    <div class="container-fluid">
      <div class="row mt-4">
        <!-- keep cards -->
        <div v-if="searched.length == 0" class="col-2" v-for="keep in keeps">
          <div class="card mb-3 shadow">
            <a @click="setViewKeep(keep.id)"><img data-toggle="modal" data-target="#view-keep" class="card-img-top"
                :src="keep.img" alt="Card image cap"></a>
            <div class="card-body text-left px-3">
              <h5 class="card-title">{{keep.name}}</h5>
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
                <img class="card-img-top view-keep-img" :src="viewKeep.img" alt="Card image cap">
              </div>
              <div class="modal-footer mfv d-flex justify-content-center">
                <i class="far fa-eye"></i><span class="ml-2">{{viewKeep.views}}</span>
                <i class="fas fa-share ml-5"></i><span class="num-shares ml-2">{{viewKeep.shares}}</span>
                <i class="fas fa-file-download ml-5"></i><span class="num-keeps ml-2">{{viewKeep.keeps}}</span>
                <div class="py-4">
                  <button v-if="activeUser.id" @click="addToVault(viewKeep.id)"
                    class="btn btn-sm btn-secondary shadow add-to-vault ml-5 mb-3">Add To
                    Vault</button>
                </div>
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
      }
    },
    methods: {
      setViewKeep(id) {
        let keep = this.keeps.find(k => k.id == id);
        this.viewKeep.name = keep.name
        this.viewKeep.description = keep.description
        this.viewKeep.img = keep.img
        this.viewKeep.views = keep.views
        this.viewKeep.shares = keep.shares
        this.viewKeep.keeps = keep.keeps
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

  .card-text {
    font-size: 0.8rem;
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

  .modal-content {
    border-bottom-left-radius: 18px;
    border-top-right-radius: 18px;
    border: 4px solid white;
  }

  .modal-header {
    background-color: #ff7300;
    border-top-right-radius: 18px;
  }

  .view-keep-desc {
    font-size: 1rem;
  }

  .view-keep-img {
    max-height: 15rem;
    min-height: 15rem;
    object-fit: contain;
  }

  .modal-footer {
    background-color: #ffc400;
    border-bottom-left-radius: 18px;
  }
</style>