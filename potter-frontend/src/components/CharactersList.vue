<script setup>
import { ref, onMounted, computed } from 'vue';

// State to store characters and search query
const characters = ref([]);
const searchQuery = ref('');

// State to store the random character
const randomCharacter = ref(null);
const isModalOpen = ref(false); // Modal state

// Function to fetch all characters
const fetchCharacters = async () => {
  try {
    const response = await fetch('http://localhost:5297/api/data/characters');
    characters.value = await response.json();
  } catch (error) {
    console.error('Error fetching characters:', error);
  }
};

// Function to fetch a random character and open the modal
const fetchRandomCharacter = async () => {
  try {
    const response = await fetch('http://localhost:5297/api/data/random');
    randomCharacter.value = await response.json();
    isModalOpen.value = true; // Open modal
  } catch (error) {
    console.error('Error fetching random character:', error);
  }
};

// Filter characters dynamically
const filteredCharacters = computed(() =>
  characters.value.filter(c => c.fullName.toLowerCase().includes(searchQuery.value.toLowerCase()))
);

// Fetch characters on component mount
onMounted(fetchCharacters);
</script>

<template>
  <div class="container">
    <h1>⚡ List of Characters from Harry Potter ⚡</h1>

    <!-- Search field -->
    <input v-model="searchQuery" placeholder="🔍 Search a character..." class="search-box" />

    <!-- Random character button -->
    <button @click="fetchRandomCharacter" class="random-button">🎲 Try Your Luck</button>

    <!-- Random Character Modal -->
    <div v-if="isModalOpen" class="modal">
      <div class="modal-content">
        <button class="close-button" @click="isModalOpen = false">✖</button>
        <h2>🎭 Random Character!</h2>
        <img :src="randomCharacter.image" :alt="randomCharacter.fullName" class="character-img-large" />
        <p><strong>Name:</strong> {{ randomCharacter.fullName }}</p>
        <p><strong>House:</strong> {{ randomCharacter.hogwartsHouse || 'Unknown' }}</p>
        <p><strong>Actor:</strong> {{ randomCharacter.interpretedBy || 'Unknown' }}</p>
        <p><strong>Birthdate:</strong> {{ randomCharacter.birthdate || 'Unknown' }}</p>
      </div>
    </div>

    <div class="table-container">
      <table>
        <thead>
          <tr>
            <th>Image</th>
            <th>Name</th>
            <th>House</th>
            <th>Actor</th>
            <th>Birthdate</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="character in filteredCharacters" :key="character.index">
            <td><img :src="character.image" :alt="character.fullName" class="character-img" /></td>
            <td>{{ character.fullName }}</td>
            <td>{{ character.hogwartsHouse }}</td>
            <td>{{ character.interpretedBy || 'Unknown' }}</td>
            <td>{{ character.birthdate }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<style scoped>
/* General Styling */
.container {
  font-family: 'Raleway', sans-serif;
  text-align: center;
  padding: 20px;
  background-color: #1a1a1a;
  color: #f5f5f5;
}

/* Title */
h1 {
  font-size: 24px;
  margin-bottom: 15px;
}

/* Search Box */
.search-box {
  padding: 8px;
  width: 80%;
  max-width: 400px;
  margin-bottom: 20px;
  border: 2px solid #ffd700;
  border-radius: 5px;
  font-size: 16px;
  background-color: #333;
  color: white;
}

/* Random Button */
.random-button {
  padding: 10px 15px;
  font-size: 18px;
  background-color: #ffd700;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-weight: bold;
  transition: 0.3s;
}

.random-button:hover {
  background-color: #ffcc00;
}

/* Modal */
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
  max-width: 400px;
  width: 100%;
  position: relative;
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
}

/* Modal Image */
.character-img-large {
  width: 100%;
  max-width: 250px;
  border-radius: 10px;
  margin-top: 10px;
}

/* Table */
.table-container {
  overflow-x: auto;
}

table {
  width: 100%;
  border-collapse: collapse;
  background-color: #222;
}

th, td {
  padding: 12px;
  text-align: left;
  border-bottom: 2px solid #ffd700;
}

th {
  background-color: #444;
  color: #ffd700;
}

/* Hover Effect */
tr:hover {
  background-color: #333;
}

/* Table Image */
.character-img {
  width: 50px;
  height: 50px;
  border-radius: 5px;
  transition: transform 0.2s;
}

.character-img:hover {
  transform: scale(1.1);
}
</style>
