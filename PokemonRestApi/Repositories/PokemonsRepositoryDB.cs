using PokemonRestApi.Models;
using PokemonRestApi.Contexts;

namespace PokemonRestApi.Repositories
{
    public class PokemonsRepositoryDB : IPokemonsRepository
    {
        private PokemonContext _context;

        public PokemonsRepositoryDB(PokemonContext context)
        {
            _context = context;
        }

        public Pokemon AddPokemon(Pokemon newPokemon)
        {
            newPokemon.Id = 0;
            _context.pokemons.Add(newPokemon);
            _context.SaveChanges();
            return newPokemon;
        }

        public Pokemon? DeletePokemon(int id)
        {
            Pokemon? pokemontoBeDeleted = GetById(id);
            if (pokemontoBeDeleted == null)
            {
                return null;
            }
            _context.pokemons.Remove(pokemontoBeDeleted);
            _context.SaveChanges();
            return pokemontoBeDeleted;
        }

        public List<Pokemon> GetAll(string? namefilter, int levelfilter)
        {
            return _context.pokemons.ToList();
        }

        public Pokemon? GetById(int id)
        {
            return _context.pokemons.Find(id);
        }

        public Pokemon? UpdatePokemon(int id, Pokemon updates)
        {
        
            Pokemon? pokemonToBeUpdated = GetById(id);
            if (pokemonToBeUpdated == null)
            {
                return null;
            }
            pokemonToBeUpdated.Name = updates.Name;
            pokemonToBeUpdated.Level = updates.Level;
            pokemonToBeUpdated.PokeDex = updates.PokeDex;

            _context.pokemons.Update(pokemonToBeUpdated);
            _context.SaveChanges();
            return pokemonToBeUpdated;
        }

    }
}
