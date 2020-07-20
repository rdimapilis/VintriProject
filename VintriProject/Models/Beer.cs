using System.Collections.Generic;

namespace VintriProject.Models
{
    public class Beer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Tagline { get; set; }
        public string First_brewed { get; set; }
        public string Description { get; set; }
        public string Image_url { get; set; }
        public float Abv { get; set; }
        public string Ibu { get; set; }
        public string Target_fg { get; set; }
        public string Target_og { get; set; }
        public string Ebc { get; set; }
        public string Srm { get; set; }
        public string Ph { get; set; }
        public string Attenuation_level { get; set; }


        public Volume Volume { get; set; }

        public Boil_volume Boil_volume { get; set; }

        public Method Method { get; set; }

        public Ingredients Ingredients { get; set; }


        public IEnumerable<string> Food_pairing { get; set; }

        public string Brewers_tips { get; set; }
        public string Contributed_by { get; set; }

    }



}
