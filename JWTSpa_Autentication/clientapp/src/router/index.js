import Vue from 'vue'
import VueRouter from 'vue-router'
import Generos from '../components/Generos.vue'
import Rol from '../components/Rol.vue'
import Login from '../components/Login.vue'
import store from '../store'

Vue.use(VueRouter)

const routes = [
    {
        path: '/generos',
        name: 'generos',
        component: Generos,
         meta: {
             invitado: true
         }
       // meta: {
         //   administrador: true, vendedor: true
       // }
    },
     {
        path: '/login',
        name: 'login',
        component: Login,
         meta: {
             invitado: true
         }
    },
      {
        path: '/roles',
        name: 'roles',
        component: Rol,
        meta: {
             invitado: true
         }
    }
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

router.beforeEach((to, from, next) => {
    if (to.matched.some(record => record.meta.invitado)) {
        next() // nos deja entrar al login
    }
    // validamos que venga un usuario y un rol
    else if (store.state.usuario && store.state.usuario.rol == 'administrador') {
        if (to.matched.some(record => record.meta.administrador)) {
            next()
        }
    }
    else if (store.state.usuario && store.state.usuario.rol == 'vendedor') {
        if (to.matched.some(record => record.meta.vendedor)) {
            next()
        }
    }
    else {
        next({
            name:'login'
        })
    }
 })

export default router