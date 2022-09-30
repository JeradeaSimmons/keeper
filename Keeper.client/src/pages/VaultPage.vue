<template>

  <div class="row mb-5 d-flex justify-content-between">
    <div class="col-md-4 masonry">
      <h3>{{vault?.name}}</h3>
      <h4>KEEPS:{{keep?.length}}</h4>
    </div>
    <div class="col-md-2">
      <button class="btn btn-secondary text-light rounded" @click="deleteVault()">DELETE VAULT</button>
    </div>
  </div>
  <div class="row">
    <div class="col-md-3" v-for="k in keep" :key="k.id">
      <KeepCard :keep="k" />
    </div>
  
      
    
  </div>

</template>
<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import { router } from "../router";
import { keepsService } from "../services/KeepsService";
import { vaultsService } from "../services/VaultsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";

export default {
  setup() {
    const route = useRoute();

    async function getKeepsByVaultId() {
      try {
        await vaultsService.getKeepsByVaultId(route.params.id);
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    }

    async function setActive() {

      try {
        await vaultsService.getOne(route.params.id)
      } catch (error) {
        Pop.toast(error.message, 'NO ACCESS')
        router.push({ name: 'Home' })
        // TODO when catching an error....do the thing..if you catch an error from this request, the user should not be able to access the page
        // logger.error(error)

      }
    }

    onMounted(() => {
      getKeepsByVaultId()
      setActive()
    })

    return {
      keep: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.activeVault),


      async deleteVault() {
        try {
          await vaultsService.delete(AppState.activeVault.id)
          Pop.toast('VAULT DELETED')
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },

      async deleteVaultKeep() {
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
.masonry {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    width: 150px;
    background: #EC985A;
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
  } 
}
</style>