using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldWebServer.Models;

namespace WorldWebServer.Controllers
{
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        private WorldDbContext countryDbContext;

        public CountriesController() {
            string connectionString = "server=localhost;port=3306;database=world;userid=root;pwd=68windingbrook;sslmode=none";
            countryDbContext = WorldDbContextFactory.Create(connectionString);
        }
        [HttpGet]
        public ActionResult Get() {
            return Ok(countryDbContext.Country.ToArray());
        }
        
        
    }
}
