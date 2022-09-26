<template>
  <div class="masonry">
    <div class="text-center" v-for="k in keeps" :key="k.id">
      <KeepCard :keep="k" />
    </div>
  </div>
    





</template>
<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import KeepCard from "../components/KeepCard.vue";



export default {

setup() {


async function getKeeps(){
  try {
    await keepsService.getKeeps();
  } catch (error) {
    logger.error(error)
    Pop.toast(error.message, 'error')
  }
}

onMounted(()=> {
  getKeeps();
});
  return {
keeps: computed(()=> AppState.keeps),


  };
},
components: {KeepCard},

}
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
