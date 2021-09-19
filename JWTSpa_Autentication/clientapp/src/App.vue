<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <div class="d-flex align-center">
        <v-img
          alt="Vuetify Logo"
          class="shrink mr-2"
          contain
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-logo-dark.png"
          transition="scale-transition"
          width="40"
        />

        <v-img
          alt="Vuetify Name"
          class="shrink mt-1 hidden-sm-and-down"
          contain
          min-width="100"
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-name-dark.png"
          width="100"
        />
      </div>

      <v-spacer></v-spacer>

      <v-btn
        href="https://github.com/vuetifyjs/vuetify/releases/latest"
        target="_blank"
        text
      >
        <span class="mr-2">Componentes</span>
        <v-icon>open_in_new</v-icon>
      </v-btn>
    </v-app-bar>

    <v-main>
      <router-view />
    </v-main>
    <template>
  <v-footer padless color="primary">
    <v-col class="text-center" cols="12">
      {{ new Date().getFullYear() }} — <strong>Vue and NetCore 3.1</strong>
    </v-col>
  </v-footer>
</template>

  </v-app>
</template>

<script>
// import HelloWorld from './components/HelloWorld';

export default {
  name: "App",

  components: {},

  data: () => ({
    //
  })
};
</script>
