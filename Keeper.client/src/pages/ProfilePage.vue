<template>

<div class="row" >
  <div class="col-md-2 offset-1">
    <img height="150" :src="profile.picture" alt="">
  </div>
  <div class="col-md-3">
    <h2>{{profile.name}}</h2>
    <h2>Vaults:</h2>
    <h2>Keeps:</h2>
    </div>
</div>

<div class="row">
  <div class="col"> <h3>VAULTS:</h3> 
    <router-link :to="{name: 'Vault'}">
      <div class="col-3" v-for="v in vault">
        <img class="m-1" height="75" :src="v.img" alt="">
      </div>
    </router-link>
  </div>
</div>

<div class="row">
  <div><h3>KEEPS: </h3>
    <div class="col-3" v-for="k in keep"> 
      <img class="m-1" height="75" :src="k.img" alt="">
    </div>
  </div>
</div>







</template>
<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { profilesService } from "../services/ProfilesService";
import { vaultsService } from "../services/VaultsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";

export default {
setup() {
  const route = useRoute();

async function getProfile(){
  try {
    await profilesService.getProfile(route.params.id);
  } catch (error) {
    logger.error(error)
    Pop.toast(error.message, 'error')
  }
}
async function getKeepsByProfile(){
  try {
    await keepsService.getKeepsByProfile(route.params.id);
  } catch (error) {
    logger.error(error)
    Pop.toast(error.message, 'error')
  }}
async function getVaultsByProfile(){
  try {
    await vaultsService.getVaultsByProfile(route.params.id);
  } catch (error) {
    logger.error(error)
    Pop.toast(error.message, 'error')
  }

}
onMounted(()=> {
  getProfile()
  getKeepsByProfile()
  getVaultsByProfile()
})
  return {
    vault: computed(()=> AppState.profileVaults),
    keep: computed(()=> AppState.profileKeeps),
    profile: computed(()=> AppState.currentProfile),

route,
  };
},
};
</script>
<style lang="scss" scoped>


</style>
