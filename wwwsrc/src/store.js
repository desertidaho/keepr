import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'

Vue.use(Vuex)

let baseUrl = location.host.includes('localhost') ? '//localhost:5000/' : '/'

let auth = Axios.create({
  baseURL: baseUrl + "account/",
  timeout: 3000,
  withCredentials: true
})

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    keeps: [],
    search: [],
    vaults: [],
    vaultkeeps: []
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    addNewKeep(state, newKeep) {
      state.keeps.unshift(newKeep)
    },
    getAllKeeps(state, data) {
      state.keeps = data
    },
    searchFor(state, searchArr) {
      state.search = searchArr
    },
    addNewVault(state, newVault) {
      state.vaults.unshift(newVault)
    },
    getAllVaults(state, data) {
      state.vaults = data
    },
    addNewVaultKeeps(state, newVaultKeeps) {
      state.vaultkeeps.unshift(newVaultKeeps)
    },
    getAllVaultKeeps(state, data) {
      state.vaultkeeps = data
    }
  },
  actions: {

    //#region -- AUTH STUFF --

    register({ commit, dispatch }, newUser) {
      auth.post('register', newUser)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('[registration failed] :', e)
        })
    },
    authenticate({ commit, dispatch }) {
      auth.get('authenticate')
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('not authenticated')
        })
    },
    login({ commit, dispatch }, creds) {
      auth.post('login', creds)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('Login Failed')
        })
    },

    logout({ commit, dispatch }) {
      auth.delete('logout')
        .then(res => {
          commit('setUser', {})
          router.push({ name: 'login' })
        })
        .catch(e => {
          console.log('Logout Failed')
        })
    },

    //#endregion

    //#region -- KEEPS --

    //get all public keeps for home view
    getKeeps({ commit, dispatch }) {
      api.get('Keep')
        .then(res => {
          commit('getAllKeeps', res.data)
        })
        .catch(e => {
          console.log(e)
          console.log('Failed to get keeps.')
        })
    },

    //get all private/public keeps for a user dashboard
    getMyKeeps({ commit, dispatch }, userId) {
      api.get(`Keep/${userId}`, userId)
        .then(res => {
          commit('getAllKeeps', res.data)
        })
        .catch(e => {
          console.log(e)
          console.log('Failed to get keeps for a user.')
        })
    },

    //create a new keep
    newKeep({ commit, dispatch }, newKeep) {
      api.post('Keep', newKeep)
        .then(res => {
          console.log(res)
          commit('addNewKeep', res.data)
          router.push({ name: 'dashboard' })
        })
        .catch(e => {
          console.log(e)
          console.log('Failed to create new keep.')
        })
    },

    //edit a keep by incrementing number of views
    updateViews({ commit, dispatch }, editedKeep) {
      let id = editedKeep.id
      api.put(`Keep/${id}`, editedKeep)
        .then(res => {
          dispatch('getKeeps')
        })
        .catch(e => {
          console.log(e)
          console.log('Failed to edit keep.')
        })
    },

    //delete a keep from user dashboard
    deleteKeep({ commit, dispatch }, id) {
      api.delete(`Keep/${id}`, id)
        .then(res => {
          console.log(res)
          commit('getAllKeeps', res.data)
        })
        .catch(e => {
          console.log(e)
          console.log('Unable to delete selected keep.')
        })
    },

    //#endregion

    //#region -- VAULTS --

    //get all private/public vaults for a user dashboard
    getMyVaults({ commit, dispatch }, userId) {
      api.get(`Vault/${userId}`, userId)
        .then(res => {
          console.log(res)
          commit('getAllVaults', res.data)
        })
        .catch(e => {
          console.log(e)
          console.log('Failed to get vaults.')
        })
    },

    //create a new vault
    newVault({ commit, dispatch }, newVault) {
      api.post('Vault', newVault)
        .then(res => {
          console.log(res)
          commit('addNewVault', res.data)
          router.push({ name: 'dashboard' })
        })
        .catch(e => {
          console.log(e)
          console.log('Failed to create new vault.')
        })
    },

    //delete a vault from user dashboard
    deleteVault({ commit, dispatch }, id) {
      api.delete(`Vault/${id}`, id)
        .then(res => {
          console.log(res)
          commit('getAllVaults', res.data)
        })
        .catch(e => {
          console.log(e)
          console.log('Unable to delete selected vault.')
        })
    },

    //#endregion

    //#region -- VAULTKEEPS --

    //create a new vault
    addKeepToVault({ commit, dispatch }, payload) {
      api.post('VaultKeeps', payload)
        .then(res => {
          console.log(res)
          commit('addNewVaultKeeps', payload)
        })
        .catch(e => {
          console.log(e)
          console.log('Failed to create new vault.')
        })
    },

    //get all private/public vaults for a user dashboard
    getMyVaultKeeps({ commit, dispatch }, userId) {
      api.get(`VaultKeeps/${userId}`, userId)
        .then(res => {
          console.log(res.data)
          commit('getAllVaultKeeps', res.data)
        })
        .catch(e => {
          console.log(e)
          console.log('Failed to get vaults.')
        })
    },

    //#endregion

    //#region -- SEARCH --

    // Search

    clearSearch({ commit, dispatch }) {
      commit('searchFor', [])
    },

    searchFor({ commit, dispatch }, searchArr) {
      commit('searchFor', searchArr)
    }

    //#endregion

  }
})
