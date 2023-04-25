using Microsoft.AspNetCore.Mvc;
using PokemonRestApi.Repositories;
using PokemonRestApi.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokemonRestApi.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    // URI : api / pokemons, den ved godt den skal tage pokemons fra PokemonsControllr
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private IPokemonsRepository _repository; 

        public PokemonsController(IPokemonsRepository repository)
        {
            _repository = repository;
        }
        // GET: api/Pokemons?Namefilter=Pikachu


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EnableCors("AllowAll")]
        [HttpGet]
        public ActionResult<IEnumerable<Pokemon>> GetAll([FromQuery] string? namefilter, [FromQuery] int levelfilter)
        {
            List<Pokemon> result = _repository.GetAll(namefilter, levelfilter);
            if (result.Count < 1)
            {
                return NoContent();
            }
            Response.Headers.Add("TotalAmount", "" + result.Count());
            return Ok(result);
        }


        // GET api/<PokemonsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EnableCors("AllowAll")]
        [HttpGet("{id}")]
        public ActionResult<Pokemon> Get(int id)
        {
            Pokemon pokemon = _repository.GetById(id);
            		if (pokemon == null) return NotFound("No such item, id: " + id);
            		return Ok(pokemon);
       }


    // POST api/<PokemonsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public IActionResult Post([FromBody] Pokemon newPokemon)
        {
            try
            {
                Pokemon createdPokemon = _repository.AddPokemon(newPokemon);
                return Created($"api/pokemons{createdPokemon.Id}", newPokemon);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message); // 400 Bad Request
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode(422, ex.Message); // 422 Unprocessable Entity
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message); // 409 Conflict
            }
        }

        // PUT api/<PokemonsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pokemon updates)
        {
            try {

                Pokemon updatedPokemon = _repository.UpdatePokemon(id, updates);
                if(updatedPokemon == null)
                {
                    return NotFound();
                }
            
                return Ok(updatedPokemon);

            }
            
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode(422, ex.Message);
            }
            
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
            
            
        }


        // DELETE api/<PokemonsController>/5
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EnableCors]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.DeletePokemon(id);
                if (_repository.GetById(id) == null)
                    return NotFound("No such item, id: " + id);
                return Ok("Item deleted succesfully, id:" + id);
            }
            catch (Exception ex)
            {
                // Handle the exception thrown by DeletePokemon method.
                return StatusCode(500, "An error occurred while deleting the item.");
            }
            
        }
    }
}
