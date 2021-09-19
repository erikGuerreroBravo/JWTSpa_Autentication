<template>
  <v-layout align-start>
    <v-flex>
      <v-data-table
        :headers="headers"
        :items="roles"
        :search="buscar"
        sort-by="Nombre"
        class="elevation-1"
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>Roles</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <!--agregamos una caja de texto de busqueda -->
            <v-text-field
              class=""
              v-model="buscar"
              append-icon="search"
              label="Búscar"
              single-line
              hide-details
            ></v-text-field>

            <!-- Este es un espacio-->
            <v-spacer></v-spacer>
            <!--terminamos con el espacio -->

            <!-- Iniciamos el dialog en modo diseño-->
            <v-dialog v-model="dialog" max-width="500px">
              <template v-slot:activator="{ on, attrs }">
                <v-btn
                  color="primary"
                  dark
                  class="mb-2"
                  v-bind="attrs"
                  v-on="on"
                >
                  Nuevo Rol
                </v-btn>
              </template>
              <v-card>
                <v-card-title>
                  <span class="text-h5">{{ formTitle }}</span>
                </v-card-title>

                <v-card-text>
                  <v-container>
                    <v-row>
                      <v-col cols="12" sm="12" md="12">
                        <v-text-field
                          v-model="rol.StrValor"
                          label="Nombre del Rol"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="12" sm="12" md="12">
                        <v-text-field
                          v-model="rol.StrDescripcion"
                          label="Descripción del Rol"
                        ></v-text-field>
                      </v-col>

                    </v-row>
                  </v-container>
                </v-card-text>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text @click="close">
                    Cancelar
                  </v-btn>
                  <v-btn color="blue darken-1" text @click="guardar">
                    Guardar
                  </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
            <!-- Terminamos con el diseño del diaglo-->
            <!-- Iniciamos el dialogo de eliminar-->
            <v-dialog v-model="dialogDelete" max-width="500px">
              <v-card>
                <v-card-title class="text-h5"
                  >Estas seguro de querer borrar este genero?</v-card-title
                >
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text @click="closeDelete"
                    >Cancelar</v-btn
                  >
                  <v-btn color="blue darken-1" text @click="deleteItemConfirm"
                    >OK</v-btn
                  >
                  <v-spacer></v-spacer>
                </v-card-actions>
              </v-card>
            </v-dialog>
            <!--Terminamos con el dialogo  de eliminar -->
          </v-toolbar>
        </template>
        
       <template slot="items" slot-scope="props">
        
        <td class="text-xs-right">{{ props.item.strValor }}</td>
        <td class="text-xs-right">{{ props.item.strDescripcion }}</td>
        <td class="text-xs-right">
            <div v-if="props.item.status.strValor">
              <span class="blue--text">Activo</span>
            </div>
            <div v-else>
                <span class="red--text">Inactivo</span>
            </div>
        </td>

        <td class="justify-center layout px-0">
            <v-icon small class="mr-2" @click="editItem(props.item)">
            edit
            </v-icon>
            <v-icon small @click="deleteItem(props.item)">
            delete
            </v-icon>
        </td>
       </template>
       <template slot="no-data">
       <v-btn color="primary" @click="initialize">Actualizar</v-btn>
       </template>

        
      </v-data-table>
    </v-flex>
  </v-layout>
</template>
<script>
import axios from "axios";

export default {
  data() {
    return {
      
      roles:[],
      buscar: "",
      dialog: false,
      dialogDelete: false,
      headers: [
        { text: "Nombre", value: "strValor" },
        { text: "Descripción", value: "strDescripcion" },
        { text: "Status", value: "status.strValor" },
        { text: "Acciones", value: "acciones", sortable: false }
      ],

      editedIndex: -1,
      editedItem: {
        StrValor:'', 
        StrDescripcion:'', 
        Status:
        {
             StrDescripcion: ''
        }

      },
      
      rol:{
        StrValor:'',
        StrDescripcion:'',
        Status:{
            StrDescripcion: ''
        }

      },
    
    };
  },
  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nuevo Rol" : "Editar Rol";
    }
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
    dialogDelete(val) {
      val || this.closeDelete();
    }
  },

  created() {
    this.listar();
  },
  methods: {
    limpiar() {
      this.rol.StrValor = "";
      this.rol.StrDescripcion = "";
    },
    listar() {
      let me = this;
      axios
        .get("api/rol")
        .then(function(response) {
          me.roles = response.data;
        })
        .catch(function(error) {
          // eslint-disable-next-line no-console
          console.log(error);
        });
    },
    guardar() {
      if (this.editedIndex > -1) {
        //codigo para edicion
        // Object.assign(this.desserts[this.editedIndex], this.editedItem);
      } else {
        //codigo para guardado
        // this.desserts.push(this.editedItem);
        let me = this;
        axios
          .post("api/rol", { StrValor: me.rol.StrValor,StrDescripcion: me.rol.StrDescripcion })
          .then(function() {
            me.close();
            me.listar();
            me.limpiar();
          })
          .catch(function(error) {
            // eslint-disable-next-line no-console
            console.log(error);
          });
      }
    },
    initialize() {
      this.roles = [];
    },

    editItem(item) {
      this.editedIndex = this.roles.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item) {
      this.editedIndex = this.roles.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      this.roles.splice(this.editedIndex, 1);
      this.closeDelete();
    },

    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    save() {
      if (this.editedIndex > -1) {
        Object.assign(this.roles[this.editedIndex], this.editedItem);
      } else {
        this.roles.push(this.editedItem);
      }
      this.close();
    }
  }
};
</script>
