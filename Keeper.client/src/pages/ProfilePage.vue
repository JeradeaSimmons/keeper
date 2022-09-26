<template>







</template>
<script>
import { onMounted } from "vue";
import { useRoute } from "vue-router";
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
route,
  };
},
};
</script>
<style lang="scss" scoped>


</style>
