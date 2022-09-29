<template>
<slot name="button">
    
  </slot>


  <Modal id="keep-form">
    
    <template #header>ADD KEEP</template>
    <template #body>
      <form class="row p-5" @submit.prevent="handleSubmit">
        <div class="col-md-4 mb-3">
          <label for="keep-name" class="form-label">Keep Name</label>
          <input required v-model="editable.name" type="text" class="form-control" name="keep-name" id="keep-name"
            placeholder="Keep Name">
        </div>
        <div class="col-md-4 mb-3">
          <label for="keep-img" class="form-label">Keep Picture</label>
          <input required v-model="editable.img" type="text" class="form-control" name="keep-img" id="keep-img"
            aria-describedby="helpId" placeholder="Picture">
        </div>
        <div class="col-12 mb-3">
          <label for="keep-description" class="form-label">Description</label>
          <textarea v-model="editable.description" class="form-control" name="keep-description" id="keep-description" rows="3"></textarea>
        </div>
        <div class="col-12">
          <button class="btn btn-secondary" data-bs-dismiss="modal" title="submit form">Create <i class="mdi mdi-plus"></i></button>
        </div>
      </form>
    </template>
   
  </Modal>








</template>
<script>
import { ref } from "vue";
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";

export default {
setup() {
  const editable = ref({})
  return {
    editable,
    async handleSubmit(){
      try {
        if (!editable.value.id){
          await keepsService.create(editable.value)
          Pop.success("Keep Created")
        }
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
</style>