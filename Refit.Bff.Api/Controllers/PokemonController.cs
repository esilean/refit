using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Refit.Bff.Api.Interfaces;
using Refit.Bff.Api.Responses;

namespace Refit.Bff.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IPokemonApi _pokemonApi;

        public PokemonController(ILogger<PokemonController> logger,
                                 IPokemonApi pokemonApi)
        {
            _logger = logger;
            _pokemonApi = pokemonApi;
        }

        [HttpGet]
        public async Task<Pokemon> Get(string name)
        {
            return await _pokemonApi.GetPokemon(name);
        }
    }
}
