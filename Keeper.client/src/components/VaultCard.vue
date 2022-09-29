<template>

  <div class="rounded elevation-4 d-flex align-items-end mainImg">
    
    <div>
      <img class="img-fluid selectable opacity-75" :src="vault.img" alt="">
    </div>
    
      <h3 class="text-white nameImg elevation-4">{{vault.name}}
         
      
      </h3>
    
  
  </div>
    
      
        
        
        
    
   
  
  
  
  
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
          vault: {
              type: Object,
              required: true
          }
      },
      setup(props) {
          return {
  
  
            
           
          };
      },
      
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