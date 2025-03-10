<script setup>
import { ref, onMounted, computed } from 'vue';

const characters = ref([]);
const searchQuery = ref('');
const randomCharacter = ref(null);
const isModalOpen = ref(false);

const fetchCharacters = async () => {
  try {
    const response = await fetch('http://localhost:5297/api/data/characters');
    characters.value = await response.json();
  } catch (error) {
    console.error('Erro ao buscar personagens:', error);
  }
};

const filteredCharacters = computed(() =>
  characters.value.filter(c => c.fullName.toLowerCase().includes(searchQuery.value.toLowerCase()))
);

const getRandomCharacter = () => {
  if (characters.value.length > 0) {
    const randomIndex = Math.floor(Math.random() * characters.value.length);
    randomCharacter.value = characters.value[randomIndex];
    isModalOpen.value = true; // Abre o modal ao pegar um personagem aleatório
  }
};

onMounted(fetchCharacters);
</script>

<template>
  <div class="container">
    <h1>⚡ Harry Potter Characters ⚡</h1>
    <input v-model="searchQuery" placeholder="🔍 Search a character..." class="search-box" />

    <!-- Botão para abrir o modal do personagem aleatório -->
    <div class="random-character">
      <button class="random-button" @click="getRandomCharacter">🎲 Get Random Character</button>
    </div>

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
          <td>{{ character.hogwartsHouse || 'Unknown' }}</td>
          <td>{{ character.interpretedBy || 'Unknown' }}</td>
          <td>{{ character.birthdate || 'Unknown' }}</td>
        </tr>
      </tbody>
    </table>

    <!-- Modal para o personagem aleatório -->
    <div v-if="isModalOpen" class="modal" @click.self="isModalOpen = false">
      <div class="modal-content">
        <button class="close-button" @click="isModalOpen = false">✖</button>
        <h2>{{ randomCharacter?.fullName }}</h2>
        <img :src="randomCharacter?.image" :alt="randomCharacter?.fullName" class="character-img-large" />
        <p><strong>House:</strong> {{ randomCharacter?.hogwartsHouse || 'Unknown' }}</p>
        <p><strong>Actor:</strong> {{ randomCharacter?.interpretedBy || 'Unknown' }}</p>
        <p><strong>Birthdate:</strong> {{ randomCharacter?.birthdate || 'Unknown' }}</p>
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

.character-img {
  width: 50px;
  height: 50px;
  border-radius: 5px;
}

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

/* Estilos do Modal */
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
</style>
