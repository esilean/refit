using System.Collections.Generic;

namespace Refit.Bff.Api.Responses
{
    public class Pokemon
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public IReadOnlyList<Abilities> Abilities { get; set; }
    }

    public class Abilities
    {
        public Ability Ability { get; set; }
    }

    public class Ability
    {
        public string Name { get; set; }
    }
}