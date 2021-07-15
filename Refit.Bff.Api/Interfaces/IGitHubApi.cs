using System.Threading.Tasks;
using Refit.Bff.Api.Responses;

namespace Refit.Bff.Api.Interfaces
{
    public interface IPokemonApi
    {
        [Get("/pokemon/{name}")]
        Task<Pokemon> GetPokemon(string name);
    }
}