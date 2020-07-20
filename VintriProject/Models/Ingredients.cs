using System.Collections.Generic;

namespace VintriProject.Models
{
    public class Ingredients
    {
        public IEnumerable<Malt> Malts { get; set; }
        public IEnumerable<Hops> Hops { get; set; }
        public string Yeast { get; set; }

    }
}
