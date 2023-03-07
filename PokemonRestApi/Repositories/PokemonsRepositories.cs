using PokemonRestApi.Models;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace PokemonRestApi.Repositories
{
    public class PokemonsRepositories
    {
        private int _nextID; 
        private List<Pokemon> _pokemons; 
        
        public PokemonsRepositories()
        {
            _nextID = 1;
            _pokemons = new List<Pokemon>()
            {
                new Pokemon() { Id = _nextID++, Name = "Pikcahu", Level = 9999, PokeDex = 25 },
                new Pokemon() { Id = _nextID++, Name = "Charmander", Level = 414, PokeDex = 15 },
                new Pokemon() { Id = _nextID++, Name = "Bulbasaur", Level = 220, PokeDex = 20 }
                            

            };
        }
        // Vi vil beskytte listen og derfor opretter vi en kopi, så den orginale liste ikke bliver ændret i
        // af nogle som får fat på den. 
        public List<Pokemon> GetAll(string? namefilter, int levelfilter)
        {
            List<Pokemon> result = new List<Pokemon>(_pokemons);

            if (namefilter != null)
            {
                result = result.FindAll(pokemon => pokemon.Name.Contains(namefilter, StringComparison.InvariantCultureIgnoreCase));
            }

            if (levelfilter != 0)
            {
                result = result.FindAll(pokemon => pokemon.Level.Equals(levelfilter));
            }


            return result;
        }

        public Pokemon AddPokemon(Pokemon newPokemon)
        {
            newPokemon.Validateid();
            newPokemon.Id = _nextID++;
            _pokemons.Add(newPokemon);
            return newPokemon;
        }

        public Pokemon GetById(int id)
        {
            return _pokemons.Find(pokemon => pokemon.Id == id);
        }
        public Pokemon DeletePokemon(int id)
        {

           Pokemon pokemon = GetById(id);
            if (pokemon != null) { return null; }
            _pokemons.Remove(pokemon); return pokemon;
        }
        public Pokemon UpdatePokemon(int id, Pokemon updates) 
        {
            updates.Validateid();
            Pokemon pokemon = GetById(id);
            if (pokemon != null)  return null;
            pokemon.Level = updates.Level;
            pokemon.Name = updates.Name;
            pokemon.PokeDex= updates.PokeDex;
            return pokemon;

          
        }    

        public void Validate()
        {
            
        }
    }
}
