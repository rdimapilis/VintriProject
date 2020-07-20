using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using VintriProject.Models;

namespace VintriProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        List<Ratings> _ratings = new List<Ratings>();
        private IWebHostEnvironment _webHostEnvironment;

        private string ContentRootPath { get; set; }
        private string DataPath { get; set; }

        public RatingsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            ContentRootPath = _webHostEnvironment.ContentRootPath;
            DataPath = ContentRootPath + @"\Data\database.json";

        }


        [HttpGet]
        public IActionResult Get()
        {

            var data = System.IO.File.ReadAllText(DataPath);

            if (data.Contains("UserName"))
            {
                var ratings = JsonConvert.DeserializeObject<List<Ratings>>(data);
                return Ok(ratings);
            }
            else
            {
                return Ok("No Data yet");
            }

        }



        //===========Task No. 2====================
        [Route("BeerRatings/{BeerName}")]
        [HttpGet]
        public IActionResult GetBeerRatings(string BeerName)
        {

            Beer selectedBeer = GetBeer(BeerName);


            if (!(selectedBeer == null))
            {
                IEnumerable<Ratings> ratings = GetSpecificRatings(Convert.ToInt32(selectedBeer.Id));


                BeerRating beerRating = new BeerRating()
                {
                    Id = selectedBeer.Id,
                    Name = selectedBeer.Name,
                    Description = selectedBeer.Name,
                    UserRatings = ratings
                };
                var jsonBeerRating = JsonConvert.SerializeObject(beerRating);
                return Ok(JValue.Parse(jsonBeerRating).ToString(Formatting.Indented));
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Beer Name");
            }



        }





        private IEnumerable<Ratings> GetSpecificRatings(int BeerId)
        {

            var data = System.IO.File.ReadAllText(DataPath);
            var _ratings = JsonConvert.DeserializeObject<List<Ratings>>(data);

            var particularRatings = _ratings.Where(x => x.BeerId == BeerId).ToList();
            return particularRatings;

        }




        private Beer GetBeer(string BeerName)
        {
            using (WebClient wc = new WebClient())
            {
                var data = wc.DownloadString("https://api.punkapi.com/v2/beers");

                var myobjList = JsonConvert.DeserializeObject<IEnumerable<Beer>>(data);


                var beer = (from u in myobjList
                            where u.Name == BeerName
                            select u).FirstOrDefault();

                return beer;


            }
        }


        //===========Task No. 1====================
        [HttpPost("{Id}")]
        public IActionResult Post(int Id, [FromBody] Ratings Ratings)
        {
            IActionResult actionResult;
            if (ValidateBeer(Id))
            {
                Ratings.BeerId = Id;
                WriteToDatabase(Ratings);
                actionResult = StatusCode(StatusCodes.Status201Created, "Record Saved!");
            }
            else
            {
                actionResult = StatusCode(StatusCodes.Status400BadRequest, "Invalid Beer Id");
            }

            return actionResult;
        }






        private void WriteToDatabase(Ratings ratings)
        {
            var data = System.IO.File.ReadAllText(DataPath);
            var _ratings = JsonConvert.DeserializeObject<List<Ratings>>(data);

            _ratings.Add(ratings);

            data = JsonConvert.SerializeObject(_ratings);
            System.IO.File.WriteAllText(DataPath, data);
        }




        private bool ValidateBeer(int Id)
        {

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(String.Format("https://api.punkapi.com/v2/beers/{0}", Id.ToString()))
            };


            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            HttpResponseMessage response = client.GetAsync(String.Format("https://api.punkapi.com/v2/beers/{0}", Id.ToString())).Result;


            if (response.IsSuccessStatusCode)
            {

                return true;
            }
            else
            {
                return false;
            }


        }



    }
}
