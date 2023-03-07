using Microsoft.AspNetCore.Components.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonRestApi.Models;
using PokemonRestApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace PokemonRestApi.Repositories.Tests
{
    [TestClass()]
    public class PokemonsRepositoriesTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            var repository = new PokemonsRepositories();
            var pokemons = repository.GetAll();
            Assert.IsNotNull(pokemons);
            Assert.AreEqual(2, pokemons.Count());
            
        }
        public void GetByIdTest()
        {
            var repository = new PokemonsRepositories();
            var pokemon = repository.GetById(1);
            Assert.IsNotNull(pokemon);
            Assert.AreEqual(pokemon.Id, 1);
        }

        public void AddPokemonTest()
        {
            var newPokemon = new Pokemon() { Name = "Pikachu", Level = 1, PokeDex = 240 };
            var repository = new PokemonsRepositories();
            var pokemon = repository.AddPokemon(newPokemon);
            Assert.IsNotNull(pokemon);
            Assert.AreEqual(pokemon, repository.GetById(pokemon.Id));
        }

        public void DeletePokemonTest()
        {
            var repository = new PokemonsRepositories();
            var pokemon = new Pokemon() { Name = "Charmander", Level = 2, PokeDex = 270 }; 
            repository.DeletePokemon(pokemon.Id);
            var deletedPokemon = repository.GetById(pokemon.Id);
            Assert.IsNull(deletedPokemon);

        }

        public void UpdatePokemonTest()
        {
            var repository = new PokemonsRepositories();
            var newPokemon = new Pokemon() { Name = null, Level = 3, PokeDex = 250 };
            var pokemon = repository.GetById(1);
            repository.UpdatePokemon(newPokemon.Id, pokemon);
            var updatedPokemon = repository.GetById(newPokemon.Id);
            Assert.IsNotNull(updatedPokemon);
        }



    }
}