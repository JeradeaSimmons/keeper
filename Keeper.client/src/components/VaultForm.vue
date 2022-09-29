<template>
<slot name="button">
    
  </slot>


  <Modal id="vault-form">
    
    <template #header>ADD VAULT</template>
    <template #body>
      <form class="row p-5" @submit.prevent="handleSubmit">
        <div class="col-md-4 mb-3">
          <label for="vault-name" class="form-label">Vault Name</label>
          <input required v-model="editable.name" type="text" class="form-control" name="vault-name" id="vault-name"
            placeholder="Vault Name">
        </div>
        <div class="col-md-4 mb-3">
          <label for="vault-img" class="form-label">Vault Picture</label>
          <input required v-model="editable.img" type="text" class="form-control" name="vault-img" id="vault-img"
            aria-describedby="helpId" placeholder="Picture">
        </div>
        <div class="col-md-4 mb-3">
          <label for="vault-isPrivate" class="form-label">Private?</label>
          <input v-model="editable.isPrivate" type="checkbox" name="vault-isPrivate" id="vault-isPrivate"
            aria-describedby="helpId" placeholder="">
        </div>
        <div class="col-12 mb-3">
          <label for="vault-description" class="form-label">Description</label>
          <textarea v-model="editable.description" class="form-control" name="vault-description" id="vault-description" rows="3"></textarea>
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
import { vaultsService } from "../services/VaultsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";

export default {
setup() {
  const editable = ref({})
  return {
    editable,
    async handleSubmit(){
      try {
        if (!editable.value.id) {
          await vaultsService.create(editable.value);
          Pop.success("Vault Created")
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