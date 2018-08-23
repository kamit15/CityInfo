using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CityInfo.API.Controllers
{
    public class CitiesController : Controller
    {
        [HttpGet("api/cities")]
        public IActionResult GetCities()
        {
            return new JsonResult(new List<object>()
            {
                new { id=1, Name="Bangalore"},
                new { id=2, Name="Muzaffarpur"}
            });
        }
    }
}