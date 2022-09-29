<template>

  <div class="row pb-5 mt-3">
    <div class="col-md-2 offset-1">
      <img height="150" class="rounded" :src="account.picture" alt="">
    </div>
    <div class="col-md-3">
      <h2>{{account.name}}</h2>
      <h2>Vaults:</h2><h2>{{vault.length}}</h2>
      <h2>Keeps:</h2><h2>{{keep.length}}</h2>
    </div>
  </div>
  <div class="row mt-4 pb-4">

<div class="col-3">
  <h3>VAULTS:</h3>
  <VaultForm>
    <template #button>
      <button class="btn btn-secondary" data-bs-target="#vault-form" data-bs-toggle="modal" title="ADD VAULT">➕</button>
    </template>
  </VaultForm>
</div>
</div>
<div class="row"><div class="col-3 m-3" v-for="v in vault" :key="v.id">
  <router-link :to="{name: 'Vault', params: {id: v?.id}}">
    <!-- <img class="m-1" :src="v.img" alt=""> -->
    <VaultCard :vault="v"/>
  </router-link>
  
</div></div>




  <div class="row mt-4 mb-4">
  <div class="col-3">
    <h3>KEEPS:</h3>
    <KeepForm>
      <template #button>
        <button class="btn btn-secondary" data-bs-target="#keep-form" data-bs-toggle="modal" title="ADD KEEP">➕</button>
      </template>
    </KeepForm>
  </div>
</div>
<div class="row">
   <div class="col-3 mainImg d-flex align-items-end m-3" v-for="k in keep" :key="k.id">
    <!-- <img class="m-1" :src="k.img" alt="">
    <h4 class="nameImg text-white">{{k.name}}</h4> -->
    <KeepCard :keep="k"/>
  </div>
</div>
 


</template>

<script>
import { computed, onMounted} from 'vue'
import { AppState } from '../AppState'
import { keepsService } from "../services/KeepsService";
import { vaultsService } from "../services/VaultsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import VaultForm from '../components/VaultForm.vue';
import KeepForm from '../components/KeepForm.vue';
import KeepCard from "../components/KeepCard.vue";
export default {
  name: 'Account',
  setup() {


  //   async function getKeepsByProfile(){
  // try {

  //     await keepsService.getKeepsByProfile(AppState.account.id);
  
  // } catch (error) {
  //   logger.error(error)
  //   Pop.toast(error.message, 'error')
  // }}

//   async function getMyVaults(){
//   try {
//     await vaultsService.getMyVaults();
//   } catch (error) {
//     logger.error(error)
//     Pop.toast(error.message, 'error')
//   }

// }
onMounted(()=> {
  // getKeepsByProfile()
  // getMyVaults()
})
    return {
      account: computed(() => AppState.account),
      vault: computed(()=> AppState.myVaults),
    keep: computed(()=> AppState.myKeeps),
    }
  },
  components: { VaultForm, KeepForm, KeepCard }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.mainImg{
  position: relative;
  
}

.nameImg{
  position: absolute;
}
</style>
