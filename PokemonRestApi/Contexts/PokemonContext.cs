using Microsoft.EntityFrameworkCore;
using PokemonRestApi.Models;

namespace PokemonRestApi.Contexts
{
    public class PokemonContext : DbContext
    {

        public PokemonContext(DbContextOptions<PokemonContext> options) : base(options) { }

        public DbSet<Pokemon> pokemons { get; set; }

    }
}
