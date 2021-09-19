import Vue from 'vue'
import Vuex from 'vuex'
import decode from 'jwt-decode'
import router from '../router'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        // estado inicial del token
        token: null,
        // estado inicial del usuario
        usuario: null
  },
    mutations: {
        // son los metodos que modifican el estate del vuex
        setToken(state,token) {
            state.token = token
        },
        setUsuario(state, usuario) {
            state.usuario = usuario
        }
  },
    actions: {
        // las acciones modifican las mutaciones
        guardarToken({ commit},token) {
            commit("setToken", token)
            commit("setUsuario", decode(token))
            localStorage.setItem("token",token)
        },
        autoLogin({ commit }) {
            // obtenemos el token del localstorage
            let token = localStorage.getItem("token")
            if (token) {
                commit("setToken", token)
                commit("setUsuario", decode(token))
            }
            router.push({name: 'home'})
        },
        salir({ commit}) {
            commit("setToken", null)
            commit("setUsuario", null)
            localStorage.removeItem("token");
            router.push({name:'login'})
        }

  },
  modules: {
  }
})
