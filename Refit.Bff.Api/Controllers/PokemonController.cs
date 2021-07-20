using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Refit.Bff.Api.Interfaces;
using Refit.Bff.Api.Metrics;
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
            var pokemon = await HttpRequestMetric.RunFuncAsync<Pokemon>(async () =>
            {
                return await _pokemonApi.GetPokemon(name).ConfigureAwait(false);
            }, _logger);

            return pokemon;
        }

        [HttpGet("metrics")]
        public async Task<Pokemon> GetHttpMetric(string name)
        {
            var pokemon = await HttpRequestMetric.RunHttpAsync<Pokemon>(async () =>
            {
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri("https://pokeapi.co/api/"),
                    Timeout = TimeSpan.FromSeconds(10)
                };

                return await httpClient.GetAsync($"v2/pokemon/{name}").ConfigureAwait(false);
            }, _logger);

            return pokemon;
        }
    }
}
