<template></template>
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
keep: computed(()=> AppState.vaultKeeps)
  };
},
};
</script>
<style lang="scss" scoped>
</style>