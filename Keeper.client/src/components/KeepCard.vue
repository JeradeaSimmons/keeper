<template>

<div class="rounded elevation-4 d-flex justify-content-between card bg-secondary">
  <div data-bs-toggle="modal" data-bs-target="#KeepsModal">
    <img class="img-fluid cardImg selectable" :src="keep.img" alt="" @click="setActive()">
  </div>
  <div class="">
    <h3 class="d-flex justify-content-between text-white">{{keep.name}}
      <router-link :to="{name: 'Profile', params: {id:keep.creatorId}}">
        <div class="">
          <img class="rounded-4 selectable" height="35" :src="keep.creator.picture" alt="">
        </div>
      </router-link>
    </h3>
  </div>

</div>
  
     <KeepsModal /> 
      
      
      
  
 




</template>
<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import KeepsModal from "./KeepsModal.vue";
export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    setup(props) {
        return {


          async setActive(){
            try {
              Modal.getOrCreateInstance(document.getElementById("KeepsModal")).toggle();
              await keepsService.getOne(props.keep.id)
            } catch (error) {
              logger.error(error)
              Pop.toast(error.message, 'error')
            }
          },
         
        };
    },
    components: { KeepsModal }
};
</script>
<style lang="scss" scoped>

.cardImg{
  object-fit: fill;
}

</style>