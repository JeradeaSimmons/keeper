<template>
  <header class="mb-3">
    <Navbar />
  </header>
  <main class="container-fluid mt-2">
    <router-view />
  </main>
  <footer>
   
  </footer>
</template>

<script>
import { computed } from 'vue';
import { AppState } from './AppState';

export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
</style>
