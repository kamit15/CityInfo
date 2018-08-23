using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        [HttpGet()]
        public IActionResult GetCities()
        {
            //return new JsonResult(new List<object>()
            //{
            //    new { id=1, Name="Bangalore"},
            //    new { id=2, Name="Muzaffarpur"}
            //});
            //var temp = new JsonResult(CitiesDataStore.Current.Cities);
            //temp.StatusCode = 200;

            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            // find city
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if(cityToReturn == null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);
            //return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
        }

    }
}