<template>

<div class="row d-flex">
  <div class="col-md-2 offset-1">
    <img height="150" :src="profile.picture" alt="">
  </div>
  <div class="col-md-3">
    <h2>{{profile.name}}</h2>
    <h2>Vaults:</h2>
    <h2>Keeps:</h2>
  </div>
</div>





</template>
<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import { profilesService } from "../services/ProfilesService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";

export default {
setup() {
  const route = useRoute();

async function getProfile(){
  try {
    await profilesService.getProfile(route.params.id);
  } catch (error) {
    logger.error(error)
    Pop.toast(error.message, 'error')
  }
}
onMounted(()=> {
  getProfile()
})
  return {
    profile: computed(()=> AppState.currentProfile),
route,
  };
},
};
</script>
<style lang="scss" scoped>


</style>
