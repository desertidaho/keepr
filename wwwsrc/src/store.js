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

    //#region -- KEEP STUFF --

    //get all public keeps for home view
    getKeeps({ commit, dispatch }) {
      api.get('Keep')
        .then(res => {
          console.log(res)
          commit('getAllKeeps', res.data)
        })
        .catch(e => {
          console.log(e)
          console.log('Failed to create new keep.')
        })
    },

    //get all private/public keeps for a user dashboard
    getMyKeeps({ commit, dispatch }, userId) {
      api.get(`Keep/${userId}`, userId)
        .then(res => {
          console.log(res)
          commit('getAllKeeps', res.data)
        })
        .catch(e => {
          console.log(e)
          console.log('Failed to create new keep.')
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
  }
})
