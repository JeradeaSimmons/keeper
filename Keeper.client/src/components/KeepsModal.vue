<template>

<div class="modal fade" id="KeepsModal" tabindex="-1" aria-labelledby="KeepsModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header bg-secondary text-white">
        
          <h5><i class="icon mdi mdi-eye"></i>{{keep.views}}--</h5>
          <h5><i class="icon mdi mdi-bank"></i>{{keep.kept}}</h5>
        

        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body d-flex justify-content-evenly text-dark">
        <div class="col-md-6"> <img class="img-fluid modalImg" :src="keep.img" alt=""> </div>
        <div class="col-md-6">
          <h3>{{keep.name}}</h3>
          <p>{{keep.description}}</p>
        </div>
      </div>
      <div class="modal-footer justify-content-between bg-secondary text-white">
        <div class="dropdown">
          <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
            aria-expanded="false">
            Add to My Vault
          </button>
          <ul class="dropdown-menu" v-for="m in myVaults">
            <li><a class="dropdown-item" href="#" @click="vaultKeepKept(m.id)">{{m.name}}</a></li>
            <!-- <li><a class="dropdown-item" href="#">Another action</a></li>
            <li><a class="dropdown-item" href="#">Something else here</a></li> -->
          </ul>
        </div>
        <!-- <button type="button" class="btn btn-dark text-white" @click="vaultKeepKept()" >Add to My Vault</button> -->
        <h4><i @click="deleteKeep()" class="icon mdi mdi-trash-can selectable"></i></h4>
        <h5>
          <router-link v-if="keep.creatorId" class="" :to="{ name: 'Profile', params: {id: keep.creatorId}}">
            <img class="selectable" data-bs-dismiss="modal" height="35" :src="keep.creator?.picture" alt="">
            {{keep.creator?.name}}
          </router-link>

        </h5>
      </div>
    </div>
  </div>
</div>



</template>
<script>
import { AppState } from '../AppState';
import { computed } from "@vue/reactivity";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService";
import { keepsService } from "../services/KeepsService";

export default {
setup() {
  return {
myVaults: computed(()=> AppState.myVaults),
    keep: computed(()=> AppState.activeKeep),
    user: computed(()=> AppState.account),

async vaultKeepKept(id){
  try {
    let newKeptKeep = {keepId: AppState.activeKeep.id, vaultId: id }
    
    
    
    await vaultsService.vaultKeepKept(newKeptKeep)
    this.keep.kept++
    Pop.success('Keep Vaulted')
  } catch (error) {
    logger.error(error)
    Pop.toast(error.message, 'error')
  }
},

async deleteKeep(){
  try {
    await keepsService.delete(AppState.activeKeep.id)
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

.modalImg{
  height: 400px;
  width: 600px;
  image-rendering: auto;
}

</style>