<script setup>
import { ref, onMounted } from 'vue';
import CharactersDesktop from './components/CharactersDesktop.vue';
import CharactersMobile from './components/CharactersMobile.vue';

const isMobile = ref(window.innerWidth <= 768);

const checkScreenSize = () => {
  isMobile.value = window.innerWidth <= 768;
};

onMounted(() => {
  window.addEventListener('resize', checkScreenSize);
});
</script>

<template>
  <div>
    <CharactersMobile v-if="isMobile" />
    <CharactersDesktop v-else />
  </div>
</template>
