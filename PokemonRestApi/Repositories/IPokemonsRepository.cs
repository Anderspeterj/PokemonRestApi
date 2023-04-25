using PokemonRestApi.Models;

namespace PokemonRestApi.Repositories
{
    public interface IPokemonsRepository
    {
        Pokemon AddPokemon(Pokemon newPokemon);
        Pokemon? DeletePokemon(int id);
        List<Pokemon> GetAll(string? namefilter, int levelfilter);
        Pokemon? GetById(int id);
        Pokemon? UpdatePokemon(int id, Pokemon updates);
    }
}
