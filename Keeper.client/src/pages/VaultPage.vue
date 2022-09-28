<template>
  <div  v-for="k in keep">
  <div class="row mb-5 d-flex justify-content-between">
    <div class="col-md-4">
      <h3>SOON TO BE VAULT NAME</h3>
    <h4>KEEPS:{{keep.length}}</h4>
    </div>
    <div class="col-md-2">
      <button class="btn btn-secondary text-light rounded">DELETE VAULT</button>
    </div>
  </div>
  <div class="row">
    <div class="col-md-4">
      <img height="75" :src="k.img" alt="">
      <h2>{{k.name}}</h2></div>
   
  </div>
  </div>
</template>
<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
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

onMounted(()=> {
  getKeepsByVaultId()
})

  return {
keep: computed(()=> AppState.vaultKeeps),

  };
},
};
</script>
<style lang="scss" scoped>
</style>