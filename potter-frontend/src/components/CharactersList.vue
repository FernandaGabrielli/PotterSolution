<script setup>
import { ref, onMounted, computed } from 'vue';

// Estado para armazenar os personagens e a busca
const characters = ref([]);
const searchQuery = ref('');

// Função para buscar os personagens do backend
const fetchCharacters = async () => {
  try {
    const response = await fetch('http://localhost:5297/api/data/characters');
    characters.value = await response.json();
  } catch (error) {
    console.error('Erro ao buscar personagens:', error);
  }
};

// Filtrar personagens dinamicamente
const filteredCharacters = computed(() =>
  characters.value.filter(c => c.fullName.toLowerCase().includes(searchQuery.value.toLowerCase()))
);

// Chama a função ao montar o componente
onMounted(fetchCharacters);
</script>

<template>
  <div class="container">
    <h1>⚡ Lista de Personagens de Harry Potter ⚡</h1>

    <!-- Campo de busca -->
    <input v-model="searchQuery" placeholder="🔍 Buscar personagem..." class="search-box" />

    <div class="table-container">
      <table>
        <thead>
          <tr>
            <th>Imagem</th>
            <th>Nome</th>
            <th>Casa</th>
            <th>Ator</th>
            <th>Nascimento</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="character in filteredCharacters" :key="character.index">
            <td><img :src="character.image" :alt="character.fullName" class="character-img" /></td>
            <td>{{ character.fullName }}</td>
            <td>{{ character.hogwartsHouse }}</td>
            <td>{{ character.interpretedBy || 'Desconhecido' }}</td>
            <td>{{ character.birthdate }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<style scoped>
/* Estilização Geral */
.container {
  font-family: 'Raleway', sans-serif;
  text-align: center;
  padding: 20px;
  background-color: #1a1a1a;
  color: #f5f5f5;
}

/* Título */
h1 {
  font-size: 24px;
  margin-bottom: 15px;
}

/* Campo de Busca */
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

/* Tabela */
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

/* Efeito Hover */
tr:hover {
  background-color: #333;
}

/* Imagem */
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
