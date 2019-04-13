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
    newKeep({ commit, dispatch }, newKeep) {
      api.post('create', newKeep)
        .then(res => {
          commit('addNewKeep', newKeep)
          router.push({ name: 'dashboard' })
        })
        .catch(e => {
          console.log('Failed to create new keep.')
        })
    }

    //#endregion
  }
})
