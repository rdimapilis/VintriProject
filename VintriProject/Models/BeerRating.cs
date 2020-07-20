using System.Collections.Generic;

namespace VintriProject.Models
{
    public class BeerRating
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Ratings> UserRatings { get; set; }
    }
}
