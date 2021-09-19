<template>
   <v-container grid-list-md text-xs-center>
        <v-row justify="center">
            <v-col xs="12" sm="8" md="7" lg="7" >
                <v-card>
                    <v-toolbar dark color="blue darken-3">
                    Login de Acceso
                    </v-toolbar>
                    <v-card-text>
                    <v-text-field v-model="email" autofocus color="accent" label="Email" required>
                    </v-text-field>
                    <v-text-field v-model="password" type="password" color="accent" label="Password" required>
                    </v-text-field>
                    <v-row v-if="error" class="red--text">
                        <v-col align-self="center">
                          {{ error }}

                        </v-col>
                    </v-row>
                    </v-card-text>
                    <v-card-actions class="px-3 pb-3">
                     <v-row>
                         <v-col  align-self="center">
                             <v-btn color="primary" @click="ingresar">Ingresar</v-btn>
                         </v-col>
                     </v-row>
                    </v-card-actions>
                </v-card>
            </v-col>
        </v-row>
    </v-container>

   
</template>
<script>
import axios from'axios'

export default {
    data(){
        return {
            email:'',
            password:'',
            error:null
        }
    },
    methods:{
        ingresar(){
            this.error = null;
            axios.post('api/Auth/login',{email:this.email, password:this.password})
            .then(respuesta=>{
                return respuesta.data
            }).then(data=>{
                this.$store.dispatch("guardar token", data.token)
                this.$router.push({name: 'home'})
            }).catch(err=> {
                
                if(err.response.status==400){
                  this.error = "El nombre del usuario no es un email valido";
                }
                else if(err.response.status==404)
                {
                    this.error ="No existe el usuario o sus datos son invalidos";
                }
                else{
                    this.error="Ocurrio un error";
                }
            })
        }
    },
}
</script>