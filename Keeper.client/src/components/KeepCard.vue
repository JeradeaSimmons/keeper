<template>

<div class="rounded elevation-4 d-flex align-items-end mainImg">
  
  <div data-bs-toggle="modal" data-bs-target="#KeepsModal">
    <img class="img-fluid selectable opacity-75" :src="keep.img" alt="" @click="setActive()">
  </div>
  
    <h3 class="text-white nameImg elevation-4">{{keep.name}}
      <router-link :to="{name: 'Profile', params: {id:keep.creatorId}}">
      
          <img class="rounded-4 selectable" height="45" :src="keep.creator.picture" alt="">
       
      </router-link>
    </h3>
  

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

.mainImg{
  position: relative;
}

.nameImg{
  position: absolute;
}

</style>