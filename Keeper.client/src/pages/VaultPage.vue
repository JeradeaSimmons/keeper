<template>
  
  <div class="row mb-5 d-flex justify-content-between">
    <div class="col-md-4">
      <h3>{{vault?.name}}</h3>
    <h4>KEEPS:{{keep?.length}}</h4>
    </div>
    <div class="col-md-2">
      <button class="btn btn-secondary text-light rounded" @click="deleteVault()">DELETE VAULT</button>
    </div>
  </div>
  <div class="row" v-for="k in keep">
    <div class="col-md-4">
      <img height="75" :src="k?.img" alt="">
      <h2>{{k?.name}}</h2>
      <button @click="deleteVaultKeep()" class="btn" title="DELETE KEEP">❌</button>
   </div>
  </div>
  
</template>
<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { vaultsService } from "../services/VaultsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";

export default {
setup() {
const route = useRoute();

async function getKeepsByVaultId(){
  try {
    await vaultsService.getKeepsByVaultId(route.params.id);
  } catch (error) {
    logger.error(error)
    Pop.toast(error.message, 'error')
  }}

  async function setActive(){
  
  try {
    await vaultsService.getOne(route.params.id)
  } catch (error) {
    logger.error(error)
    Pop.toast(error.message, 'error')
  }
}

onMounted(()=> {
  getKeepsByVaultId()
  setActive()
})

  return {
keep: computed(()=> AppState.vaultKeeps),
vault: computed(()=> AppState.activeVault),


async deleteVault(){
  try {
    await vaultsService.delete(AppState.activeVault.id)
    Pop.toast('VAULT DELETED')
  } catch (error) {
    logger.error(error)
    Pop.toast(error.message, 'error')
  }
},

async deleteVaultKeep(){
  try {
    
    await keepsService.vaultKeepsDelete(AppState.vaultKeeps.id)
    Pop.toast('KEEP DELETED')
  } catch (error) {
    logger.error(error)
    Pop.toast(error.message, 'error')
  }
}



  };
},
};
</script>
<style lang="scss" scoped>
</style>