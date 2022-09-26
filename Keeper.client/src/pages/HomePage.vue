<template>
  <div class="row justify-content-evenly mt-3">
    <div class="col-sm-5 col-md-2 m-2 text-center" v-for="k in keeps" :key="k.id">
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
</style>
