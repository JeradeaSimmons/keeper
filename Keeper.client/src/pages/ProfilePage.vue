<template>

<div class="row pb-5" >
  <div class="col-md-2 offset-1">
    <img height="150" :src="profile.picture" alt="">
  </div>
  <div class="col-md-3">
    <h2>{{profile.name}}</h2>
    <h2>Vaults:</h2><h2>{{vault.length}}</h2>
    <h2>Keeps:</h2><h2>{{keep.length}}</h2>
    </div>
</div>

<div class="row mt-4 pb-4">

  <div class="col-3">
    <h3>VAULTS:</h3>
  </div>
</div>
<div class="row">
  <div class="col-3" v-for="v in vault" :key="v.id">
    <router-link :to="{name: 'Vault', params: {id: v?.id}}">
      <!-- <img class="m-1" height="75" :src="v.img" alt=""> -->
      <vault-card :vault="v"/>
    </router-link>
    
  </div>
 </div>



<div class="row mt-4">
  <div class="col-3">
    <h3>KEEPS:</h3>
   
  </div>
</div>
<div class="row">
  <div class="col-3" v-for="k in keep" :key="k.id">
    <!-- <img class="m-1" height="75" :src="k.img" alt=""> -->
    <KeepCard :keep="k"/>
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
import VaultCard from "../components/VaultCard.vue";



export default {
    setup() {
        const route = useRoute();
        async function getProfile() {
            try {
                await profilesService.getProfile(route.params.id);
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message, "error");
            }
        }
        async function getKeepsByProfile() {
            try {
                await keepsService.getKeepsByProfile(route.params.id);
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message, "error");
            }
        }
        async function getVaultsByProfile() {
            try {
                await vaultsService.getVaultsByProfile(route.params.id);
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message, "error");
            }
        }
        onMounted(() => {
            getProfile();
            getKeepsByProfile();
            getVaultsByProfile();
        });
        return {
            vault: computed(() => AppState.vault),
            keep: computed(() => AppState.keeps),
            profile: computed(() => AppState.currentProfile),
            route,
        };
    },
    components: { VaultCard }
};
</script>
<style lang="scss" scoped>


</style>
