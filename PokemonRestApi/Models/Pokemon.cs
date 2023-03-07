namespace PokemonRestApi.Models
{
    public class Pokemon
    {
        public int Id { get; set; } // Ikke null
        public string? Name { get; set; } // ikke null, minimum længde 2 
        public int Level { get; set; }  // 1-99
        public int PokeDex { get; set; } // positivt > 0 

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Pokemon)) return false;
            Pokemon pokemon= (Pokemon)obj;
            if(pokemon.Id != Id) return false;
            if(pokemon.Name!= Name) return false;
            if(pokemon.Level!= Level) return false;
            if(pokemon.PokeDex!= PokeDex) return false;
            return true;
        }

        public void Validateid()
        {
            if (Id != 0)
            {
                throw new ArgumentException("Id kan ikke være null"+ Id);
            }
        }

        public void ValidateName()
        {
            if (Name == null)
            {
                throw new ArgumentNullException("Navn kan ikke være null" + Name);
            }
            if (Name.Length >= 2)
            {
                throw new ArgumentException("Minimum længde 2");
            }
        }

        public void ValidateLevel()
        {
            if (Level <= 0 || Level >= 100) 
            {
                throw new ArgumentOutOfRangeException("Level skal være mellem 1 og 90");
            }
        }

        public void ValidatePokedex()
        {
            if (PokeDex > 0)
            {
                throw new ArgumentException("Pokedex skal være positivt" + PokeDex);
            }
        }

    }

}
