using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VintriProject.Models
{
    public class Ratings
    {

        [EmailAddress]
        public string UserName { get; set; }

        public int BeerId { get; set; }


        [Range(1,5)]
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
