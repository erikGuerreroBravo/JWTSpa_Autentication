import Vue from 'vue'
import App from './App.vue'
import store from './store'
import vuetify from './plugins/vuetify'
import router from './router'
import axios from 'axios'

Vue.config.productionTip = false
axios.defaults.baseURL='http://localhost:50598/'
new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
