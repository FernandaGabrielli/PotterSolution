<script setup>
import { ref, onMounted, computed } from 'vue';

const characters = ref([]);
const searchQuery = ref('');
const selectedCharacter = ref(null);
const isModalOpen = ref(false);
const randomCharacter = ref(null);

// Fetch characters from the API
const fetchCharacters = async () => {
  try {
    const response = await fetch('http://localhost:5297/api/data/characters');
    characters.value = await response.json();
  } catch (error) {
    console.error('Error fetching characters:', error);
  }
};

// Filter characters based on the search query
const filteredCharacters = computed(() =>
  characters.value.filter(c => c.fullName.toLowerCase().includes(searchQuery.value.toLowerCase()))
);

// Open modal with selected character
const openModal = (character) => {
  selectedCharacter.value = character;
  isModalOpen.value = true;
};

// Get a random character from the list
const getRandomCharacter = () => {
  if (characters.value.length > 0) {
    const randomIndex = Math.floor(Math.random() * characters.value.length);
    randomCharacter.value = characters.value[randomIndex];
    isModalOpen.value = true; // Opens modal with random character
  }
};

// Fetch characters when the component is mounted
onMounted(fetchCharacters);
</script>

<template>
  <div class="container">
    <h1>⚡ Harry Potter Characters ⚡</h1>
    <input v-model="searchQuery" placeholder="🔍 Search a character..." class="search-box" />

    <!-- Button to open the random character modal -->
    <div class="random-character">
      <button class="random-button" @click="getRandomCharacter">🎲 Get Random Character</button>
    </div>

    <div class="character-list">
      <div v-for="character in filteredCharacters" :key="character.index" class="character-card" @click="openModal(character)">
        <img :src="character.image" :alt="character.fullName" class="character-img" />
        <p>{{ character.fullName }}</p>
      </div>
    </div>

    <!-- Modal for selected character -->
    <div v-if="isModalOpen" class="modal" @click.self="isModalOpen = false">
      <div class="modal-content">
        <button class="close-button" @click="isModalOpen = false">✖</button>
        <h2>{{ selectedCharacter?.fullName || randomCharacter?.fullName }}</h2>
        <img :src="selectedCharacter?.image || randomCharacter?.image" :alt="selectedCharacter?.fullName || randomCharacter?.fullName" class="character-img-large" />
        <p><strong>House:</strong> {{ selectedCharacter?.hogwartsHouse || randomCharacter?.hogwartsHouse || 'Unknown' }}</p>
        <p><strong>Actor:</strong> {{ selectedCharacter?.interpretedBy || randomCharacter?.interpretedBy || 'Unknown' }}</p>
        <p><strong>Birthdate:</strong> {{ selectedCharacter?.birthdate || randomCharacter?.birthdate || 'Unknown' }}</p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.container {
  font-family: 'Raleway', sans-serif;
  text-align: center;
  padding: 20px;
  background-color: #1a1a1a;
  color: #f5f5f5;
}

.search-box {
  padding: 10px;
  width: 90%;
  max-width: 400px;
  margin-bottom: 20px;
  border: 2px solid #ffd700;
  border-radius: 5px;
  font-size: 16px;
  background-color: #333;
  color: white;
}

.character-list {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 10px;
}

.character-card {
  background-color: #222;
  padding: 10px;
  border-radius: 8px;
  text-align: center;
  cursor: pointer;
  width: 100px;
}

.character-img {
  width: 80px;
  height: 80px;
  border-radius: 5px;
}

/* Modal styles */
.modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.8);
  display: flex;
  align-items: center;
  justify-content: center;
}

.modal-content {
  background: #222;
  padding: 20px;
  border-radius: 8px;
  text-align: center;
  color: white;
  width: 90%;
  max-width: 400px;
  position: relative;
}

.character-img-large {
  width: 80%;
}

.close-button {
  position: absolute;
  top: 10px;
  right: 10px;
  background: none;
  border: none;
  font-size: 20px;
  color: white;
  cursor: pointer;
  padding: 5px;
}

/* Random character button styles */
.random-character {
  padding: 15px;
  margin-bottom: 20px;
  border-radius: 8px;
  text-align: center;
}

.random-button {
  background-color: #ffd700;
  color: #222;
  border: none;
  padding: 10px 20px;
  font-size: 16px;
  border-radius: 5px;
  cursor: pointer;
  margin-bottom: 20px;
}

.random-button:hover {
  background-color: #d4a800;
}
</style>
